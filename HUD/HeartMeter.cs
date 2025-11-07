using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using sprint0.Sprites;
using static sprint0.HUD.HudConstants;

namespace sprint0.HUD
{
    public class HeartMeter : IHudElement
    {
        private readonly Func<int> getHearts;
        private readonly Func<int> getMaxHearts;
        private readonly Vector2 pos;
        private readonly Texture2D fullHeart;
        private readonly Texture2D emptyHeart;
        private readonly SpriteFont font;
        private readonly Vector2 labelPos;

        public HeartMeter(Func<int> getHearts, Func<int> getMaxHearts, Vector2 position, SpriteFont font)
        {
            this.getHearts = getHearts;
            this.getMaxHearts = getMaxHearts;
            this.font = font;
            pos = position; // Position for hearts
            labelPos = LifeLabelPos; // Use the constant label position to avoid overlap
            fullHeart = Texture2DStorage.GetTexture("hud_heart_full");
            emptyHeart = Texture2DStorage.GetTexture("hud_heart_empty");
        }

        public void Update(GameTime gameTime) { }

        public void Draw(SpriteBatch spriteBatch)
        {
            // Draw "-LIFE-" label
            if (font != null)
            {
                float scale = 1.3f; // Increased from 1.0f for bigger text
                string label = "-LIFE-";
                spriteBatch.DrawString(font, label, labelPos + new Vector2(1, 1), Color.Black * 0.6f, 0f, Vector2.Zero, scale, SpriteEffects.None, 0f);
                spriteBatch.DrawString(font, label, labelPos, Color.OrangeRed, 0f, Vector2.Zero, scale, SpriteEffects.None, 0f);
            }

            int filled = Math.Max(0, getHearts());
            int max    = Math.Max(0, getMaxHearts());

            // Need textures to draw hearts
            if (fullHeart == null || emptyHeart == null) return;

            var cursor = pos;
            for (int i = 0; i < max; i++)
            {
                var size = new Rectangle((int)cursor.X, (int)cursor.Y, 22, 22); // Increased from 18x18 to 22x22
                var tex  = (i < filled) ? fullHeart : emptyHeart;
                var tint = (i < filled) ? Color.Red : Color.DarkGray;
                spriteBatch.Draw(tex, size, tint);
                cursor.X += HudConstants.HeartSpacing;
            }
        }
    }
}
