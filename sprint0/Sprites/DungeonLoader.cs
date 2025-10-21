using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using sprint0.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
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
        private List<ICollidable> collidableObjects = new List<ICollidable>();

        public Texture2D border = Texture2DStorage.GetDungeonBorder();
        public DungeonLoader(BlockFactory blocks, string path)
        {
            this.blocks = blocks;
            this.path = path;
            int totalCells = 84;
            storage = new ISprite[totalCells];
            storageIdx = 0;
        }

        public void Update(GameTime gameTime)
        {

        }
        
        public List<ICollidable> GetCollidableObjects()
        {
            // Only update positions if the list is empty (first time)
            if (collidableObjects.Count == 0)
            {
                UpdateBlockPositions();
            }
            return collidableObjects;
        }
        
        private void UpdateBlockPositions()
        {
            const int gridColumns = 12;
            const int gridRows = 7;
            const int tileSize = 48;
            const int offset = 2;
            
            // Clear and rebuild the collidable objects list
            collidableObjects.Clear();
            
            int currentIdx = 0;
            string[] lines = File.ReadAllLines(path);
            
            foreach (string line in lines)
            {
                string[] columns = line.Split(',');
                foreach (string block in columns)
                {
                    if (currentIdx >= storage.Length) break;
                    
                    int col = currentIdx % gridColumns;
                    int row = currentIdx / gridColumns;
                    Vector2 position = new Vector2((col + offset) * tileSize, (row + offset) * tileSize);
                    
                    // Update the block's position
                    if (storage[currentIdx] is IBlock blockSprite)
                    {
                        blockSprite.SetPosition(position);
                        
                        // Add solid blocks to collidable objects
                        if (blockSprite.IsSolid())
                        {
                            collidableObjects.Add(blockSprite);
                        }
                    }
                    
                    currentIdx++;
                }
                if (currentIdx >= storage.Length) break;
            }
        }
        public void Draw(SpriteBatch sprite, GraphicsDevice graphics)
        {
            sprite.Draw(border, new Vector2(0, 0), new Rectangle(0, 0, graphics.Viewport.Width, graphics.Viewport.Height), Color.White, 0f, Vector2.Zero, 3.0f, SpriteEffects.None, 0f);

            const int gridColumns = 12;
            const int gridRows = 7;
            const int tileSize = 48;
            const int offset = 2;

            storageIdx = 0;
            string[] lines = File.ReadAllLines(path);

            int maxCells = Math.Min(storage.Length, gridColumns * gridRows);

            foreach (string line in lines )
            {
                string[] columns = line.Split(',');
                foreach (string block in columns)
                {
                    switch (block)
                    {
                        case "Tile":
                            storage[storageIdx] = blocks.BuildTileBlock(sprite);
                            break;
                        case "ChiseledTile":
                            storage[storageIdx] = blocks.BuildChiseledTileBlock(sprite);
                            break;
                        case "Fish":
                            storage[storageIdx] = blocks.BuildFishBlock(sprite);
                            break;
                        case "Dragon":
                            storage[storageIdx] = blocks.BuildDragonBlock(sprite);
                            break;
                        case "Void":
                            storage[storageIdx] = blocks.BuildVoidBlock(sprite);
                            break;
                        case "Dirt":
                            storage[storageIdx] = blocks.BuildDirtBlock(sprite);    
                            break;
                        case "Solid":
                            storage[storageIdx] = blocks.BuildSolidBlock(sprite);
                            break;
                        case "Stair":
                            storage[storageIdx] = blocks.BuildStairBlock(sprite);
                            break;
                        case "Brick":
                            storage[storageIdx] = blocks.BuildBrickBlock(sprite);
                            break;
                        case "Grate":
                            storage[storageIdx] = blocks.BuildGrateBlock(sprite);
                            break;
                    }

                    int col = storageIdx % gridColumns;
                    int row = storageIdx / gridColumns;
                    Vector2 position = new Vector2((col + offset) * tileSize, (row + offset) * tileSize);

                    // Draw the block at the correct position
                    storage[storageIdx].Draw(sprite, position);
                    
                    storageIdx++;
                }

                if (storageIdx >= maxCells) break;
            }
        }
    }
}
