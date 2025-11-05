using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using sprint0.Sprites;

namespace sprint0.HUD
{
    public class HeartMeter : IHudElement
    {
        private readonly Func<int> getHearts;
        private readonly Func<int> getMaxHearts;
        private readonly Vector2 pos;
        private readonly Texture2D fullHeart;
        private readonly Texture2D emptyHeart;

        public HeartMeter(Func<int> getHearts, Func<int> getMaxHearts, Vector2 position)
        {
            this.getHearts = getHearts;
            this.getMaxHearts = getMaxHearts;
            pos = position;
            fullHeart = Texture2DStorage.GetTexture("hud_heart_full");
            emptyHeart = Texture2DStorage.GetTexture("hud_heart_empty");
        }

        public void Update(GameTime gameTime) { }

        public void Draw(SpriteBatch spriteBatch)
        {
            int filled = Math.Max(0, getHearts());
            int max    = Math.Max(0, getMaxHearts());

            var cursor = pos;
            for (int i = 0; i < max; i++)
            {
                var size = new Rectangle((int)cursor.X, (int)cursor.Y, 18, 18);
                var tex  = (i < filled) ? fullHeart : emptyHeart;
                var tint = (i < filled) ? Color.Red : Color.DarkGray;
                spriteBatch.Draw(tex, size, tint);
                cursor.X += HudConstants.HeartSpacing;
            }
        }
    }
}
