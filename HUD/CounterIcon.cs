using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using sprint0.Sprites;
using sprint0.Interfaces;
using static sprint0.Sprites.ItemFactory;

namespace sprint0.HUD
{
    public class CounterIcon : IHudElement
    {
        private readonly ItemType itemType;
        private readonly Func<int> getValue;
        private readonly SpriteFont font;
        private readonly Vector2 pos;
        private readonly Texture2D itemSpriteSheet;
        private readonly IItem itemInstance;

        public CounterIcon(ItemType itemType, Func<int> getValue, SpriteFont font, Vector2 position)
        {
            this.itemType = itemType;
            this.getValue = getValue;
            this.font = font;
            this.pos = position;
            
            itemSpriteSheet = Texture2DStorage.GetItemSpriteSheet();
            itemInstance = CreateItem(itemType);
        }

        public void Update(GameTime gameTime) { }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (itemSpriteSheet != null && itemInstance != null)
            {
                var sourceRect = GetItemSourceRect(itemType);
                if (sourceRect.HasValue)
                {
                    float scale = 1.5f;
                    spriteBatch.Draw(itemSpriteSheet, pos, sourceRect.Value, Color.White, 0f, Vector2.Zero, scale, SpriteEffects.None, 0f);
                }
            }
            
            var textPos = new Vector2(pos.X + 20, pos.Y - 2);
            float textScale = 1.2f;
            spriteBatch.DrawString(font, "x" + getValue(), textPos, Color.White, 0f, Vector2.Zero, textScale, SpriteEffects.None, 0f);
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
    }
}
