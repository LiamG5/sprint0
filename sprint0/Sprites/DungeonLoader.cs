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
        private List<IBlock> blockObjects;
        private List<IEnemy> enemies;
        private List<Projectile> projectiles;
        private List<IItem> items;

        public Texture2D border;
        public DungeonLoader(BlockFactory blocks, string csvContent)
        {
            this.blocks = blocks;
            this.path = csvContent;
            int totalCells = 84;
            storage = new ISprite[totalCells];
            storageIdx = 0;
            this.rectangles = new List<Rectangle>();
            this.blockObjects = new List<IBlock>();
            this.enemies = new List<IEnemy>();
            this.projectiles = new List<Projectile>();
            this.items = new List<IItem>();
            this.border = Texture2DStorage.GetDungeonBorder();
        }

        public void LoadRectangles()
        {
            const int gridColumns = 12;
            const int tileSize = 48;
            const int offset = 2;

            rectangles.Clear();
            blockObjects.Clear();
            string[] lines = path.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);

            int cellIndex = 0;
            foreach (string line in lines)
            {
                string[] columns = line.Split(',');
                foreach (string blockType in columns)
                {
                    int col = cellIndex % gridColumns;
                    int row = cellIndex / gridColumns;
                    Vector2 position = new Vector2((col + offset) * tileSize, (row + offset) * tileSize);
                    
                    // Create block object based on type
                    IBlock block = null;
                    switch (blockType)
                    {
                        case "Tile":
                            block = blocks.BuildTileBlock(null, position) as IBlock;
                            break;
                        case "ChiseledTile":
                            block = blocks.BuildChiseledTileBlock(null, position) as IBlock;
                            break;
                        case "Fish":
                            block = blocks.BuildFishBlock(null, position) as IBlock;
                            break;
                        case "Dragon":
                            block = blocks.BuildDragonBlock(null, position) as IBlock;
                            break;
                        case "Void":
                            block = blocks.BuildVoidBlock(null, position) as IBlock;
                            break;
                        case "Dirt":
                            block = blocks.BuildDirtBlock(null, position) as IBlock;
                            break;
                        case "Solid":
                            block = blocks.BuildSolidBlock(null, position) as IBlock;
                            break;
                        case "Stair":
                            block = blocks.BuildStairBlock(null, position) as IBlock;
                            break;
                        case "Brick":
                            block = blocks.BuildBrickBlock(null, position) as IBlock;
                            break;
                        case "Grate":
                            block = blocks.BuildGrateBlock(null, position) as IBlock;
                            break;
                    }
                    
                    if (block != null && block.IsSolid())
                    {
                        rectangles.Add(new Rectangle((int)position.X, (int)position.Y, 48, 48));
                        blockObjects.Add(block);
                    }
                    
                    cellIndex++;
                }
            }
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
		
		public List<IBlock> GetBlocks()
		{
			return blockObjects;
		}
		
		public List<IEnemy> GetEnemies()
		{
			return enemies;
		}
		
		public List<Projectile> GetProjectiles()
		{
			return projectiles;
		}
		
		public void AddEnemy(IEnemy enemy)
		{
			enemies.Add(enemy);
		}
		
		public void RemoveEnemy(IEnemy enemy)
		{
			enemies.Remove(enemy);
		}
		
		public void AddProjectile(Projectile projectile)
		{
			projectiles.Add(projectile);
		}
		
		public void RemoveProjectile(Projectile projectile)
		{
			projectiles.Remove(projectile);
		}
		
	public void CleanupDeadEntities()
	{
		enemies.RemoveAll(e => e.IsDead());
		projectiles.RemoveAll(p => p.ShouldDestroy);
		items.RemoveAll(i => i.IsCollected());
	}
	
	public List<IItem> GetItems()
	{
		return items;
	}
	
	public void AddItem(IItem item)
	{
		items.Add(item);
	}
	
	public void RemoveItem(IItem item)
	{
		items.Remove(item);
	}
    }
}
