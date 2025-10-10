using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using sprint0.Classes;
using sprint0.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace sprint0.Sprites
{
    public class ItemFactory 
    {
        public enum ItemType
        {
            // Equipment
            Sword, WhiteSword, Boomerang, MagicalBoomerang, Bomb, Bow,
            Arrow, SilverArrow,
            CandleRed, CandleBlue, Recorder, Food, Letter, PotionRed, PotionBlue, MagicalRod,

            // Inventory
            Raft, BookOfMagic, BlueRing, RedRing, Stepladder, MagicalKey, PowerBracelet,

            // Dungeon
            Compass, DungeonMap, SmallKey, TriforceFragmentBlue, TriforceFragmentYellow,

            // Miscellaneous
            RecoveryHeart, HeartContainer, Clock, RupeeRed, RupeeBlue, Fairy
        }
        
        private ItemType currItem = ItemType.Sword;
        
        public ItemType GetCurrentItem()
        {
            return currItem;
        }

        public Texture2D itemSpritesheet;
        private static ItemFactory instance = new ItemFactory(); 
        
        private ItemFactory() 
        { 
            itemSpritesheet = sprint0.Sprites.Texture2DStorage.GetItemSpriteSheet(); 
        } 
        
        public static ItemFactory Instance 
        { 
            get {
                return instance; 
            } 
        }

        // Equipment Items
        public ISprite BuildSword(SpriteBatch spriteBatch)
        {
            currItem = ItemType.Sword;
            return new ItemSword(itemSpritesheet);
        }

        public ISprite BuildWhiteSword(SpriteBatch spriteBatch)
        {
            currItem = ItemType.WhiteSword;
            return new ItemWhiteSword(itemSpritesheet);
        }

        public ISprite BuildBoomerang(SpriteBatch spriteBatch)
        {
            currItem = ItemType.Boomerang;
            return new ItemBoomerang(itemSpritesheet);
        }

        public ISprite BuildMagicalBoomerang(SpriteBatch spriteBatch)
        {
            currItem = ItemType.MagicalBoomerang;
            return new ItemMagicalBoomerang(itemSpritesheet);
        }

        public ISprite BuildBomb(SpriteBatch spriteBatch)
        {
            currItem = ItemType.Bomb;
            return new ItemBomb(itemSpritesheet);
        }

        public ISprite BuildBow(SpriteBatch spriteBatch)
        {
            currItem = ItemType.Bow;
            return new ItemBow(itemSpritesheet);
        }

        public ISprite BuildArrow(SpriteBatch spriteBatch)
        {
            currItem = ItemType.Arrow;
            return new ItemArrow(itemSpritesheet);
        }

        public ISprite BuildSilverArrow(SpriteBatch spriteBatch)
        {
            currItem = ItemType.SilverArrow;
            return new ItemSilverArrow(itemSpritesheet);
        }

        public ISprite BuildCandleRed(SpriteBatch spriteBatch)
        {
            currItem = ItemType.CandleRed;
            return new ItemCandleRed(itemSpritesheet);
        }

        public ISprite BuildCandleBlue(SpriteBatch spriteBatch)
        {
            currItem = ItemType.CandleBlue;
            return new ItemCandleBlue(itemSpritesheet);
        }

        public ISprite BuildRecorder(SpriteBatch spriteBatch)
        {
            currItem = ItemType.Recorder;
            return new ItemRecorder(itemSpritesheet);
        }

        public ISprite BuildFood(SpriteBatch spriteBatch)
        {
            currItem = ItemType.Food;
            return new ItemFood(itemSpritesheet);
        }

        public ISprite BuildLetter(SpriteBatch spriteBatch)
        {
            currItem = ItemType.Letter;
            return new ItemLetter(itemSpritesheet);
        }

        public ISprite BuildPotionRed(SpriteBatch spriteBatch)
        {
            currItem = ItemType.PotionRed;
            return new ItemPotionRed(itemSpritesheet);
        }

        public ISprite BuildPotionBlue(SpriteBatch spriteBatch)
        {
            currItem = ItemType.PotionBlue;
            return new ItemPotionBlue(itemSpritesheet);
        }

        public ISprite BuildMagicalRod(SpriteBatch spriteBatch)
        {
            currItem = ItemType.MagicalRod;
            return new ItemMagicalRod(itemSpritesheet);
        }

        // Inventory Items
        public ISprite BuildRaft(SpriteBatch spriteBatch)
        {
            currItem = ItemType.Raft;
            return new ItemRaft(itemSpritesheet);
        }

        public ISprite BuildBookOfMagic(SpriteBatch spriteBatch)
        {
            currItem = ItemType.BookOfMagic;
            return new ItemBookOfMagic(itemSpritesheet);
        }

        public ISprite BuildBlueRing(SpriteBatch spriteBatch)
        {
            currItem = ItemType.BlueRing;
            return new ItemBlueRing(itemSpritesheet);
        }

        public ISprite BuildRedRing(SpriteBatch spriteBatch)
        {
            currItem = ItemType.RedRing;
            return new ItemRedRing(itemSpritesheet);
        }

        public ISprite BuildStepladder(SpriteBatch spriteBatch)
        {
            currItem = ItemType.Stepladder;
            return new ItemStepladder(itemSpritesheet);
        }
        

        public ISprite BuildMagicalKey(SpriteBatch spriteBatch)
        {
            currItem = ItemType.MagicalKey;
            return new ItemMagicalKey(itemSpritesheet);
        }

        public ISprite BuildPowerBracelet(SpriteBatch spriteBatch)
        {
            currItem = ItemType.PowerBracelet;
            return new ItemPowerBracelet(itemSpritesheet);
        }

        // Dungeon Items
        public ISprite BuildCompass(SpriteBatch spriteBatch)
        {
            currItem = ItemType.Compass;
            return new ItemCompass(itemSpritesheet);
        }

        public ISprite BuildDungeonMap(SpriteBatch spriteBatch)
        {
            currItem = ItemType.DungeonMap;
            return new ItemDungeonMap(itemSpritesheet);
        }

        public ISprite BuildSmallKey(SpriteBatch spriteBatch)
        {
            currItem = ItemType.SmallKey;
            return new ItemSmallKey(itemSpritesheet);
        }

        public ISprite BuildTriforceFragmentBlue(SpriteBatch spriteBatch)
        {
            currItem = ItemType.TriforceFragmentBlue;
            return new ItemTriforceFragmentBlue(itemSpritesheet);
        }

        public ISprite BuildTriforceFragmentYellow(SpriteBatch spriteBatch)
        {
            currItem = ItemType.TriforceFragmentYellow;
            return new ItemTriforceFragmentYellow(itemSpritesheet);
        }

        // Miscellaneous Items
        public ISprite BuildRecoveryHeart(SpriteBatch spriteBatch)
        {
            currItem = ItemType.RecoveryHeart;
            return new ItemRecoveryHeart(itemSpritesheet);
        }

        public ISprite BuildHeartContainer(SpriteBatch spriteBatch)
        {
            currItem = ItemType.HeartContainer;
            return new ItemHeartContainer(itemSpritesheet);
        }

        public ISprite BuildClock(SpriteBatch spriteBatch)
        {
            currItem = ItemType.Clock;
            return new ItemClock(itemSpritesheet);
        }

        public ISprite BuildRupeeRed(SpriteBatch spriteBatch)
        {
            currItem = ItemType.RupeeRed;
            return new ItemRupeeRed(itemSpritesheet);
        }

        public ISprite BuildRupeeBlue(SpriteBatch spriteBatch)
        {
            currItem = ItemType.RupeeBlue;
            return new ItemRupeeBlue(itemSpritesheet);
        }
         public ISprite BuildFairy(SpriteBatch spriteBatch)
        {
            currItem = ItemType.Fairy;
            return new ItemFairy(itemSpritesheet);
        }
    }
}