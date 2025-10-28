using Microsoft.Xna.Framework.Graphics;
using sprint0.Interfaces;
using sprint0.Sprites;

namespace sprint0.Sprites
{
    public class ItemCarousel : ICarousel
    {
        private ItemFactory itemFactory;
        private SpriteBatch spriteBatch;
        private ISprite currentItem;
        private int currentIndex = 0;
        private ItemFactory.ItemType[] itemTypes = {
            // Equipment
            ItemFactory.ItemType.Sword,
            ItemFactory.ItemType.WhiteSword,
            ItemFactory.ItemType.Boomerang,
            ItemFactory.ItemType.MagicalBoomerang,
            ItemFactory.ItemType.Bomb,
            ItemFactory.ItemType.Bow,
            ItemFactory.ItemType.Arrow,
            ItemFactory.ItemType.SilverArrow,
            ItemFactory.ItemType.CandleRed,
            ItemFactory.ItemType.CandleBlue,
            ItemFactory.ItemType.Recorder,
            ItemFactory.ItemType.Food,
            ItemFactory.ItemType.Letter,
            ItemFactory.ItemType.PotionRed,
            ItemFactory.ItemType.PotionBlue,
            ItemFactory.ItemType.MagicalRod,
            
            // Inventory
            ItemFactory.ItemType.Raft,
            ItemFactory.ItemType.BookOfMagic,
            ItemFactory.ItemType.BlueRing,
            ItemFactory.ItemType.RedRing,
            ItemFactory.ItemType.Stepladder,
            ItemFactory.ItemType.MagicalKey,
            ItemFactory.ItemType.PowerBracelet,
            
            // Dungeon
            ItemFactory.ItemType.Compass,
            ItemFactory.ItemType.DungeonMap,
            ItemFactory.ItemType.SmallKey,
            ItemFactory.ItemType.TriforceFragmentBlue,
            ItemFactory.ItemType.TriforceFragmentYellow,
            
            // Miscellaneous
            ItemFactory.ItemType.RecoveryHeart,
            ItemFactory.ItemType.HeartContainer,
            ItemFactory.ItemType.Clock,
            ItemFactory.ItemType.RupeeRed,
            ItemFactory.ItemType.RupeeBlue,
            ItemFactory.ItemType.Fairy
        };

        public ItemCarousel(ItemFactory itemFactory ,SpriteBatch spriteBatch)
        {
            this.itemFactory = itemFactory;
            this.spriteBatch = spriteBatch;
            currentItem = itemFactory.BuildSword(spriteBatch);
        }

        public void Next()
        {
            currentIndex = (currentIndex + 1) % itemTypes.Length;
            UpdateCurrentItem();
        }

        public void Prev()
        {
            currentIndex = (currentIndex - 1 + itemTypes.Length) % itemTypes.Length;
            UpdateCurrentItem();
        }

