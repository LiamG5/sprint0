using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using sprint0.Sprites;
using sprint0.Interfaces;
using static sprint0.HUD.HudConstants;
using static sprint0.Factories.ItemFactory;

namespace sprint0.HUD
{
    public class InventorySlotsHud : IHudElement
    {
        private readonly Func<ItemType?> getItemB;
        private readonly Func<ItemType?> getItemA;
        private readonly Texture2D itemSpriteSheet;

        private readonly Vector2 aPos;
        private readonly Vector2 bPos;

        private readonly Texture2D slotBg;
        private readonly SpriteFont font;

        public InventorySlotsHud(Func<ItemType?> getItemB, Func<ItemType?> getItemA, Vector2 aPos, Vector2 bPos, SpriteFont font = null)
        {
            this.getItemB = getItemB ?? (() => null);
            this.getItemA = getItemA ?? (() => null);
            this.aPos = aPos;
            this.bPos = bPos;
            this.font = font;

            slotBg = Texture2DStorage.GetTexture("hud_slot_bg");
            itemSpriteSheet = Texture2DStorage.GetItemSpriteSheet();
        }

        public void Update(GameTime gameTime) { }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (slotBg == null) return;
            
            var bRect = new Rectangle((int)bPos.X, (int)bPos.Y, SlotSize.X, SlotSize.Y);
            var aRect = new Rectangle((int)aPos.X, (int)aPos.Y, SlotSize.X, SlotSize.Y);

            if (font != null)
            {
                float labelScale = 1.1f;
                Vector2 bLabelPos = new Vector2(bRect.X + (bRect.Width - font.MeasureString("B").X * labelScale) / 2, bRect.Y - 25);
                Vector2 aLabelPos = new Vector2(aRect.X + (aRect.Width - font.MeasureString("A").X * labelScale) / 2, aRect.Y - 25);
                
                spriteBatch.DrawString(font, "B", bLabelPos, Color.White, 0f, Vector2.Zero, labelScale, SpriteEffects.None, 0f);
                spriteBatch.DrawString(font, "A", aLabelPos, Color.White, 0f, Vector2.Zero, labelScale, SpriteEffects.None, 0f);
            }

            Stroke(spriteBatch, bRect, new Color(0, 200, 255));
            Stroke(spriteBatch, aRect, new Color(0, 200, 255));

            var itemB = getItemB();
            var itemA = getItemA();

            if (itemB.HasValue && itemSpriteSheet != null)
            {
                var sourceRect = GetItemSourceRect(itemB.Value);
                if (sourceRect.HasValue)
                {
                    float scale = 2.0f;
                    var itemPos = new Vector2(bRect.X + (bRect.Width - sourceRect.Value.Width * scale) / 2, bRect.Y + (bRect.Height - sourceRect.Value.Height * scale) / 2);
                    spriteBatch.Draw(itemSpriteSheet, itemPos, sourceRect.Value, Color.White, 0f, Vector2.Zero, scale, SpriteEffects.None, 0f);
                }
            }

            if (itemA.HasValue && itemSpriteSheet != null)
            {
                var sourceRect = GetItemSourceRect(itemA.Value);
                if (sourceRect.HasValue)
                {
                    float scale = 2.0f;
                    var itemPos = new Vector2(aRect.X + (aRect.Width - sourceRect.Value.Width * scale) / 2, aRect.Y + (aRect.Height - sourceRect.Value.Height * scale) / 2);
                    spriteBatch.Draw(itemSpriteSheet, itemPos, sourceRect.Value, Color.White, 0f, Vector2.Zero, scale, SpriteEffects.None, 0f);
                }
            }
        }
        
