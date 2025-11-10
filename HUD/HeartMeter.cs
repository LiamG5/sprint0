using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using sprint0.Sprites;
using sprint0.Interfaces;
using static sprint0.HUD.HudConstants;

namespace sprint0.HUD
{
    public class HeartMeter : IHudElement
    {
        private readonly Func<int> getHearts;
        private readonly Func<int> getMaxHearts;
        private readonly Vector2 pos;
        private readonly IItem fullHeartItem;
        private readonly IItem emptyHeartItem;
        private readonly SpriteFont font;
        private readonly Vector2 labelPos;

        public HeartMeter(Func<int> getHearts, Func<int> getMaxHearts, Vector2 position, SpriteFont font)
        {
            this.getHearts = getHearts;
            this.getMaxHearts = getMaxHearts;
            this.font = font;
            pos = position;
            labelPos = LifeLabelPos;
            
            var itemSpriteSheet = Texture2DStorage.GetItemSpriteSheet();
            if (itemSpriteSheet != null)
            {
                fullHeartItem = new ItemRecoveryHeart(itemSpriteSheet);
                emptyHeartItem = new ItemEmptyHeart(itemSpriteSheet);
            }
        }

        public void Update(GameTime gameTime) { }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (font != null)
            {
                float scale = 1.3f;
                string label = "-LIFE-";
                spriteBatch.DrawString(font, label, labelPos + new Vector2(1, 1), Color.Black * 0.6f, 0f, Vector2.Zero, scale, SpriteEffects.None, 0f);
                spriteBatch.DrawString(font, label, labelPos, Color.OrangeRed, 0f, Vector2.Zero, scale, SpriteEffects.None, 0f);
            }

            int filled = Math.Max(0, getHearts());
            int max    = Math.Max(0, getMaxHearts());

            if (fullHeartItem == null || emptyHeartItem == null) return;

            var cursor = pos;
            for (int i = 0; i < max; i++)
            {
                var heartItem = (i < filled) ? fullHeartItem : emptyHeartItem;
                heartItem.Draw(spriteBatch, cursor);
                cursor.X += HudConstants.HeartSpacing;
            }
        }
    }
}