        private void UpdateCurrentItem()
        {
            switch (itemTypes[currentIndex])
            {
                // Equipment
                case ItemFactory.ItemType.Sword:
                    currentItem = itemFactory.BuildSword(spriteBatch);
                    break;
                case ItemFactory.ItemType.WhiteSword:
                    currentItem = itemFactory.BuildWhiteSword(spriteBatch);
                    break;
                case ItemFactory.ItemType.Boomerang:
                    currentItem = itemFactory.BuildBoomerang(spriteBatch);
                    break;
                case ItemFactory.ItemType.MagicalBoomerang:
                    currentItem = itemFactory.BuildMagicalBoomerang(spriteBatch);
                    break;
                case ItemFactory.ItemType.Bomb:
                    currentItem = itemFactory.BuildBomb(spriteBatch);
                    break;
                case ItemFactory.ItemType.Bow:
                    currentItem = itemFactory.BuildBow(spriteBatch);
                    break;
                case ItemFactory.ItemType.Arrow:
                    currentItem = itemFactory.BuildArrow(spriteBatch);
                    break;
                case ItemFactory.ItemType.SilverArrow:
                    currentItem = itemFactory.BuildSilverArrow(spriteBatch);
                    break;
                case ItemFactory.ItemType.CandleRed:
                    currentItem = itemFactory.BuildCandleRed(spriteBatch);
                    break;
                case ItemFactory.ItemType.CandleBlue:
                    currentItem = itemFactory.BuildCandleBlue(spriteBatch);
                    break;
                case ItemFactory.ItemType.Recorder:
                    currentItem = itemFactory.BuildRecorder(spriteBatch);
                    break;
                case ItemFactory.ItemType.Food:
                    currentItem = itemFactory.BuildFood(spriteBatch);
                    break;
                case ItemFactory.ItemType.Letter:
                    currentItem = itemFactory.BuildLetter(spriteBatch);
                    break;
                case ItemFactory.ItemType.PotionRed:
                    currentItem = itemFactory.BuildPotionRed(spriteBatch);
                    break;
                case ItemFactory.ItemType.PotionBlue:
                    currentItem = itemFactory.BuildPotionBlue(spriteBatch);
                    break;
                case ItemFactory.ItemType.MagicalRod:
                    currentItem = itemFactory.BuildMagicalRod(spriteBatch);
                    break;
                
                // Inventory
                case ItemFactory.ItemType.Raft:
                    currentItem = itemFactory.BuildRaft(spriteBatch);
                    break;
                case ItemFactory.ItemType.BookOfMagic:
                    currentItem = itemFactory.BuildBookOfMagic(spriteBatch);
                    break;
                case ItemFactory.ItemType.BlueRing:
                    currentItem = itemFactory.BuildBlueRing(spriteBatch);
                    break;
                case ItemFactory.ItemType.RedRing:
                    currentItem = itemFactory.BuildRedRing(spriteBatch);
                    break;
                case ItemFactory.ItemType.Stepladder:
                    currentItem = itemFactory.BuildStepladder(spriteBatch);
                    break;
                case ItemFactory.ItemType.MagicalKey:
                    currentItem = itemFactory.BuildMagicalKey(spriteBatch);
                    break;
                case ItemFactory.ItemType.PowerBracelet:
                    currentItem = itemFactory.BuildPowerBracelet(spriteBatch);
                    break;
                
                // Dungeon
                case ItemFactory.ItemType.Compass:
                    currentItem = itemFactory.BuildCompass(spriteBatch);
                    break;
                case ItemFactory.ItemType.DungeonMap:
                    currentItem = itemFactory.BuildDungeonMap(spriteBatch);
                    break;
                case ItemFactory.ItemType.SmallKey:
                    currentItem = itemFactory.BuildSmallKey(spriteBatch);
                    break;
                case ItemFactory.ItemType.TriforceFragmentBlue:
                    currentItem = itemFactory.BuildTriforceFragmentBlue(spriteBatch);
                    break;
                case ItemFactory.ItemType.TriforceFragmentYellow:
                    currentItem = itemFactory.BuildTriforceFragmentYellow(spriteBatch);
                    break;
                
                // Miscellaneous
                case ItemFactory.ItemType.RecoveryHeart:
                    currentItem = itemFactory.BuildRecoveryHeart(spriteBatch);
                    break;
                case ItemFactory.ItemType.HeartContainer:
                    currentItem = itemFactory.BuildHeartContainer(spriteBatch);
                    break;
                case ItemFactory.ItemType.Clock:
                    currentItem = itemFactory.BuildClock(spriteBatch);
                    break;
                case ItemFactory.ItemType.RupeeRed:
                    currentItem = itemFactory.BuildRupeeRed(spriteBatch);
                    break;
                case ItemFactory.ItemType.RupeeBlue:
                    currentItem = itemFactory.BuildRupeeBlue(spriteBatch);
                    break;
                case ItemFactory.ItemType.Fairy:
                    currentItem = itemFactory.BuildFairy(spriteBatch);
                    break;
            }
        }

        public ISprite GetCurrentItem()
        {
            return currentItem;
        }

        
    }
}