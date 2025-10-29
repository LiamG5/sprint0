using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using sprint0.Interfaces;
using sprint0.Classes;
using System;
using Microsoft.Xna.Framework.Content;

namespace sprint0.Sprites
{
    public class DungeonTallWall : ICollidable {


        private Vector2 position;
        private Rectangle rectangle;

        public DungeonTallWall(Vector2 pos)
        {
            position = pos;
            rectangle = new Rectangle((int)position.X, (int)position.Y, 24, 222);
            
        }
        
        public Rectangle GetBounds()
        {
            return rectangle;
        }
        
        public bool IsSolid()
        {
            return true;
        }   
        
        public Vector2 GetPosition()
        {
            return position;
        }
        
        public void OnCollision(sprint0.Interfaces.ICollidable other, sprint0.Collisions.CollisionDirection direction)
        {
        }
    }
}
