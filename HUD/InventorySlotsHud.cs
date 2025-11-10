using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using sprint0.Sprites;
using static sprint0.HUD.HudConstants;

namespace sprint0.HUD
{
    public class InventorySlotsHud : IHudElement
    {
        private readonly Func<Texture2D> getIconB;
        private readonly Func<Texture2D> getIconA;

        private readonly Vector2 aPos;
        private readonly Vector2 bPos;

        private readonly Texture2D slotBg;
        private readonly SpriteFont font;

        public InventorySlotsHud(Func<Texture2D> getIconB, Func<Texture2D> getIconA, Vector2 aPos, Vector2 bPos, SpriteFont font = null)
        {
            this.getIconB = getIconB ?? (() => null);
            this.getIconA = getIconA ?? (() => null);
            this.aPos = aPos;
            this.bPos = bPos;
            this.font = font;

            slotBg = Texture2DStorage.GetTexture("hud_slot_bg");
        }

        public void Update(GameTime gameTime) { }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (slotBg == null) return; // Can't draw without background texture
            
            var bRect = new Rectangle((int)bPos.X, (int)bPos.Y, SlotSize.X, SlotSize.Y);
            var aRect = new Rectangle((int)aPos.X, (int)aPos.Y, SlotSize.X, SlotSize.Y);

            // Draw "B" and "A" labels above the slots
            if (font != null)
            {
                float labelScale = 1.1f;
                Vector2 bLabelPos = new Vector2(bRect.X + (bRect.Width - font.MeasureString("B").X * labelScale) / 2, bRect.Y - 16);
                Vector2 aLabelPos = new Vector2(aRect.X + (aRect.Width - font.MeasureString("A").X * labelScale) / 2, aRect.Y - 16);
                
                spriteBatch.DrawString(font, "B", bLabelPos, Color.White, 0f, Vector2.Zero, labelScale, SpriteEffects.None, 0f);
                spriteBatch.DrawString(font, "A", aLabelPos, Color.White, 0f, Vector2.Zero, labelScale, SpriteEffects.None, 0f);
            }

            spriteBatch.Draw(slotBg, bRect, Color.Blue * 0.35f);
            spriteBatch.Draw(slotBg, aRect, Color.Blue * 0.35f);

            Stroke(spriteBatch, bRect, Color.White);
            Stroke(spriteBatch, aRect, Color.White);

            var bIcon = getIconB();
            var aIcon = getIconA();

            const int iconSize = 28; // Increased from 24 to match bigger slots
            if (bIcon != null)
            {
                var dst = new Rectangle(bRect.X + (bRect.Width - iconSize) / 2, bRect.Y + (bRect.Height - iconSize) / 2, iconSize, iconSize);
                spriteBatch.Draw(bIcon, dst, Color.White);
            }

            if (aIcon != null)
            {
                var dst = new Rectangle(aRect.X + (aRect.Width - iconSize) / 2, aRect.Y + (aRect.Height - iconSize) / 2, iconSize, iconSize);
                spriteBatch.Draw(aIcon, dst, Color.White);
            }
        }

        private static void Stroke(SpriteBatch sb, Rectangle r, Color color)
        {
            var p = Texture2DStorage.GetTexture("hud_slot_bg");
            if (p == null) return; // Can't draw without texture
            const int t = 2;
            sb.Draw(p, new Rectangle(r.X, r.Y, r.Width, t), color);
            sb.Draw(p, new Rectangle(r.X, r.Bottom - t, r.Width, t), color);
            sb.Draw(p, new Rectangle(r.X, r.Y, t, r.Height), color);
            sb.Draw(p, new Rectangle(r.Right - t, r.Y, t, r.Height), color);
        }
    }
}
