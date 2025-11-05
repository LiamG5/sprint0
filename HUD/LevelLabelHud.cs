using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace sprint0.HUD
{
    public class LevelLabelHud : IHudElement
    {
        private readonly Func<string> getText;
        private readonly SpriteFont font;
        private readonly Vector2 pos;
        private readonly Color color;

        public LevelLabelHud(Func<string> getText, SpriteFont font, Vector2 pos, Color? color = null)
        {
            this.getText = getText;
            this.font = font;
            this.pos = pos;
            this.color = color ?? Color.White;
        }

        public void Update(GameTime gameTime) { }
        public void Draw(SpriteBatch spriteBatch)
        {
            float scale = 1.2f;
            var t = getText();
            spriteBatch.DrawString(font, t, pos + new Vector2(1,1), Color.Black * 0.6f, 0f, Vector2.Zero, scale, SpriteEffects.None, 0f);
            spriteBatch.DrawString(font, t, pos, Color.OrangeRed, 0f, Vector2.Zero, scale, SpriteEffects.None, 0f);
        }
    }
}
