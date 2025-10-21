using Microsoft.Xna.Framework;
using sprint0.Interfaces;

namespace sprint0.Classes
{
    public class CollisionResponse
    {
        // Collision response system - assumes collision detection is handled elsewhere
        // This class only provides response logic when a collision is detected
        
        public Vector2 ResolveCollision(Vector2 oldPosition, Vector2 newPosition, Rectangle playerBounds, Rectangle collidableBounds)
        {
            Vector2 resolvedPosition = oldPosition;
            
            // Calculate overlap in each direction
            float overlapLeft = (newPosition.X + playerBounds.Width) - collidableBounds.X;
            float overlapRight = (collidableBounds.X + collidableBounds.Width) - newPosition.X;
            float overlapTop = (newPosition.Y + playerBounds.Height) - collidableBounds.Y;
            float overlapBottom = (collidableBounds.Y + collidableBounds.Height) - newPosition.Y;
            
            // Find the minimum overlap to determine which side to push away from
            float minOverlap = MathHelper.Min(MathHelper.Min(overlapLeft, overlapRight), 
                                            MathHelper.Min(overlapTop, overlapBottom));
            
            // Apply collision response based on the minimum overlap
            if (minOverlap == overlapLeft)
            {
                // Collision from the left, push player to the left
                resolvedPosition.X = collidableBounds.X - playerBounds.Width;
            }
            else if (minOverlap == overlapRight)
            {
                // Collision from the right, push player to the right
                resolvedPosition.X = collidableBounds.X + collidableBounds.Width;
            }
            else if (minOverlap == overlapTop)
            {
                // Collision from the top, push player up
                resolvedPosition.Y = collidableBounds.Y - playerBounds.Height;
            }
            else if (minOverlap == overlapBottom)
            {
                // Collision from the bottom, push player down
                resolvedPosition.Y = collidableBounds.Y + collidableBounds.Height;
            }
            
            return resolvedPosition;
        }
        
        // Helper method to calculate collision response for a specific direction
        public Vector2 ResolveCollisionDirection(Vector2 oldPosition, Vector2 newPosition, Rectangle playerBounds, Rectangle collidableBounds, Vector2 direction)
        {
            Vector2 resolvedPosition = newPosition;
            
            // Simple directional collision response
            if (direction.X > 0) // Moving right
            {
                resolvedPosition.X = collidableBounds.X - playerBounds.Width;
            }
            else if (direction.X < 0) // Moving left
            {
                resolvedPosition.X = collidableBounds.X + collidableBounds.Width;
            }
            
            if (direction.Y > 0) // Moving down
            {
                resolvedPosition.Y = collidableBounds.Y - playerBounds.Height;
            }
            else if (direction.Y < 0) // Moving up
            {
                resolvedPosition.Y = collidableBounds.Y + collidableBounds.Height;
            }
            
            return resolvedPosition;
        }
    }
}
