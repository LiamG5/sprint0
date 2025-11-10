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
            RecoveryHeart, HeartContainer, Clock, RupeeRed, RupeeBlue, Rupee, Fairy
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

        public ISprite BuildSword(Vector2 position)
        {
            currItem = ItemType.Sword;
            return new ItemSword(itemSpritesheet, position);
        }

        public ISprite BuildWhiteSword()
        {
            currItem = ItemType.WhiteSword;
            return new ItemWhiteSword(itemSpritesheet);
        }

        public ISprite BuildWhiteSword(Vector2 position)
        {
            currItem = ItemType.WhiteSword;
            return new ItemWhiteSword(itemSpritesheet, position);
        }

        public ISprite BuildBoomerang()
        {
            currItem = ItemType.Boomerang;
            return new ItemBoomerang(itemSpritesheet);
        }

        public ISprite BuildBoomerang(Vector2 position)
        {
            currItem = ItemType.Boomerang;
            return new ItemBoomerang(itemSpritesheet, position);
        }

        public ISprite BuildMagicalBoomerang()
        {
            currItem = ItemType.MagicalBoomerang;
            return new ItemMagicalBoomerang(itemSpritesheet);
        }

        public ISprite BuildMagicalBoomerang(Vector2 position)
        {
            currItem = ItemType.MagicalBoomerang;
            return new ItemMagicalBoomerang(itemSpritesheet, position);
        }

        public ISprite BuildBomb()
        {
            currItem = ItemType.Bomb;
            return new ItemBomb(itemSpritesheet);
        }

        public ISprite BuildBomb(Vector2 position)
        {
            currItem = ItemType.Bomb;
            return new ItemBomb(itemSpritesheet, position);
        }

        public ISprite BuildBow()
        {
            currItem = ItemType.Bow;
            return new ItemBow(itemSpritesheet);
        }

        public ISprite BuildBow(Vector2 position)
        {
            currItem = ItemType.Bow;
            return new ItemBow(itemSpritesheet, position);
        }

        public ISprite BuildArrow()
        {
            currItem = ItemType.Arrow;
            return new ItemArrow(itemSpritesheet);
        }

        public ISprite BuildArrow(Vector2 position)
        {
            currItem = ItemType.Arrow;
            return new ItemArrow(itemSpritesheet, position);
        }

        public ISprite BuildSilverArrow()
        {
            currItem = ItemType.SilverArrow;
            return new ItemSilverArrow(itemSpritesheet);
        }

        public ISprite BuildSilverArrow(Vector2 position)
        {
            currItem = ItemType.SilverArrow;
            return new ItemSilverArrow(itemSpritesheet, position);
        }

        public ISprite BuildCandleRed()
        {
            currItem = ItemType.CandleRed;
            return new ItemCandleRed(itemSpritesheet);
        }

        public ISprite BuildCandleRed(Vector2 position)
        {
            currItem = ItemType.CandleRed;
            return new ItemCandleRed(itemSpritesheet, position);
        }

        public ISprite BuildCandleBlue()
        {
            currItem = ItemType.CandleBlue;
            return new ItemCandleBlue(itemSpritesheet);
        }

        public ISprite BuildCandleBlue(Vector2 position)
        {
            currItem = ItemType.CandleBlue;
            return new ItemCandleBlue(itemSpritesheet, position);
        }

        public ISprite BuildRecorder()
        {
            currItem = ItemType.Recorder;
            return new ItemRecorder(itemSpritesheet);
        }

        public ISprite BuildRecorder(Vector2 position)
        {
            currItem = ItemType.Recorder;
            return new ItemRecorder(itemSpritesheet, position);
        }

        public ISprite BuildFood()
        {
            currItem = ItemType.Food;
            return new ItemFood(itemSpritesheet);
        }

        public ISprite BuildFood(Vector2 position)
        {
            currItem = ItemType.Food;
            return new ItemFood(itemSpritesheet, position);
        }

        public ISprite BuildLetter()
        {
            currItem = ItemType.Letter;
            return new ItemLetter(itemSpritesheet);
        }

        public ISprite BuildLetter(Vector2 position)
        {
            currItem = ItemType.Letter;
            return new ItemLetter(itemSpritesheet, position);
        }

        public ISprite BuildPotionRed()
        {
            currItem = ItemType.PotionRed;
            return new ItemPotionRed(itemSpritesheet);
        }

        public ISprite BuildPotionRed(Vector2 position)
        {
            currItem = ItemType.PotionRed;
            return new ItemPotionRed(itemSpritesheet, position);
        }

        public ISprite BuildPotionBlue()
        {
            currItem = ItemType.PotionBlue;
            return new ItemPotionBlue(itemSpritesheet);
        }

        public ISprite BuildPotionBlue(Vector2 position)
        {
            currItem = ItemType.PotionBlue;
            return new ItemPotionBlue(itemSpritesheet, position);
        }

        public ISprite BuildMagicalRod()
        {
            currItem = ItemType.MagicalRod;
            return new ItemMagicalRod(itemSpritesheet);
        }

        public ISprite BuildMagicalRod(Vector2 position)
        {
            currItem = ItemType.MagicalRod;
            return new ItemMagicalRod(itemSpritesheet, position);
        }

        // Inventory Items
        public ISprite BuildRaft()
        {
            currItem = ItemType.Raft;
            return new ItemRaft(itemSpritesheet);
        }

        public ISprite BuildRaft(Vector2 position)
        {
            currItem = ItemType.Raft;
            return new ItemRaft(itemSpritesheet, position);
        }

        public ISprite BuildBookOfMagic()
        {
            currItem = ItemType.BookOfMagic;
            return new ItemBookOfMagic(itemSpritesheet);
        }

        public ISprite BuildBookOfMagic(Vector2 position)
        {
            currItem = ItemType.BookOfMagic;
            return new ItemBookOfMagic(itemSpritesheet, position);
        }

        public ISprite BuildBlueRing()
        {
            currItem = ItemType.BlueRing;
            return new ItemBlueRing(itemSpritesheet);
        }

        public ISprite BuildBlueRing(Vector2 position)
        {
            currItem = ItemType.BlueRing;
            return new ItemBlueRing(itemSpritesheet, position);
        }

        public ISprite BuildRedRing()
        {
            currItem = ItemType.RedRing;
            return new ItemRedRing(itemSpritesheet);
        }

        public ISprite BuildRedRing(Vector2 position)
        {
            currItem = ItemType.RedRing;
            return new ItemRedRing(itemSpritesheet, position);
        }

        public ISprite BuildStepladder()
        {
            currItem = ItemType.Stepladder;
            return new ItemStepladder(itemSpritesheet);
        }

        public ISprite BuildStepladder(Vector2 position)
        {
            currItem = ItemType.Stepladder;
            return new ItemStepladder(itemSpritesheet, position);
        }

        public ISprite BuildMagicalKey()
        {
            currItem = ItemType.MagicalKey;
            return new ItemMagicalKey(itemSpritesheet);
        }

        public ISprite BuildMagicalKey(Vector2 position)
        {
            currItem = ItemType.MagicalKey;
            return new ItemMagicalKey(itemSpritesheet, position);
        }

        public ISprite BuildPowerBracelet()
        {
            currItem = ItemType.PowerBracelet;
            return new ItemPowerBracelet(itemSpritesheet);
        }

        public ISprite BuildPowerBracelet(Vector2 position)
        {
            currItem = ItemType.PowerBracelet;
            return new ItemPowerBracelet(itemSpritesheet, position);
        }

        // Dungeon Items
        public ISprite BuildCompass()
        {
            currItem = ItemType.Compass;
            return new ItemCompass(itemSpritesheet);
        }

        public ISprite BuildCompass(Vector2 position)
        {
            currItem = ItemType.Compass;
            return new ItemCompass(itemSpritesheet, position);
        }

        public ISprite BuildDungeonMap()
        {
            currItem = ItemType.DungeonMap;
            return new ItemDungeonMap(itemSpritesheet);
        }

        public ISprite BuildDungeonMap(Vector2 position)
        {
            currItem = ItemType.DungeonMap;
            return new ItemDungeonMap(itemSpritesheet, position);
        }

        public ISprite BuildSmallKey()
        {
            currItem = ItemType.SmallKey;
            return new ItemSmallKey(itemSpritesheet);
        }

        public ISprite BuildSmallKey(Vector2 position)
        {
            currItem = ItemType.SmallKey;
            return new ItemSmallKey(itemSpritesheet, position);
        }

        public ISprite BuildTriforceFragment()
        {
            currItem = ItemType.TriforceFragment;
            return new ItemTriforceFragment(itemSpritesheet);
        }

        public ISprite BuildTriforceFragment(Vector2 position)
        {
            currItem = ItemType.TriforceFragment;
            return new ItemTriforceFragment(itemSpritesheet, position);
        }

        // Miscellaneous Items
        public ISprite BuildRecoveryHeart()
        {
            currItem = ItemType.RecoveryHeart;
            return new ItemRecoveryHeart(itemSpritesheet);
        }

        public ISprite BuildRecoveryHeart(Vector2 position)
        {
            currItem = ItemType.RecoveryHeart;
            return new ItemRecoveryHeart(itemSpritesheet, position);
        }

        public ISprite BuildHeartContainer()
        {
            currItem = ItemType.HeartContainer;
            return new ItemHeartContainer(itemSpritesheet);
        }

        public ISprite BuildHeartContainer(Vector2 position)
        {
            currItem = ItemType.HeartContainer;
            return new ItemHeartContainer(itemSpritesheet, position);
        }

        public ISprite BuildClock()
        {
            currItem = ItemType.Clock;
            return new ItemClock(itemSpritesheet);
        }

        public ISprite BuildClock(Vector2 position)
        {
            currItem = ItemType.Clock;
            return new ItemClock(itemSpritesheet, position);
        }

        public ISprite BuildRupeeRed()
        {
            currItem = ItemType.RupeeRed;
            return new ItemRupeeRed(itemSpritesheet);
        }

        public ISprite BuildRupeeRed(Vector2 position)
        {
            currItem = ItemType.RupeeRed;
            return new ItemRupeeRed(itemSpritesheet, position);
        }

        public ISprite BuildRupeeBlue()
        {
            currItem = ItemType.RupeeBlue;
            return new ItemRupeeBlue(itemSpritesheet);
        }

        public ISprite BuildRupeeBlue(Vector2 position)
        {
            currItem = ItemType.RupeeBlue;
            return new ItemRupeeBlue(itemSpritesheet, position);
        }
        public ISprite BuildRupee()
        {
            currItem = ItemType.Rupee;
            return new ItemRupee(itemSpritesheet);
        }

        public ISprite BuildRupee(Vector2 position)
        {
            currItem = ItemType.Rupee;
            return new ItemRupee(itemSpritesheet, position);
        }


        public ISprite BuildFairy()
        {
            currItem = ItemType.Fairy;
            return new ItemFairy(itemSpritesheet);
        }

        public ISprite BuildFairy(Vector2 position)
        {
            currItem = ItemType.Fairy;
            return new ItemFairy(itemSpritesheet, position);
        }
    }
}