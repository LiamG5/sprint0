using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using sprint0.Factories;
using sprint0.Interfaces;
using sprint0.Managers;
using sprint0.Sprites;
using sprint0.Sprites.Dungeon;
using sprint0.Sprites.Projectiles;
using System;
using System.Collections.Generic;
using System.Linq;

namespace sprint0.Loaders
{
    public class DungeonLoader
    {
        private ISprite[] storage;
        private string path;
        private int storageIdx = 0;
        private BlockFactory blocks = BlockFactory.Instance;
        private ItemDroper itemDroper;
        private List<Rectangle> rectangles;
        private List<IBlock> blockObjects;
        private List<IEnemy> enemies;
        private List<Projectile> projectiles;
        private List<BoomerangProjectile> boomerangProjectiles;
        private List<BombProjectile> bombProjectiles;
        private List<IItem> items;
        private List<ICollidable> boarders;
        private List<TransitionZone> transitionZones;
        private int roomId;
        private RoomManager roomManager;
        private ItemLoader itemLoader;
        private EnemyLoader enemyLoader;

        public Texture2D border;

        public DungeonLoader(BlockFactory blocks, ItemLoader itemLoader, EnemyLoader enemyLoader, string csvContent)
        {
            this.blocks = blocks;
            path = csvContent;
            this.itemLoader = itemLoader;
            this.enemyLoader = enemyLoader;
            int totalCells = 84;
            storage = new ISprite[totalCells];
            storageIdx = 0;
            rectangles = new List<Rectangle>();
            blockObjects = new List<IBlock>();
            enemies = new List<IEnemy>();
            projectiles = new List<Projectile>();
            boomerangProjectiles = new List<BoomerangProjectile>();
            bombProjectiles = new List<BombProjectile>();
            items = new List<IItem>();
            border = Texture2DStorage.GetDungeonBorder();
            boarders = new List<ICollidable>();
            transitionZones = new List<TransitionZone>();
            roomId = 8;
            itemDroper = new ItemDroper();
            
        }



        public void SetRoomManager(RoomManager manager, int currentRoomId)
        {
            roomManager = manager;
            roomId = currentRoomId;

            CreateTransitionZones();
        }

        private void CreateTransitionZones()
        {
            if (roomManager == null) return;

            transitionZones.Clear();

            // Helper: make a solid blocking rectangle when no neighbor exists
            void AddBlockingRect(Rectangle rect)
            {
                // Use an invisible TransitionZone that does nothing (solid block)
                transitionZones.Add(new TransitionZone(rect, TransitionDirection.None, roomManager, isBlocking: true));
            }

            int northRoom = roomManager.GetConnectedRoom(roomId, TransitionDirection.North);
            if (northRoom != -1)
            {
                transitionZones.Add(new TransitionZone(new Rectangle(336, 0, 50, 50),
                    TransitionDirection.North, roomManager));
            }
            else
            {
                AddBlockingRect(new Rectangle(346, 24, 50, 50));
            }

            int southRoom = roomManager.GetConnectedRoom(roomId, TransitionDirection.South);
            if (southRoom != -1)
            {
                transitionZones.Add(new TransitionZone(new Rectangle(346, 454, 50, 50),
                    TransitionDirection.South, roomManager));
            }
            else
            {
                AddBlockingRect(new Rectangle(380, 432, 50, 50));
            }

            int westRoom = roomManager.GetConnectedRoom(roomId, TransitionDirection.West);
            if (westRoom != -1)
            {
                transitionZones.Add(new TransitionZone(new Rectangle(6, 248, 50, 50),
                    TransitionDirection.West, roomManager));
            }
            else
            {
                AddBlockingRect(new Rectangle(24, 248, 50, 50));
            }

            int eastRoom = roomManager.GetConnectedRoom(roomId, TransitionDirection.East);
            if (eastRoom != -1)
            {
                transitionZones.Add(new TransitionZone(new Rectangle(700, 248, 50, 50),
                    TransitionDirection.East, roomManager));
            }
            else
            {
                AddBlockingRect(new Rectangle(682, 248, 50, 50));
            }
        }




        public void LoadRectangles()
        {
            const int gridColumns = 12;
            const int tileSize = 48;
            const int offset = 2;

            rectangles.Clear();
            blockObjects.Clear();
            boarders.Clear(); // Clear borders before rebuilding
            
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
                        case "MovableTile":
                            block = blocks.BuildMovableTileBlock(null, position) as IBlock;
                            break;
                        case "InvisibleBlock":
                            block = blocks.BuildInvisibleBlock(null, position) as IBlock;
                            break;
                    }

