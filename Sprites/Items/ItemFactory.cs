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
            Compass, DungeonMap, SmallKey, TriforceFragment,

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
        public ISprite BuildSword()
        {
            currItem = ItemType.Sword;
            return new ItemSword(itemSpritesheet);
        }

        public ISprite BuildWhiteSword()
        {
            currItem = ItemType.WhiteSword;
            return new ItemWhiteSword(itemSpritesheet);
        }

        public ISprite BuildBoomerang()
        {
            currItem = ItemType.Boomerang;
            return new ItemBoomerang(itemSpritesheet);
        }

        public ISprite BuildMagicalBoomerang()
        {
            currItem = ItemType.MagicalBoomerang;
            return new ItemMagicalBoomerang(itemSpritesheet);
        }

        public ISprite BuildBomb()
        {
            currItem = ItemType.Bomb;
            return new ItemBomb(itemSpritesheet);
        }

        public ISprite BuildBow()
        {
            currItem = ItemType.Bow;
            return new ItemBow(itemSpritesheet);
        }

        public ISprite BuildArrow()
        {
            currItem = ItemType.Arrow;
            return new ItemArrow(itemSpritesheet);
        }

        public ISprite BuildSilverArrow()
        {
            currItem = ItemType.SilverArrow;
            return new ItemSilverArrow(itemSpritesheet);
        }

        public ISprite BuildCandleRed()
        {
            currItem = ItemType.CandleRed;
            return new ItemCandleRed(itemSpritesheet);
        }

        public ISprite BuildCandleBlue()
        {
            currItem = ItemType.CandleBlue;
            return new ItemCandleBlue(itemSpritesheet);
        }

        public ISprite BuildRecorder()
        {
            currItem = ItemType.Recorder;
            return new ItemRecorder(itemSpritesheet);
        }

        public ISprite BuildFood()
        {
            currItem = ItemType.Food;
            return new ItemFood(itemSpritesheet);
        }

        public ISprite BuildLetter()
        {
            currItem = ItemType.Letter;
            return new ItemLetter(itemSpritesheet);
        }

        public ISprite BuildPotionRed()
        {
            currItem = ItemType.PotionRed;
            return new ItemPotionRed(itemSpritesheet);
        }

        public ISprite BuildPotionBlue()
        {
            currItem = ItemType.PotionBlue;
            return new ItemPotionBlue(itemSpritesheet);
        }

        public ISprite BuildMagicalRod()
        {
            currItem = ItemType.MagicalRod;
            return new ItemMagicalRod(itemSpritesheet);
        }

        // Inventory Items
        public ISprite BuildRaft()
        {
            currItem = ItemType.Raft;
            return new ItemRaft(itemSpritesheet);
        }

        public ISprite BuildBookOfMagic()
        {
            currItem = ItemType.BookOfMagic;
            return new ItemBookOfMagic(itemSpritesheet);
        }

        public ISprite BuildBlueRing()
        {
            currItem = ItemType.BlueRing;
            return new ItemBlueRing(itemSpritesheet);
        }

        public ISprite BuildRedRing()
        {
            currItem = ItemType.RedRing;
            return new ItemRedRing(itemSpritesheet);
        }

        public ISprite BuildStepladder()
        {
            currItem = ItemType.Stepladder;
            return new ItemStepladder(itemSpritesheet);
        }
        

        public ISprite BuildMagicalKey()
        {
            currItem = ItemType.MagicalKey;
            return new ItemMagicalKey(itemSpritesheet);
        }

        public ISprite BuildPowerBracelet()
        {
            currItem = ItemType.PowerBracelet;
            return new ItemPowerBracelet(itemSpritesheet);
        }

        // Dungeon Items
        public ISprite BuildCompass()
        {
            currItem = ItemType.Compass;
            return new ItemCompass(itemSpritesheet);
        }

        public ISprite BuildDungeonMap()
        {
            currItem = ItemType.DungeonMap;
            return new ItemDungeonMap(itemSpritesheet);
        }

        public ISprite BuildSmallKey()
        {
            currItem = ItemType.SmallKey;
            return new ItemSmallKey(itemSpritesheet);
        }


        public ISprite BuildTriforceFragment()
        {
            currItem = ItemType.TriforceFragment;
            return new ItemTriforceFragment(itemSpritesheet);
        }

        // Miscellaneous Items
        public ISprite BuildRecoveryHeart()
        {
            currItem = ItemType.RecoveryHeart;
            return new ItemRecoveryHeart(itemSpritesheet);
        }

        public ISprite BuildHeartContainer()
        {
            currItem = ItemType.HeartContainer;
            return new ItemHeartContainer(itemSpritesheet);
        }

        public ISprite BuildClock()
        {
            currItem = ItemType.Clock;
            return new ItemClock(itemSpritesheet);
        }

        public ISprite BuildRupeeRed()
        {
            currItem = ItemType.RupeeRed;
            return new ItemRupeeRed(itemSpritesheet);
        }

        public ISprite BuildRupeeBlue()
        {
            currItem = ItemType.RupeeBlue;
            return new ItemRupeeBlue(itemSpritesheet);
        }
         public ISprite BuildFairy()
        {
            currItem = ItemType.Fairy;
            return new ItemFairy(itemSpritesheet);
        }
    }
}