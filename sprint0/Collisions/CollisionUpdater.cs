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
        private readonly CollisionHandler collisionHandler;
        private List<Rectangle> rectangles;
        private CollisionResponse collisionResponse;
        private List<CollisionDirection> collisions;
        private DungeonLoader dungeonLoader;
        private Link link;
       

        public CollisionUpdater(DungeonLoader dungeonLoader, Link link)
        {
            collisionHandler = new CollisionHandler();
            collisionResponse = new CollisionResponse(); 
            rectangles = new List<Rectangle>();
            collisions = new List<CollisionDirection>();
            this.link = link;
            this.dungeonLoader = dungeonLoader;

        }

        public void getList()
        {
            rectangles.Add(link.GetBounds());
            rectangles.AddRange(dungeonLoader.GetBlockList());

        }


        public void Update()
        {
            rectangles[0] = link.GetBounds();
            collisions = collisionHandler.CollisionDetect(rectangles);
            for (int i = 0; i < collisions.Count; i++)
            {   
                if (collisions[i] != CollisionDirection.None)
                {
                    Vector2 resolvedPosition = collisionResponse.ResolveCollisionDirection(rectangles[0], rectangles[i + 1], collisions[i]);
                    link.HandleCollisionResponse(resolvedPosition);
                    
                    Vector2 velocity = link.velocity;
                    if (collisions[i] == CollisionDirection.Left || collisions[i] == CollisionDirection.Right)
                    {
                        velocity.X = 0;
                    }
                    if (collisions[i] == CollisionDirection.Up || collisions[i] == CollisionDirection.Down)
                    {
                        velocity.Y = 0;
                    }
                    link.velocity = velocity;
                    
                    rectangles[0] = link.GetBounds(); 
                }
            }

        }
        
        

    }
}