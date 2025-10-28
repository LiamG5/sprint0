using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using sprint0.Interfaces;
using sprint0.Managers;
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
        private List<ICollidable> boarders;
        private List<TransitionZone> transitionZones;
        private int roomId;
        private RoomManager roomManager;

        public Texture2D border;
        
        public DungeonLoader(BlockFactory blocks, string csvContent, int roomId)
        {
            this.blocks = blocks;
            this.path = csvContent;
            this.roomId = roomId;
            int totalCells = 84;
            storage = new ISprite[totalCells];
            storageIdx = 0;
            this.rectangles = new List<Rectangle>();
            this.blockObjects = new List<IBlock>();
            this.enemies = new List<IEnemy>();
            this.projectiles = new List<Projectile>();
            this.items = new List<IItem>();
            this.border = Texture2DStorage.GetDungeonBorder();
            this.boarders = new List<ICollidable>();
            this.transitionZones = new List<TransitionZone>();
        }

        public void SetRoomManager(RoomManager manager, string contentRoot)
        {
            this.roomManager = manager;
            CreateTransitionZones(contentRoot);
        }

        private void CreateTransitionZones(string contentRoot)
        {
            if (roomManager == null) return;
            
            transitionZones.Clear();
            
            transitionZones.Add(new TransitionZone(
                new Rectangle(352, 0, 80, 48),
                TransitionDirection.North,
                roomId - 4,
                roomManager,
                contentRoot
            ));
            
            transitionZones.Add(new TransitionZone(
                new Rectangle(352, 432, 80, 48),
                TransitionDirection.South,
                roomId + 4,
                roomManager,
                contentRoot
            ));
            
            transitionZones.Add(new TransitionZone(
                new Rectangle(0, 200, 48, 80),
                TransitionDirection.West,
                roomId - 1,
                roomManager,
                contentRoot
            ));
            
            transitionZones.Add(new TransitionZone(
                new Rectangle(688, 200, 48, 80),
                TransitionDirection.East,
                roomId + 1,
                roomManager,
                contentRoot
            ));
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
                    
                    if (block != null)
                    {
                        blockObjects.Add(block);
                        if (block.IsSolid())
                        {
                            rectangles.Add(new Rectangle((int)position.X, (int)position.Y, 48, 48));
                        }
                    }

                    cellIndex++;
                }
            }
            for (int i = 0; i < 2; i++)
            {
                boarders.Add(new DungeonLongWall(new Vector2(0 + i * 424, 48)));
                boarders.Add(new DungeonLongWall(new Vector2(0 + i * 424, 432)));
                boarders.Add(new DungeonTallWall(new Vector2(48 + i * 634, 0)));
                boarders.Add(new DungeonTallWall(new Vector2(48 + i * 634, 294)));
            }
        }

        public void Update(GameTime gameTime)
        {
            foreach (var block in blockObjects)
            {
                if (block is ISprite sprite)
                {
                    sprite.Update(gameTime);
                }
            }
        }
        
        public void Draw(SpriteBatch sprite, GraphicsDevice graphics)
        {
            sprite.Draw(border, new Vector2(0, 0), new Rectangle(0, 0, graphics.Viewport.Width, graphics.Viewport.Height), Color.White, 0f, Vector2.Zero, 3.0f, SpriteEffects.None, 0f);

            const int gridColumns = 12;
            const int gridRows = 7;
            const int tileSize = 48;
            const int offset = 2;

            ISprite baseTile = blocks.BuildTileBlock(sprite, Vector2.Zero);
            for (int row = 0; row < gridRows; row++)
            {
                for (int col = 0; col < gridColumns; col++)
                {
                    Vector2 position = new Vector2((col + offset) * tileSize, (row + offset) * tileSize);
                    baseTile.Draw(sprite, position);
                }
            }

            List <BlockChiseledTile> chiseledTiles = new List<BlockChiseledTile>();
            foreach (var block in blockObjects)
            {
                if (block is ISprite blockSprite)
                {
                    if (!(block is BlockTile)) {
                        blockSprite.Draw(sprite, block.GetPosition());
                    }
                    if (block is BlockChiseledTile) {
                        chiseledTiles.Add(block as BlockChiseledTile);
                    }
                }
            }
            foreach (var chiseledTile in chiseledTiles) {
                chiseledTile.Draw(sprite, chiseledTile.GetPosition());
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
        
        public List<ICollidable> GetBoarders()
        {
            return boarders;
        }
        
        public List<ICollidable> GetTransitionZones()
        {
            return new List<ICollidable>(transitionZones);
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