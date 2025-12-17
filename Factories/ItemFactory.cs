using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using sprint0.Classes;
using sprint0.Interfaces;
using sprint0.Sprites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace sprint0.Factories
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
            itemSpritesheet = Texture2DStorage.GetItemSpriteSheet(); 
        } 
        
        public static ItemFactory Instance 
        { 
            get {
                return instance; 
            } 
        }

        // Equipment Items
        public IItem BuildSword()
        {
            currItem = ItemType.Sword;
            return new ItemSword(itemSpritesheet);
        }

        public IItem BuildSword(Vector2 position)
        {
            currItem = ItemType.Sword;
            return new ItemSword(itemSpritesheet, position);
        }

        public IItem BuildWhiteSword()
        {
            currItem = ItemType.WhiteSword;
            return new ItemWhiteSword(itemSpritesheet);
        }

        public IItem BuildWhiteSword(Vector2 position)
        {
            currItem = ItemType.WhiteSword;
            return new ItemWhiteSword(itemSpritesheet, position);
        }

        public IItem BuildBoomerang()
        {
            currItem = ItemType.Boomerang;
            return new ItemBoomerang(itemSpritesheet);
        }

        public IItem BuildBoomerang(Vector2 position)
        {
            currItem = ItemType.Boomerang;
            return new ItemBoomerang(itemSpritesheet, position);
        }

        public IItem BuildMagicalBoomerang()
        {
            currItem = ItemType.MagicalBoomerang;
            return new ItemMagicalBoomerang(itemSpritesheet);
        }

        public IItem BuildMagicalBoomerang(Vector2 position)
        {
            currItem = ItemType.MagicalBoomerang;
            return new ItemMagicalBoomerang(itemSpritesheet, position);
        }

        public IItem BuildBomb()
        {
            currItem = ItemType.Bomb;
            return new ItemBomb(itemSpritesheet);
        }

        public IItem BuildBomb(Vector2 position)
        {
            currItem = ItemType.Bomb;
            return new ItemBomb(itemSpritesheet, position);
        }

        public IItem BuildBow()
        {
            currItem = ItemType.Bow;
            return new ItemBow(itemSpritesheet);
        }

        public IItem BuildBow(Vector2 position)
        {
            currItem = ItemType.Bow;
            return new ItemBow(itemSpritesheet, position);
        }

        public IItem BuildArrow()
        {
            currItem = ItemType.Arrow;
            return new ItemArrow(itemSpritesheet);
        }

        public IItem BuildArrow(Vector2 position)
        {
            currItem = ItemType.Arrow;
            return new ItemArrow(itemSpritesheet, position);
        }

        public IItem BuildSilverArrow()
        {
            currItem = ItemType.SilverArrow;
            return new ItemSilverArrow(itemSpritesheet);
        }

        public IItem BuildSilverArrow(Vector2 position)
        {
            currItem = ItemType.SilverArrow;
            return new ItemSilverArrow(itemSpritesheet, position);
        }

        public IItem BuildCandleRed()
        {
            currItem = ItemType.CandleRed;
            return new ItemCandleRed(itemSpritesheet);
        }

        public IItem BuildCandleRed(Vector2 position)
        {
            currItem = ItemType.CandleRed;
            return new ItemCandleRed(itemSpritesheet, position);
        }

        public IItem BuildCandleBlue()
        {
            currItem = ItemType.CandleBlue;
            return new ItemCandleBlue(itemSpritesheet);
        }

        public IItem BuildCandleBlue(Vector2 position)
        {
            currItem = ItemType.CandleBlue;
            return new ItemCandleBlue(itemSpritesheet, position);
        }

        public IItem BuildRecorder()
        {
            currItem = ItemType.Recorder;
            return new ItemRecorder(itemSpritesheet);
        }

        public IItem BuildRecorder(Vector2 position)
        {
            currItem = ItemType.Recorder;
            return new ItemRecorder(itemSpritesheet, position);
        }

        public IItem BuildFood()
        {
            currItem = ItemType.Food;
            return new ItemFood(itemSpritesheet);
        }

        public IItem BuildFood(Vector2 position)
        {
            currItem = ItemType.Food;
            return new ItemFood(itemSpritesheet, position);
        }

        public IItem BuildLetter()
        {
            currItem = ItemType.Letter;
            return new ItemLetter(itemSpritesheet);
        }

        public IItem BuildLetter(Vector2 position)
        {
            currItem = ItemType.Letter;
            return new ItemLetter(itemSpritesheet, position);
        }

        public IItem BuildPotionRed()
        {
            currItem = ItemType.PotionRed;
            return new ItemPotionRed(itemSpritesheet);
        }

        public IItem BuildPotionRed(Vector2 position)
        {
            currItem = ItemType.PotionRed;
            return new ItemPotionRed(itemSpritesheet, position);
        }

        public IItem BuildPotionBlue()
        {
            currItem = ItemType.PotionBlue;
            return new ItemPotionBlue(itemSpritesheet);
        }

        public IItem BuildPotionBlue(Vector2 position)
        {
            currItem = ItemType.PotionBlue;
            return new ItemPotionBlue(itemSpritesheet, position);
        }

        public IItem BuildMagicalRod()
        {
            currItem = ItemType.MagicalRod;
            return new ItemMagicalRod(itemSpritesheet);
        }

        public IItem BuildMagicalRod(Vector2 position)
        {
            currItem = ItemType.MagicalRod;
            return new ItemMagicalRod(itemSpritesheet, position);
        }

        // Inventory Items
        public IItem BuildRaft()
        {
            currItem = ItemType.Raft;
            return new ItemRaft(itemSpritesheet);
        }

        public IItem BuildRaft(Vector2 position)
        {
            currItem = ItemType.Raft;
            return new ItemRaft(itemSpritesheet, position);
        }

        public IItem BuildBookOfMagic()
        {
            currItem = ItemType.BookOfMagic;
            return new ItemBookOfMagic(itemSpritesheet);
        }

        public IItem BuildBookOfMagic(Vector2 position)
        {
            currItem = ItemType.BookOfMagic;
            return new ItemBookOfMagic(itemSpritesheet, position);
        }

        public IItem BuildBlueRing()
        {
            currItem = ItemType.BlueRing;
            return new ItemBlueRing(itemSpritesheet);
        }

        public IItem BuildBlueRing(Vector2 position)
        {
            currItem = ItemType.BlueRing;
            return new ItemBlueRing(itemSpritesheet, position);
        }

        public IItem BuildRedRing()
        {
            currItem = ItemType.RedRing;
            return new ItemRedRing(itemSpritesheet);
        }

        public IItem BuildRedRing(Vector2 position)
        {
            currItem = ItemType.RedRing;
            return new ItemRedRing(itemSpritesheet, position);
        }

        public IItem BuildStepladder()
        {
            currItem = ItemType.Stepladder;
            return new ItemStepladder(itemSpritesheet);
        }

        public IItem BuildStepladder(Vector2 position)
        {
            currItem = ItemType.Stepladder;
            return new ItemStepladder(itemSpritesheet, position);
        }

        public IItem BuildMagicalKey()
        {
            currItem = ItemType.MagicalKey;
            return new ItemMagicalKey(itemSpritesheet);
        }

        public IItem BuildMagicalKey(Vector2 position)
        {
            currItem = ItemType.MagicalKey;
            return new ItemMagicalKey(itemSpritesheet, position);
        }

        public IItem BuildPowerBracelet()
        {
            currItem = ItemType.PowerBracelet;
            return new ItemPowerBracelet(itemSpritesheet);
        }

        public IItem BuildPowerBracelet(Vector2 position)
        {
            currItem = ItemType.PowerBracelet;
            return new ItemPowerBracelet(itemSpritesheet, position);
        }

        // Dungeon Items
        public IItem BuildCompass()
        {
            currItem = ItemType.Compass;
            return new ItemCompass(itemSpritesheet);
        }

        public IItem BuildCompass(Vector2 position)
        {
            currItem = ItemType.Compass;
            return new ItemCompass(itemSpritesheet, position);
        }

        public IItem BuildDungeonMap()
        {
            currItem = ItemType.DungeonMap;
            return new ItemDungeonMap(itemSpritesheet);
        }

        public IItem BuildDungeonMap(Vector2 position)
        {
            currItem = ItemType.DungeonMap;
            return new ItemDungeonMap(itemSpritesheet, position);
        }

        public IItem BuildSmallKey()
        {
            currItem = ItemType.SmallKey;
            return new ItemSmallKey(itemSpritesheet);
        }

        public IItem BuildSmallKey(Vector2 position)
        {
            currItem = ItemType.SmallKey;
            return new ItemSmallKey(itemSpritesheet, position);
        }

        public IItem BuildTriforceFragment()
        {
            currItem = ItemType.TriforceFragment;
            return new ItemTriforceFragment(itemSpritesheet);
        }

        public IItem BuildTriforceFragment(Vector2 position)
        {
            currItem = ItemType.TriforceFragment;
            return new ItemTriforceFragment(itemSpritesheet, position);
        }

        // Miscellaneous Items
        public IItem BuildRecoveryHeart()
        {
            currItem = ItemType.RecoveryHeart;
            return new ItemRecoveryHeart(itemSpritesheet);
        }

        public IItem BuildRecoveryHeart(Vector2 position)
        {
            currItem = ItemType.RecoveryHeart;
            return new ItemRecoveryHeart(itemSpritesheet, position);
        }

        public IItem BuildHeartContainer()
        {
            currItem = ItemType.HeartContainer;
            return new ItemHeartContainer(itemSpritesheet);
        }

        public IItem BuildHeartContainer(Vector2 position)
        {
            currItem = ItemType.HeartContainer;
            return new ItemHeartContainer(itemSpritesheet, position);
        }

        public IItem BuildClock()
        {
            currItem = ItemType.Clock;
            return new ItemClock(itemSpritesheet);
        }

        public IItem BuildClock(Vector2 position)
        {
            currItem = ItemType.Clock;
            return new ItemClock(itemSpritesheet, position);
        }

        public IItem BuildRupeeRed()
        {
            currItem = ItemType.RupeeRed;
            return new ItemRupeeRed(itemSpritesheet);
        }

        public IItem BuildRupeeRed(Vector2 position)
        {
            currItem = ItemType.RupeeRed;
            return new ItemRupeeRed(itemSpritesheet, position);
        }

        public IItem BuildRupeeBlue()
        {
            currItem = ItemType.RupeeBlue;
            return new ItemRupeeBlue(itemSpritesheet);
        }

        public IItem BuildRupeeBlue(Vector2 position)
        {
            currItem = ItemType.RupeeBlue;
            return new ItemRupeeBlue(itemSpritesheet, position);
        }
        public IItem BuildRupee()
        {
            currItem = ItemType.Rupee;
            return new ItemRupee(itemSpritesheet);
        }

        public IItem BuildRupee(Vector2 position)
        {
            currItem = ItemType.Rupee;
            return new ItemRupee(itemSpritesheet, position);
        }


        public IItem BuildFairy()
        {
            currItem = ItemType.Fairy;
            return new ItemFairy(itemSpritesheet);
        }

        public IItem BuildFairy(Vector2 position)
        {
            currItem = ItemType.Fairy;
            return new ItemFairy(itemSpritesheet, position);
        }

        public IItem BuildFairy(Vector2 position, Func<Vector2> targetProvider)
        {
            currItem = ItemType.Fairy;
            return new ItemFairy(itemSpritesheet, position, targetProvider);
        }
    }
}