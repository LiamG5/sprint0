using Microsoft.Xna.Framework;
using sprint0.Interfaces;

namespace sprint0.Collisions
{
    public class CollisionResponse
    {
        
        public Vector2 ResolveCollision(Vector2 oldPosition, Vector2 newPosition, Rectangle playerBounds, Rectangle collidableBounds)
        {
            Vector2 resolvedPosition = oldPosition;
            
            float overlapLeft = (newPosition.X + playerBounds.Width) - collidableBounds.X;
            float overlapRight = (collidableBounds.X + collidableBounds.Width) - newPosition.X;
            float overlapTop = (newPosition.Y + playerBounds.Height) - collidableBounds.Y;
            float overlapBottom = (collidableBounds.Y + collidableBounds.Height) - newPosition.Y;
            
            float minOverlap = MathHelper.Min(MathHelper.Min(overlapLeft, overlapRight), 
                                            MathHelper.Min(overlapTop, overlapBottom));
            
            if (minOverlap == overlapLeft)
            {
                resolvedPosition.X = collidableBounds.X - playerBounds.Width;
            }
            else if (minOverlap == overlapRight)
            {
                resolvedPosition.X = collidableBounds.X + collidableBounds.Width;
            }
            else if (minOverlap == overlapTop)
            {
                resolvedPosition.Y = collidableBounds.Y - playerBounds.Height;
            }
            else if (minOverlap == overlapBottom)
            {
                resolvedPosition.Y = collidableBounds.Y + collidableBounds.Height;
            }
            
            return resolvedPosition;
        }
        
        public Vector2 ResolveCollisionDirection(Rectangle playerBounds, Rectangle collidableBounds, CollisionDirection direction)
        {
            Vector2 resolvedPosition = new Vector2(playerBounds.Left, playerBounds.Top);
            
            if (direction == CollisionDirection.Right)
            {
                resolvedPosition.X = collidableBounds.X - playerBounds.Width;
            }
            else if (direction == CollisionDirection.Left)
            {
                resolvedPosition.X = collidableBounds.X + collidableBounds.Width;
            }
            
            if (direction == CollisionDirection.Up)
            {
                resolvedPosition.Y = collidableBounds.Y + collidableBounds.Height;
            }
            else if (direction == CollisionDirection.Down)
            {
                resolvedPosition.Y = collidableBounds.Y - playerBounds.Height;
            }
            
            return resolvedPosition;
        }
    }
}