        private Rectangle? GetItemSourceRect(ItemType itemType)
        {
            return itemType switch
            {
                ItemType.RupeeRed => new Rectangle(40 * 4, 40 * 3, 15, 16),
                ItemType.RupeeBlue => new Rectangle(40 * 5, 40 * 3, 15, 16),
                ItemType.SmallKey => new Rectangle(40 * 7, 40 * 1, 15, 16),
                ItemType.Bomb => new Rectangle(40 * 5, 40 * 0, 15, 16),
                ItemType.Boomerang => new Rectangle(40 * 7, 40 * 0, 15, 16),
                ItemType.Bow => new Rectangle(40 * 0, 40 * 1, 15, 16),
                ItemType.Arrow => new Rectangle(40 * 1, 40 * 1, 15, 16),
                ItemType.CandleRed => new Rectangle(40 * 2, 40 * 1, 15, 16),
                ItemType.CandleBlue => new Rectangle(40 * 3, 40 * 1, 15, 16),
                ItemType.Recorder => new Rectangle(360, 40 * 2, 13, 16),
                ItemType.Food => new Rectangle(40 * 5, 40 * 1, 15, 16),
                ItemType.PotionRed => new Rectangle(40 * 6, 40 * 1, 15, 16),
                ItemType.PotionBlue => new Rectangle(40 * 7, 40 * 2, 15, 16),
                ItemType.MagicalRod => new Rectangle(40 * 0, 40 * 2, 15, 16),
                ItemType.Sword => new Rectangle(40 * 7, 40 * 3, 15, 16),
                ItemType.WhiteSword => new Rectangle(40 * 0, 40 * 3, 15, 16),
                ItemType.MagicalBoomerang => new Rectangle(40 * 1, 40 * 3, 15, 16),
                ItemType.SilverArrow => new Rectangle(40 * 2, 40 * 3, 15, 16),
                ItemType.Letter => new Rectangle(40 * 6, 40 * 2, 15, 16),
                ItemType.Raft => new Rectangle(40 * 3, 40 * 2, 15, 16),
                ItemType.BookOfMagic => new Rectangle(40 * 4, 40 * 2, 15, 16),
                ItemType.BlueRing => new Rectangle(40 * 5, 40 * 2, 15, 16),
                ItemType.RedRing => new Rectangle(40 * 6, 40 * 2, 15, 16),
                ItemType.Stepladder => new Rectangle(40 * 1, 40 * 2, 15, 16),
                ItemType.MagicalKey => new Rectangle(40 * 2, 40 * 2, 15, 16),
                ItemType.PowerBracelet => new Rectangle(40 * 4, 40 * 1, 15, 16),
                ItemType.Compass => new Rectangle(40 * 0, 40 * 0, 15, 16),
                ItemType.DungeonMap => new Rectangle(40 * 1, 40 * 0, 15, 16),
                ItemType.TriforceFragment => new Rectangle(40 * 2, 40 * 0, 15, 16),
                ItemType.RecoveryHeart => new Rectangle(40 * 6, 40 * 1, 15, 16),
                ItemType.HeartContainer => new Rectangle(40 * 7, 40 * 1, 15, 16),
                ItemType.Clock => new Rectangle(40 * 3, 40 * 0, 15, 16),
                ItemType.Fairy => new Rectangle(40 * 3, 40 * 1, 15, 16),
                _ => null
            };
        }
        
        private IItem CreateItem(ItemType itemType)
        {
            if (itemSpriteSheet == null) return null;
            
            return itemType switch
            {
                ItemType.Boomerang => new ItemBoomerang(itemSpriteSheet),
                ItemType.Bomb => new ItemBomb(itemSpriteSheet),
                ItemType.Bow => new ItemBow(itemSpriteSheet),
                ItemType.Arrow => new ItemArrow(itemSpriteSheet),
                ItemType.CandleRed => new ItemCandleRed(itemSpriteSheet),
                ItemType.CandleBlue => new ItemCandleBlue(itemSpriteSheet),
                ItemType.Recorder => new ItemRecorder(itemSpriteSheet),
                ItemType.Food => new ItemFood(itemSpriteSheet),
                ItemType.PotionRed => new ItemPotionRed(itemSpriteSheet),
                ItemType.PotionBlue => new ItemPotionBlue(itemSpriteSheet),
                ItemType.MagicalRod => new ItemMagicalRod(itemSpriteSheet),
                ItemType.Sword => new ItemSword(itemSpriteSheet),
                ItemType.WhiteSword => new ItemWhiteSword(itemSpriteSheet),
                ItemType.MagicalBoomerang => new ItemMagicalBoomerang(itemSpriteSheet),
                ItemType.SilverArrow => new ItemSilverArrow(itemSpriteSheet),
                ItemType.Letter => new ItemLetter(itemSpriteSheet),
                ItemType.Raft => new ItemRaft(itemSpriteSheet),
                ItemType.BookOfMagic => new ItemBookOfMagic(itemSpriteSheet),
                ItemType.BlueRing => new ItemBlueRing(itemSpriteSheet),
                ItemType.RedRing => new ItemRedRing(itemSpriteSheet),
                ItemType.Stepladder => new ItemStepladder(itemSpriteSheet),
                ItemType.MagicalKey => new ItemMagicalKey(itemSpriteSheet),
                ItemType.PowerBracelet => new ItemPowerBracelet(itemSpriteSheet),
                ItemType.Compass => new ItemCompass(itemSpriteSheet),
                ItemType.DungeonMap => new ItemDungeonMap(itemSpriteSheet),
                ItemType.SmallKey => new ItemSmallKey(itemSpriteSheet),
                ItemType.TriforceFragment => new ItemTriforceFragment(itemSpriteSheet),
                ItemType.RecoveryHeart => new ItemRecoveryHeart(itemSpriteSheet),
                ItemType.HeartContainer => new ItemHeartContainer(itemSpriteSheet),
                ItemType.Clock => new ItemClock(itemSpriteSheet),
                ItemType.RupeeRed => new ItemRupeeRed(itemSpriteSheet),
                ItemType.RupeeBlue => new ItemRupeeBlue(itemSpriteSheet),
                ItemType.Fairy => new ItemFairy(itemSpriteSheet),
                _ => null
            };
        }

        private static void Stroke(SpriteBatch sb, Rectangle r, Color color)
        {
            var p = Texture2DStorage.GetTexture("hud_slot_bg");
            if (p == null) return;
            const int t = 2;
            sb.Draw(p, new Rectangle(r.X, r.Y, r.Width, t), color);
            sb.Draw(p, new Rectangle(r.X, r.Bottom - t, r.Width, t), color);
            sb.Draw(p, new Rectangle(r.X, r.Y, t, r.Height), color);
            sb.Draw(p, new Rectangle(r.Right - t, r.Y, t, r.Height), color);
        }
    }
}
