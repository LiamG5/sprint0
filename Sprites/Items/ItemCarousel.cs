using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
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
            ItemFactory.ItemType.TriforceFragment,
            
            
            // Miscellaneous
            ItemFactory.ItemType.RecoveryHeart,
            ItemFactory.ItemType.HeartContainer,
            ItemFactory.ItemType.Clock,
            ItemFactory.ItemType.RupeeRed,
            ItemFactory.ItemType.RupeeBlue,
            ItemFactory.ItemType.Rupee,
            ItemFactory.ItemType.Fairy
        };

        public ItemCarousel(ItemFactory itemFactory ,SpriteBatch spriteBatch)
        {
            this.itemFactory = itemFactory;
            this.spriteBatch = spriteBatch;
            currentItem = itemFactory.BuildSword();
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
                    currentItem = itemFactory.BuildSword();
                    break;
                case ItemFactory.ItemType.WhiteSword:
                    currentItem = itemFactory.BuildWhiteSword();
                    break;
                case ItemFactory.ItemType.Boomerang:
                    currentItem = itemFactory.BuildBoomerang();
                    break;
                case ItemFactory.ItemType.MagicalBoomerang:
                    currentItem = itemFactory.BuildMagicalBoomerang();
                    break;
                case ItemFactory.ItemType.Bomb:
                    currentItem = itemFactory.BuildBomb();
                    break;
                case ItemFactory.ItemType.Bow:
                    currentItem = itemFactory.BuildBow();
                    break;
                case ItemFactory.ItemType.Arrow:
                    currentItem = itemFactory.BuildArrow();
                    break;
                case ItemFactory.ItemType.SilverArrow:
                    currentItem = itemFactory.BuildSilverArrow();
                    break;
                case ItemFactory.ItemType.CandleRed:
                    currentItem = itemFactory.BuildCandleRed();
                    break;
                case ItemFactory.ItemType.CandleBlue:
                    currentItem = itemFactory.BuildCandleBlue();
                    break;
                case ItemFactory.ItemType.Recorder:
                    currentItem = itemFactory.BuildRecorder();
                    break;
                case ItemFactory.ItemType.Food:
                    currentItem = itemFactory.BuildFood();
                    break;
                case ItemFactory.ItemType.Letter:
                    currentItem = itemFactory.BuildLetter();
                    break;
                case ItemFactory.ItemType.PotionRed:
                    currentItem = itemFactory.BuildPotionRed();
                    break;
                case ItemFactory.ItemType.PotionBlue:
                    currentItem = itemFactory.BuildPotionBlue();
                    break;
                case ItemFactory.ItemType.MagicalRod:
                    currentItem = itemFactory.BuildMagicalRod();
                    break;
                
                // Inventory
                case ItemFactory.ItemType.Raft:
                    currentItem = itemFactory.BuildRaft();
                    break;
                case ItemFactory.ItemType.BookOfMagic:
                    currentItem = itemFactory.BuildBookOfMagic();
                    break;
                case ItemFactory.ItemType.BlueRing:
                    currentItem = itemFactory.BuildBlueRing();
                    break;
                case ItemFactory.ItemType.RedRing:
                    currentItem = itemFactory.BuildRedRing();
                    break;
                case ItemFactory.ItemType.Stepladder:
                    currentItem = itemFactory.BuildStepladder();
                    break;
                case ItemFactory.ItemType.MagicalKey:
                    currentItem = itemFactory.BuildMagicalKey();
                    break;
                case ItemFactory.ItemType.PowerBracelet:
                    currentItem = itemFactory.BuildPowerBracelet();
                    break;
                
                // Dungeon
                case ItemFactory.ItemType.Compass:
                    currentItem = itemFactory.BuildCompass();
                    break;
                case ItemFactory.ItemType.DungeonMap:
                    currentItem = itemFactory.BuildDungeonMap();
                    break;
                case ItemFactory.ItemType.SmallKey:
                    currentItem = itemFactory.BuildSmallKey();
                    break;
                case ItemFactory.ItemType.TriforceFragment:
                    currentItem = itemFactory.BuildTriforceFragment();
                    break;
                // Miscellaneous
                case ItemFactory.ItemType.RecoveryHeart:
                    currentItem = itemFactory.BuildRecoveryHeart();
                    break;
                case ItemFactory.ItemType.HeartContainer:
                    currentItem = itemFactory.BuildHeartContainer();
                    break;
                case ItemFactory.ItemType.Clock:
                    currentItem = itemFactory.BuildClock();
                    break;
                case ItemFactory.ItemType.RupeeRed:
                    currentItem = itemFactory.BuildRupeeRed();
                    break;
                case ItemFactory.ItemType.RupeeBlue:
                    currentItem = itemFactory.BuildRupeeBlue();
                    break;
                case ItemFactory.ItemType.Rupee:
                    currentItem = itemFactory.BuildRupee();
                    break;
                case ItemFactory.ItemType.Fairy:
                    currentItem = itemFactory.BuildFairy();
                    break;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        { 
            currentItem.Draw(spriteBatch, new Vector2(200, 100));
        }

        
    }
}