using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using sprint0.Interfaces;
using sprint0.Classes;
using System;
using sprint0.Collisions;
using Microsoft.Xna.Framework.Content;

namespace sprint0.Sprites
{
    public class BlockMovableTile : IBlock, ISprite {

        private Texture2D blockSS;
        private static int blockType = 1;
        private static Vector2 blockPos = new Vector2(100, 100);
        private static Rectangle block = new Rectangle(16 * blockType, 0, 16, 16);
        private Vector2 position;
        private bool hasBeenMoved;
        private Vector2 targetPosition;
        private bool isMoving;

        public BlockMovableTile (Texture2D sheet, Vector2 pos)
        {
            blockSS = sheet;
            position = pos;
            hasBeenMoved = false;
            isMoving = false;
            targetPosition = pos;
        }
        public void Update(GameTime gameTime)
        {
            if (isMoving)
            {
                Vector2 direction = targetPosition - position;
                float distance = direction.Length();
                
                if (distance > 2f)
                {
                    direction.Normalize();
                    position += direction * 3f;
                }
                else
                {
                    position = targetPosition;
                    isMoving = false;
                }
            }
        }
        public void Draw(SpriteBatch spriteBatch, Vector2 position)
        {
            spriteBatch.Draw(blockSS, position, block, Color.White, 0f, Vector2.Zero, 3.0f, SpriteEffects.None, 0f);
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
            switch (other)
            {
                case Link link:
                    if (direction == Collisions.CollisionDirection.Down && !hasBeenMoved && !isMoving)
                    {
                        targetPosition = new Vector2(position.X, position.Y - 48);
                        isMoving = true;
                        hasBeenMoved = true;
                    }
                    break;
                case ICollidable block:
                    if (block.BlocksMovement() && isMoving) 
                    {
                        if (direction == Collisions.CollisionDirection.Up) 
                        {
                            isMoving = false;
                            hasBeenMoved = true;
                        }
                    }
                    break;
            }
        }
    }
}

