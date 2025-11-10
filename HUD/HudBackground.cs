using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace sprint0.HUD
{
    public class HudBackground : IHudElement
    {
        private readonly Rectangle rect;
        private readonly Texture2D pixelTexture;

        public HudBackground(int width, int height, GraphicsDevice graphicsDevice)
        {
            rect = new Rectangle(0, 0, width, height);
            
            // Create a simple pixel texture for drawing
            pixelTexture = new Texture2D(graphicsDevice, 1, 1);
            pixelTexture.SetData(new[] { Color.White });
        }

        public void Update(GameTime gameTime) { }

        public void Draw(SpriteBatch spriteBatch)
        {
            // Draw black background for HUD box
            spriteBatch.Draw(pixelTexture, rect, Color.Black);
        }
    }
}

