using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using sprint0.Interfaces;
using sprint0.Classes;
using System;
using Microsoft.Xna.Framework.Content;

namespace sprint0.Sprites
{
    public class InvisibleBlock : IBlock, ISprite {

        private Vector2 position;

        public InvisibleBlock(Vector2 pos)
        {
            position = pos;
        }
        
        public void Update(GameTime gameTime)
        {
            
        }
        
        public void Draw(SpriteBatch spriteBatch, Vector2 position)
        {
        }
        
        public Rectangle GetBounds()
        {
            return new Rectangle((int)position.X, (int)position.Y, 48, 48);
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
        
        public void OnCollision(sprint0.Interfaces.ICollidable other, sprint0.Collisions.CollisionDirection direction)
        {
        }
    }
}

