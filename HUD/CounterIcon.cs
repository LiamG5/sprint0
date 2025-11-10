using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace sprint0.HUD
{
    public class CounterIcon : IHudElement
    {
        private readonly Texture2D icon;
        private readonly Func<int> getValue;
        private readonly SpriteFont font;
        private readonly Vector2 pos;

        public CounterIcon(Texture2D icon, Func<int> getValue, SpriteFont font, Vector2 position)
        {
            this.icon = icon;
            this.getValue = getValue;
            this.font = font;
            this.pos = position;
        }

        public void Update(GameTime gameTime) { }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (icon == null) return;
            
            // Increased icon size from 12x12 to 16x16
            spriteBatch.Draw(icon, new Rectangle((int)pos.X, (int)pos.Y, 16, 16), Color.White);
            var textPos = new Vector2(pos.X + 22, pos.Y - 4); // Moved up a little (from -1 to -4)
            float textScale = 1.2f; // Increased text size
            spriteBatch.DrawString(font, "x" + getValue(), textPos, Color.White, 0f, Vector2.Zero, textScale, SpriteEffects.None, 0f);
        }
    }
}
