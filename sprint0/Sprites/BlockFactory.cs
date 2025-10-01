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
    public class BlockFactory 
    { 
        public enum BlockType
        {
            Tile, ChiseledTile, Fish, Dragon, Void, Dirt, Solid, Stair, Brick, Grate 
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
            blockSpritesheet = Texture2DStorage.GetBlockSpriteSheet(); 
        } 
        public static BlockFactory Instance 
        { 
            get {
                return instance; 
            } 
        }
        public ISprite BuildTileBlock(SpriteBatch sprite)
        {
            currBlock = BlockType.Tile;
            return new BlockTile(blockSpritesheet);
        }

        public ISprite BuildChiseledTileBlock(SpriteBatch sprite)
        {
            currBlock = BlockType.ChiseledTile;
            return new BlockChiseledTile(blockSpritesheet);
        }

        public ISprite BuildFishBlock(SpriteBatch sprite)
        {
            currBlock = BlockType.Fish;
            return new BlockFish(blockSpritesheet);
        }

        public ISprite BuildDragonBlock(SpriteBatch sprite)
        {
            currBlock = BlockType.Dragon;
            return new BlockDragon(blockSpritesheet);
        }

        public ISprite BuildVoidBlock(SpriteBatch sprite)
        {
            currBlock = BlockType.Void;
            return new BlockVoid(blockSpritesheet);
        }
        public ISprite BuildDirtBlock(SpriteBatch sprite)
        {
            currBlock = BlockType.Dirt;
            return new BlockDirt(blockSpritesheet);
        }
        
        public ISprite BuildSolidBlock(SpriteBatch sprite)
        {
            currBlock = BlockType.Solid;
            return new BlockSolid(blockSpritesheet);
        }
        public ISprite BuildStairBlock(SpriteBatch sprite)
        {
            currBlock = BlockType.Stair;
            return new BlockStair(blockSpritesheet);
        }

        public ISprite BuildBrickBlock(SpriteBatch sprite)
        {
            currBlock = BlockType.Brick;
            return new BlockBrick(blockSpritesheet);
        }

        public ISprite BuildGrateBlock(SpriteBatch sprite)
        {
            currBlock = BlockType.Grate;
            return new BlockGrate(blockSpritesheet);
        }
    }
}
