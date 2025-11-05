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
            spriteBatch.Draw(icon, new Rectangle((int)pos.X, (int)pos.Y, 12, 12), Color.White);
            var textPos = new Vector2(pos.X + 18, pos.Y - 2);
            spriteBatch.DrawString(font, "x" + getValue(), textPos, Color.White);
        }
    }
}
