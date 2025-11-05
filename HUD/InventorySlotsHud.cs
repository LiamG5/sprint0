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

        public InventorySlotsHud(Func<Texture2D> getIconB, Func<Texture2D> getIconA, Vector2 aPos, Vector2 bPos)
        {
            this.getIconB = getIconB ?? (() => null);
            this.getIconA = getIconA ?? (() => null);
            this.aPos = aPos;
            this.bPos = bPos;

            slotBg = Texture2DStorage.GetTexture("hud_slot_bg");
        }

        public void Update(GameTime gameTime) { }

        public void Draw(SpriteBatch spriteBatch)
        {
            var bRect = new Rectangle((int)bPos.X, (int)bPos.Y, SlotSize.X, SlotSize.Y);
            var aRect = new Rectangle((int)aPos.X, (int)aPos.Y, SlotSize.X, SlotSize.Y);

            spriteBatch.Draw(slotBg, bRect, Color.Blue * 0.35f);
            spriteBatch.Draw(slotBg, aRect, Color.Blue * 0.35f);

            Stroke(spriteBatch, bRect, Color.White);
            Stroke(spriteBatch, aRect, Color.White);

            var bIcon = getIconB();
            var aIcon = getIconA();

            const int iconSize = 24;
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
            const int t = 2;
            sb.Draw(p, new Rectangle(r.X, r.Y, r.Width, t), color);
            sb.Draw(p, new Rectangle(r.X, r.Bottom - t, r.Width, t), color);
            sb.Draw(p, new Rectangle(r.X, r.Y, t, r.Height), color);
            sb.Draw(p, new Rectangle(r.Right - t, r.Y, t, r.Height), color);
        }
    }
}
