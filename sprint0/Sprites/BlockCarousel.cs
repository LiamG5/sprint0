using Microsoft.Xna.Framework.Graphics;
using sprint0.Interfaces;
using sprint0.Sprites;

namespace sprint0.Sprites
{
    public class BlockCarousel : ICarousel
    {
        private BlockFactory blockFactory;
        private SpriteBatch spriteBatch;
        private ISprite currentBlock;
        private int currentIndex = 0;
        private BlockFactory.BlockType[] blockTypes = {
            BlockFactory.BlockType.Tile,
            BlockFactory.BlockType.ChiseledTile,
            BlockFactory.BlockType.Fish,
            BlockFactory.BlockType.Dragon,
            BlockFactory.BlockType.Void,
            BlockFactory.BlockType.Dirt,
            BlockFactory.BlockType.Solid,
            BlockFactory.BlockType.Stair,
            BlockFactory.BlockType.Brick,
            BlockFactory.BlockType.Grate
        };

        public BlockCarousel(BlockFactory blockFactory, SpriteBatch spriteBatch)
        {
            this.blockFactory = blockFactory;
            this.spriteBatch = spriteBatch;
            currentBlock = blockFactory.BuildTileBlock(spriteBatch);
        }

        public void Next()
        {
            currentIndex = (currentIndex + 1) % blockTypes.Length;
            UpdateCurrentBlock();
        }

        public void Prev()
        {
            currentIndex = (currentIndex - 1 + blockTypes.Length) % blockTypes.Length;
            UpdateCurrentBlock();
        }

        private void UpdateCurrentBlock()
        {
            switch (blockTypes[currentIndex])
            {
                case BlockFactory.BlockType.Tile:
                    currentBlock = blockFactory.BuildTileBlock(spriteBatch);
                    break;
                case BlockFactory.BlockType.ChiseledTile:
                    currentBlock = blockFactory.BuildChiseledTileBlock(spriteBatch);
                    break;
                case BlockFactory.BlockType.Fish:
                    currentBlock = blockFactory.BuildFishBlock(spriteBatch);
                    break;
                case BlockFactory.BlockType.Dragon:
                    currentBlock = blockFactory.BuildDragonBlock(spriteBatch);
                    break;
                case BlockFactory.BlockType.Void:
                    currentBlock = blockFactory.BuildVoidBlock(spriteBatch);
                    break;
                case BlockFactory.BlockType.Dirt:
                    currentBlock = blockFactory.BuildDirtBlock(spriteBatch);
                    break;
                case BlockFactory.BlockType.Solid:
                    currentBlock = blockFactory.BuildSolidBlock(spriteBatch);
                    break;
                case BlockFactory.BlockType.Stair:
                    currentBlock = blockFactory.BuildStairBlock(spriteBatch);
                    break;
                case BlockFactory.BlockType.Brick:
                    currentBlock = blockFactory.BuildBrickBlock(spriteBatch);
                    break;
                case BlockFactory.BlockType.Grate:
                    currentBlock = blockFactory.BuildGrateBlock(spriteBatch);
                    break;
            }
        }

        public ISprite GetCurrentBlock()
        {
            return currentBlock;
        }
    }
}
