using System.Collections.Generic;
using System.Linq;
using sprint0.Classes;
using sprint0.Interfaces;
using sprint0.Sprites;
using Rectangle = Microsoft.Xna.Framework.Rectangle;
using Microsoft.Xna.Framework;


namespace sprint0.Collisions
{
    public class CollisionUpdater
    {
        private readonly CollisionDetection collisionDetection;
        private DungeonLoader dungeonLoader;
        private Link link;
        private List<ICollidable> allCollidables;

        public CollisionUpdater(DungeonLoader dungeonLoader, Link link)
        {
            collisionDetection = new CollisionDetection();
            this.link = link;
            this.dungeonLoader = dungeonLoader;
            allCollidables = new List<ICollidable>();
        }

        public void getList()
        {
            allCollidables.Clear();
            allCollidables.Add(link);
            allCollidables.AddRange(dungeonLoader.GetBlocks());
            allCollidables.AddRange(dungeonLoader.GetEnemies());
            allCollidables.AddRange(dungeonLoader.GetProjectiles());
            allCollidables.AddRange(dungeonLoader.GetItems());

        }


        public void Update()
        {
            dungeonLoader.CleanupDeadEntities();
            
            allCollidables.Clear();
            allCollidables.Add(link);
            allCollidables.AddRange(dungeonLoader.GetBlocks());
            allCollidables.AddRange(dungeonLoader.GetEnemies());
            allCollidables.AddRange(dungeonLoader.GetProjectiles());
            allCollidables.AddRange(dungeonLoader.GetItems());
            allCollidables.AddRange(dungeonLoader.GetBoarders());
            
            for (int i = 0; i < allCollidables.Count; i++)
            {
                for (int j = i + 1; j < allCollidables.Count; j++)
                {
                    ICollidable objA = allCollidables[i];
                    ICollidable objB = allCollidables[j];
                    
                    Rectangle boundsA = objA.GetBounds();
                    Rectangle boundsB = objB.GetBounds();
                    
                    if (boundsB.Left >= boundsA.Right)
                        continue;
                    
                    CollisionDirection direction = collisionDetection.GetCollision(boundsA, boundsB);
                    
                    if (direction != CollisionDirection.None)
                    {
                        objA.OnCollision(objB, direction);
                        objB.OnCollision(objA, GetOppositeDirection(direction));
                    }
                }
            }
        }
        
        private CollisionDirection GetOppositeDirection(CollisionDirection direction)
        {
            return direction switch
            {
                CollisionDirection.Up => CollisionDirection.Down,
                CollisionDirection.Down => CollisionDirection.Up,
                CollisionDirection.Left => CollisionDirection.Right,
                CollisionDirection.Right => CollisionDirection.Left,
                _ => CollisionDirection.None
            };
        }
    }
}