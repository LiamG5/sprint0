using Microsoft.Xna.Framework;
using sprint0.Interfaces;

namespace sprint0.Collisions
{
    public class CollisionResponse
    {
        public Vector2 ResolveCollisionDirection(Rectangle objA, Rectangle objB, CollisionDirection direction)
        {
            Vector2 resolvedPosition = new Vector2(objA.Left, objA.Top);
            
            if (direction == CollisionDirection.Right)
            {
                resolvedPosition.X = objB.X - objA.Width;
            }
            else if (direction == CollisionDirection.Left)
            {
                resolvedPosition.X = objB.X + objB.Width;
            }
            
            if (direction == CollisionDirection.Up)
            {
                resolvedPosition.Y = objB.Y + objB.Height;
            }
            else if (direction == CollisionDirection.Down)
            {
                resolvedPosition.Y = objB.Y - objA.Height;
            }
            
            return resolvedPosition;
        }
    }
}
