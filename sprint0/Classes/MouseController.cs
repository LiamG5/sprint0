using sprint0.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;


namespace sprint0.Classes

{
    public class MouseController : IController
    {
        private MouseState previousState;
        private Game1 game;
        private MarioSprite marioSprite; 

        public MouseController(Game1 game, MarioSprite marioSprite)
        {
            this.game = game;
            this.marioSprite = marioSprite;
        }

        public void Update()
        {
            MouseState currentState = Mouse.GetState();
            int width = game.GraphicsDevice.Viewport.Width;
            int height = game.GraphicsDevice.Viewport.Height;

            // left click
            if (currentState.LeftButton == ButtonState.Pressed && previousState.LeftButton == ButtonState.Released)
            {
                Vector2 position = new Vector2(currentState.X, currentState.Y);
                if (position.X < width / 2 && position.Y < height / 2)
                {
                marioSprite.state = 1;
                }
                else if (position.X >= width / 2 && position.Y < height / 2)
                {
                    marioSprite.state = 2;
                }
                else if (position.X < width / 2 && position.Y >= height / 2)
                {
                marioSprite.state = 3;
                }
                else
                {
                marioSprite.state = 4;
                }
            }

            // Right click
            if (currentState.RightButton == ButtonState.Pressed && previousState.RightButton == ButtonState.Released)
            {
                game.Exit();
            }

            previousState = currentState;
        }
    }
}