                    if (block != null)
                    {
                        blockObjects.Add(block);
                        if (block.BlocksMovement())
                        {
                            rectangles.Add(new Rectangle((int)position.X, (int)position.Y, 48, 48));
                        }
                    }

                    cellIndex++;
                }
                items = itemLoader.GetItems();
                enemies = enemyLoader.GetEnemies();

            }
            
            // Add wall borders (these are the outer walls that always exist)
            for (int i = 0; i < 2; i++)
            {
                boarders.Add(new DungeonLongWall(new Vector2(0 + i * 424, 48)));
                boarders.Add(new DungeonLongWall(new Vector2(0 + i * 424, 432)));
                boarders.Add(new DungeonTallWall(new Vector2(48 + i * 634, 0)));
                boarders.Add(new DungeonTallWall(new Vector2(48 + i * 634, 294)));
            }
            
            // Add solid walls in doorway locations where there is NO connection
            // This makes doors behave like walls when there's no neighboring room
            if (roomManager != null)
            {
                // North door/wall
                if (roomManager.GetConnectedRoom(roomId, TransitionDirection.North) == -1)
                {
                    boarders.Add(new SolidWallBlock(new Vector2(336, 24), 96, 48));
                    Console.WriteLine($"[DungeonLoader] Added North wall for Room {roomId} (no connection)");
                }
                else
                {
                    Console.WriteLine($"[DungeonLoader] North doorway open for Room {roomId}");
                }
                
                // South door/wall
                if (roomManager.GetConnectedRoom(roomId, TransitionDirection.South) == -1)
                {
                    boarders.Add(new SolidWallBlock(new Vector2(336, 432), 96, 48));
                    Console.WriteLine($"[DungeonLoader] Added South wall for Room {roomId} (no connection)");
                }
                else
                {
                    Console.WriteLine($"[DungeonLoader] South doorway open for Room {roomId}");
                }
                
                // West door/wall
                if (roomManager.GetConnectedRoom(roomId, TransitionDirection.West) == -1)
                {
                    boarders.Add(new SolidWallBlock(new Vector2(24, 192), 48, 96));
                    Console.WriteLine($"[DungeonLoader] Added West wall for Room {roomId} (no connection)");
                }
                else
                {
                    Console.WriteLine($"[DungeonLoader] West doorway open for Room {roomId}");
                }
                
                // East door/wall
                if (roomManager.GetConnectedRoom(roomId, TransitionDirection.East) == -1)
                {
                    boarders.Add(new SolidWallBlock(new Vector2(682, 192), 48, 96));
                    Console.WriteLine($"[DungeonLoader] Added East wall for Room {roomId} (no connection)");
                }
                else
                {
                    Console.WriteLine($"[DungeonLoader] East doorway open for Room {roomId}");
                }
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
            foreach (var enmey in enemies)
            {
                if (enmey is IEnemy sprite)
                {
                    sprite.Update(gameTime);
                }
            }
            itemLoader.Update(gameTime);
            enemyLoader.Update();
            foreach (var boomerang in boomerangProjectiles)
            {
                boomerang.Update(gameTime);
            }
            foreach (var bomb in bombProjectiles)
            {
                bomb.Update(gameTime);
            }
        }
        
        public void Draw(SpriteBatch sprite, GraphicsDevice graphics)
        {
            if (border != null)
            {
                // Calculate game world height (viewport height minus HUD height)
                int gameWorldHeight = graphics.Viewport.Height - HUD.HudConstants.HudHeight;
                sprite.Draw(border, new Vector2(0, 0), new Rectangle(0, 0, graphics.Viewport.Width, gameWorldHeight), Color.White, 0f, Vector2.Zero, 3.0f, SpriteEffects.None, 0f);
            }

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

        public void AddBoomerangProjectile(BoomerangProjectile boomerang)
        {
            boomerangProjectiles.Add(boomerang);
        }
        
        public void RemoveBoomerangProjectile(BoomerangProjectile boomerang)
        {
            boomerangProjectiles.Remove(boomerang);
        }

        public List<BoomerangProjectile> GetBoomerangProjectiles()
        {
            return boomerangProjectiles;
        }

        public void AddBombProjectile(BombProjectile bomb)
        {
            bombProjectiles.Add(bomb);
        }
        
        public void RemoveBombProjectile(BombProjectile bomb)
        {
            bombProjectiles.Remove(bomb);
        }

        public List<BombProjectile> GetBombProjectiles()
        {
            return bombProjectiles;
        }
        
        public void CleanupDeadEntities()
        {
            enemies.RemoveAll(e => e.IsDead());
            projectiles.RemoveAll(p => p.ShouldDestroy);
            boomerangProjectiles.RemoveAll(b => b.ShouldDestroy);
            bombProjectiles.RemoveAll(b => b.ShouldDestroy);
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