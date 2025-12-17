using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using sprint0.Interfaces;
using sprint0.Classes;
using System;
using Microsoft.Xna.Framework.Content;

namespace sprint0.Sprites.Dungeon
{
    public class DungeonDoor : ICollidable 
    {
        private Vector2 position;
        private Rectangle rectangle;
        private bool doorThere = false;
        private bool doorOpen = false;

        public DungeonDoor(Vector2 pos)
        {
            position = pos;
            rectangle = new Rectangle((int)position.X, (int)position.Y, 48, 48);
        }
        
        public Rectangle GetBounds()
        {
            return rectangle;
        }
        public bool BlocksMovement()
        {
            return true;
        }
        
        public bool BlocksProjectiles()
        {
            return true;
        }   
        
        public Vector2 GetPosition()
        {
            return position;
        }
        
        public void OnCollision(ICollidable other, Collisions.CollisionDirection direction)
        {
        }
    }
}