using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using sprint0.Interfaces;
using sprint0.Classes;
using System;
using sprint0.Collisions;
using Microsoft.Xna.Framework.Content;

namespace sprint0.Sprites
{
    public class BlockChiseledTile : IBlock, ISprite {

        private Texture2D blockSS;
        private static int blockType = 1;
        private static Vector2 blockPos = new Vector2(100, 100);
        private static Rectangle block = new Rectangle(16 * blockType, 0, 16, 16);
        private Vector2 position;
        private Vector2 velocity;

        public BlockChiseledTile (Texture2D sheet, Vector2 pos)
        {
            blockSS = sheet;
            position = pos;
            velocity = new Vector2(0, 0);
        }
        public void Update(GameTime gameTime)
        {
            position += velocity;
            velocity = Vector2.Zero;
        }
        public void Draw(SpriteBatch spriteBatch, Vector2 position)
        {
            spriteBatch.Draw(blockSS, position, block, Color.White, 0f, Vector2.Zero, 3.0f, SpriteEffects.None, 0f);
        }
        
        public Rectangle GetBounds()
        {
            return new Rectangle((int)position.X, (int)position.Y, 48, 48);
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
            switch (other)
            {
                case Link link:
                    switch (direction)
                    {
                        case Collisions.CollisionDirection.Up:
                            velocity = new Vector2(0, 5);
                            break;
                        case Collisions.CollisionDirection.Down:
                            velocity = new Vector2(0, -5);
                            break;
                        case Collisions.CollisionDirection.Left:
                            velocity = new Vector2(5, 0);
                            break;
                        case Collisions.CollisionDirection.Right:
                            velocity = new Vector2(-5, 0);
                            break;
                        default:
                            break;
                    }
                    break;
                case IBlock block:
                    if (block.IsSolid()) 
                    {
                        Vector2 newVelocity = velocity;
                        Vector2 resolvedPosition = new Collisions.CollisionResponse().ResolveCollisionDirection(this.GetBounds(), block.GetBounds(), direction);
                        if (direction == Collisions.CollisionDirection.Left || direction == Collisions.CollisionDirection.Right) 
                        {
                            newVelocity.X = 0;
                        } 
                        else if (direction == Collisions.CollisionDirection.Up || direction == Collisions.CollisionDirection.Down) 
                        {
                            newVelocity.Y = 0;
                        }
                        velocity = newVelocity;
                    }
                    break;
            }
        }
    }
}
