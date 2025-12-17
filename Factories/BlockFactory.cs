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
    public class BlockFactory 
    { 
        public enum BlockType
        {
            Tile, ChiseledTile, Fish, Dragon, Void, Dirt, Solid, Stair, Brick, Grate, MovableTile, InvisibleBlock 
        }

        private BlockType currBlock = BlockType.Tile;
        public BlockType getBlock()
        {
            return currBlock;
        }

        public Texture2D blockSpritesheet;
        private static BlockFactory instance = new BlockFactory(); 
        private BlockFactory() 
        { 
        } 
        public static BlockFactory Instance 
        { 
            get {
                return instance; 
            } 
        }

        public void Initialize()
        {
            if (blockSpritesheet == null)
            {
                blockSpritesheet = Texture2DStorage.GetBlockSpriteSheet();
                if (blockSpritesheet == null)
                {
                    throw new InvalidOperationException("Block sprite sheet not loaded. Make sure Texture2DStorage.LoadAllTextures() is called before using BlockFactory.");
                }
            }
        }
        public ISprite BuildTileBlock(SpriteBatch sprite, Vector2 position)
        {
            Initialize();
            currBlock = BlockType.Tile;
            
            return new BlockTile(blockSpritesheet, position);
        }

        public ISprite BuildChiseledTileBlock(SpriteBatch sprite, Vector2 position)
        {
            Initialize();
            currBlock = BlockType.ChiseledTile;
            return new BlockChiseledTile(blockSpritesheet, position);
        }

        public ISprite BuildFishBlock(SpriteBatch sprite, Vector2 position)
        {
            Initialize();
            currBlock = BlockType.Fish;
            return new BlockFish(blockSpritesheet, position);
        }

        public ISprite BuildDragonBlock(SpriteBatch sprite, Vector2 position)
        {
            Initialize();
            currBlock = BlockType.Dragon;
            return new BlockDragon(blockSpritesheet, position);
        }

        public ISprite BuildVoidBlock(SpriteBatch sprite, Vector2 position)
        {
            Initialize();
            currBlock = BlockType.Void;
            return new BlockVoid(blockSpritesheet, position);
        }
        public ISprite BuildDirtBlock(SpriteBatch sprite, Vector2 position)
        {
            Initialize();
            currBlock = BlockType.Dirt;
            return new BlockDirt(blockSpritesheet, position);
        }
        
        public ISprite BuildSolidBlock(SpriteBatch sprite, Vector2 position)
        {
            Initialize();
            currBlock = BlockType.Solid;
            return new BlockSolid(blockSpritesheet, position);
        }
        public ISprite BuildStairBlock(SpriteBatch sprite, Vector2 position)
        {
            Initialize();
            currBlock = BlockType.Stair;
            return new BlockStair(blockSpritesheet, position);
        }

        public ISprite BuildBrickBlock(SpriteBatch sprite, Vector2 position)
        {
            Initialize();
            currBlock = BlockType.Brick;
            return new BlockBrick(blockSpritesheet, position);
        }

        public ISprite BuildGrateBlock(SpriteBatch sprite, Vector2 position)
        {
            Initialize();
            currBlock = BlockType.Grate;
            return new BlockGrate(blockSpritesheet, position);
        }

        public ISprite BuildMovableTileBlock(SpriteBatch sprite, Vector2 position)
        {
            Initialize();
            currBlock = BlockType.MovableTile;
            return new BlockMovableTile(blockSpritesheet, position);
        }

        public ISprite BuildInvisibleBlock(SpriteBatch sprite, Vector2 position)
        {
            Initialize();
            currBlock = BlockType.InvisibleBlock;
            return new InvisibleBlock(position);
        }
    }
}
