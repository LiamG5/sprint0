using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace sprint0.Managers
{
    public class RoomTransitionManager
    {
        private bool isTransitioning;
        private float transitionProgress;
        private readonly float transitionSpeed;
        private TransitionState currentState;
        private TransitionType transitionType;
        private Action onTransitionMiddle;
        private Texture2D fadeTexture;
        private Rectangle screenRectangle;
        private Vector2 slideOffset;
        private TransitionDirection slideDirection;
        private RenderTarget2D oldRoomTexture;
        private RenderTarget2D newRoomTexture;
        private GraphicsDevice graphicsDevice;
        private bool capturedOldRoom;
        private bool capturedNewRoom;
        private bool middleInvoked; // NEW

        private enum TransitionState
        {
            None,
            FadingOut,
            Sliding,
            FadingIn
        }

        public enum TransitionType
        {
            Fade,
            Slide,
            Push
        }

        public bool IsTransitioning => isTransitioning;

        public RoomTransitionManager(GraphicsDevice graphicsDevice, int screenWidth, int screenHeight, float transitionSpeed = 0.08f)
        {
            this.graphicsDevice = graphicsDevice;
            this.transitionSpeed = transitionSpeed;
            this.isTransitioning = false;
            this.transitionProgress = 0f;
            this.currentState = TransitionState.None;
            this.transitionType = TransitionType.Push;

            fadeTexture = new Texture2D(graphicsDevice, 1, 1);
            fadeTexture.SetData(new[] { Color.Black });

            int gameWorldHeight = screenHeight - sprint0.HUD.HudConstants.HudHeight;
            screenRectangle = new Rectangle(0, 0, screenWidth, gameWorldHeight);

            oldRoomTexture = new RenderTarget2D(graphicsDevice, screenWidth, gameWorldHeight);
            newRoomTexture = new RenderTarget2D(graphicsDevice, screenWidth, gameWorldHeight);

            capturedOldRoom = false;
            capturedNewRoom = false;
            middleInvoked = false;
        }

        public void StartTransition(Action onMiddle, TransitionDirection direction = TransitionDirection.None)
        {
            if (isTransitioning) return;

            isTransitioning = true;
            transitionProgress = 0f;
            onTransitionMiddle = onMiddle;
            slideDirection = direction;
            capturedOldRoom = false;
            capturedNewRoom = false;
            middleInvoked = false;

            if (direction == TransitionDirection.None)
            {
                transitionType = TransitionType.Fade;
                currentState = TransitionState.FadingOut;
            }
            else
            {
                transitionType = TransitionType.Push;
                currentState = TransitionState.Sliding;
            }
        }

        public void CaptureOldRoom(SpriteBatch spriteBatch, Action drawRoomAction)
        {
            if (capturedOldRoom || !isTransitioning) return;

            graphicsDevice.SetRenderTarget(oldRoomTexture);
            graphicsDevice.Clear(Color.Black);

            spriteBatch.Begin(samplerState: SamplerState.PointClamp);
            drawRoomAction?.Invoke();
            spriteBatch.End();

            graphicsDevice.SetRenderTarget(null);
            capturedOldRoom = true;
        }

        public void CaptureNewRoom(SpriteBatch spriteBatch, Action drawRoomAction)
        {
            if (capturedNewRoom || !isTransitioning) return;

            graphicsDevice.SetRenderTarget(newRoomTexture);
            graphicsDevice.Clear(Color.Black);

            spriteBatch.Begin(samplerState: SamplerState.PointClamp);
            drawRoomAction?.Invoke();
            spriteBatch.End();

            graphicsDevice.SetRenderTarget(null);
            capturedNewRoom = true;
        }

        public void Update(GameTime gameTime)
        {
            if (!isTransitioning) return;

            switch (currentState)
            {
                case TransitionState.FadingOut:
                    transitionProgress += transitionSpeed;
                    if (transitionProgress >= 1.0f)
                    {
                        transitionProgress = 1.0f;
                        onTransitionMiddle?.Invoke();
                        currentState = TransitionState.FadingIn;
                        transitionProgress = 0f;
                    }
                    break;

                case TransitionState.Sliding:
                    // Move the slide forward
                    transitionProgress += transitionSpeed * 1.5f;

                    // Call middle callback around halfway through the slide
                    if (!middleInvoked && transitionProgress >= 0.5f)
                    {
                        onTransitionMiddle?.Invoke();
                        middleInvoked = true;
                        capturedNewRoom = false; // ensure we recapture after room change
                    }

                    if (transitionProgress >= 1.0f)
                    {
                        transitionProgress = 1.0f;
                        isTransitioning = false;
                        currentState = TransitionState.None;
                        capturedOldRoom = false;
                        capturedNewRoom = false;
                        middleInvoked = false;
                    }
                    break;

                case TransitionState.FadingIn:
                    transitionProgress += transitionSpeed;
                    if (transitionProgress >= 1.0f)
                    {
                        transitionProgress = 1.0f;
                        isTransitioning = false;
                        currentState = TransitionState.None;
                    }
                    break;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (!isTransitioning && currentState == TransitionState.None) return;

            if (currentState == TransitionState.Sliding && transitionType == TransitionType.Push)
            {
                DrawPushTransition(spriteBatch);
            }
            else
            {
                DrawFadeTransition(spriteBatch);
            }
        }

        private void DrawFadeTransition(SpriteBatch spriteBatch)
        {
            float alpha = 0f;

            switch (currentState)
            {
                case TransitionState.FadingOut:
                    alpha = transitionProgress;
                    break;

                case TransitionState.FadingIn:
                    alpha = 1.0f - transitionProgress;
                    break;
            }

            Color fadeColor = Color.Black * alpha;
            spriteBatch.Draw(fadeTexture, screenRectangle, fadeColor);
        }

        private void DrawPushTransition(SpriteBatch spriteBatch)
        {
            if (!capturedOldRoom) return;

            float screenWidth = screenRectangle.Width;
            float screenHeight = screenRectangle.Height;

            Vector2 oldRoomOffset = Vector2.Zero;
            Vector2 newRoomOffset = Vector2.Zero;

            float distance = 0f;

            switch (slideDirection)
            {
                case TransitionDirection.North:
                    distance = screenHeight * transitionProgress;
                    oldRoomOffset = new Vector2(0, distance);
                    newRoomOffset = new Vector2(0, distance - screenHeight);
                    break;

                case TransitionDirection.South:
                    distance = screenHeight * transitionProgress;
                    oldRoomOffset = new Vector2(0, -distance);
                    newRoomOffset = new Vector2(0, screenHeight - distance);
                    break;

                case TransitionDirection.East:
                    distance = screenWidth * transitionProgress;
                    oldRoomOffset = new Vector2(-distance, 0);
                    newRoomOffset = new Vector2(screenWidth - distance, 0);
                    break;

                case TransitionDirection.West:
                    distance = screenWidth * transitionProgress;
                    oldRoomOffset = new Vector2(distance, 0);
                    newRoomOffset = new Vector2(distance - screenWidth, 0);
                    break;
            }

            if (oldRoomTexture != null)
            {
                spriteBatch.Draw(oldRoomTexture, oldRoomOffset, Color.White);
            }

            if (newRoomTexture != null && capturedNewRoom)
            {
                spriteBatch.Draw(newRoomTexture, newRoomOffset, Color.White);
            }
        }

        public bool NeedsRoomCapture()
        {
            return isTransitioning &&
                   currentState == TransitionState.Sliding &&
                   !capturedOldRoom;
        }

        public bool NeedsNewRoomCapture()
        {
            return isTransitioning &&
                   currentState == TransitionState.Sliding &&
                   middleInvoked &&
                   capturedOldRoom &&
                   !capturedNewRoom;
        }

        public void Reset()
        {
            isTransitioning = false;
            transitionProgress = 0f;
            currentState = TransitionState.None;
            capturedOldRoom = false;
            capturedNewRoom = false;
            middleInvoked = false;
        }
    }
}
