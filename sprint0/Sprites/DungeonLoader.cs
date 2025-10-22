using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using sprint0.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using static sprint0.Sprites.DungeonCarousel;

namespace sprint0.Sprites
{
    public class DungeonLoader
    {
        private ISprite[] storage;
        private String path;
        private int storageIdx = 0;
        private BlockFactory blocks = BlockFactory.Instance;

        private List<Rectangle> rectangles;

        public Texture2D border = Texture2DStorage.GetDungeonBorder();
        public DungeonLoader(BlockFactory blocks, string csvContent)
        {
            this.blocks = blocks;
            this.path = csvContent;
            int totalCells = 84;
            storage = new ISprite[totalCells];
            storageIdx = 0;
            this.rectangles = new List<Rectangle>();
        }

        public void Update(GameTime gameTime)
        {

        }
        public void Draw(SpriteBatch sprite, GraphicsDevice graphics)
        {
            sprite.Draw(border, new Vector2(0, 0), new Rectangle(0, 0, graphics.Viewport.Width, graphics.Viewport.Height), Color.White, 0f, Vector2.Zero, 3.0f, SpriteEffects.None, 0f);

            const int gridColumns = 12;
            const int gridRows = 7;
            const int tileSize = 48;
            const int offset = 2;

            storageIdx = 0;
            string[] lines = path.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);

            int maxCells = Math.Min(storage.Length, gridColumns * gridRows);

            foreach (string line in lines)
            {
                string[] columns = line.Split(',');
                foreach (string block in columns)
                {
                    
                    switch (block)
                    {
                        case "Tile":
                            storage[storageIdx] = blocks.BuildTileBlock(sprite, Vector2.Zero);
                            break;
                        case "ChiseledTile":
                            storage[storageIdx] = blocks.BuildChiseledTileBlock(sprite, Vector2.Zero);
                            break;
                        case "Fish":
                            storage[storageIdx] = blocks.BuildFishBlock(sprite, Vector2.Zero);
                            break;
                        case "Dragon":
                            storage[storageIdx] = blocks.BuildDragonBlock(sprite, Vector2.Zero);
                            break;
                        case "Void":
                            storage[storageIdx] = blocks.BuildVoidBlock(sprite, Vector2.Zero);
                            break;
                        case "Dirt":
                            storage[storageIdx] = blocks.BuildDirtBlock(sprite, Vector2.Zero);
                            break;
                        case "Solid":
                            storage[storageIdx] = blocks.BuildSolidBlock(sprite, Vector2.Zero);
                            break;
                        case "Stair":
                            storage[storageIdx] = blocks.BuildStairBlock(sprite, Vector2.Zero);
                            break;
                        case "Brick":
                            storage[storageIdx] = blocks.BuildBrickBlock(sprite, Vector2.Zero);
                            break;
                        case "Grate":
                            storage[storageIdx] = blocks.BuildGrateBlock(sprite, Vector2.Zero);
                            break;
                    }

                    int col = storageIdx % gridColumns;
                    int row = storageIdx / gridColumns;
                    Vector2 position = new Vector2((col + offset) * tileSize, (row + offset) * tileSize);
                    rectangles.Add(new Rectangle((int)position.X, (int)position.Y, 48, 48));
                    storage[storageIdx].Draw(sprite, position);
                    storageIdx++;
                    
                }

                if (storageIdx >= maxCells) break;
            }
        }
        public List<Rectangle> GetBlockList()
		{
            return rectangles;
		}
    }
}
