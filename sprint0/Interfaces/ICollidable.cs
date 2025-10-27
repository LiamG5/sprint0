using Microsoft.Xna.Framework;
using sprint0.Collisions;

namespace sprint0.Interfaces
{
    public interface ICollidable
    {
        public Rectangle GetBounds();
        public bool IsSolid();
        public Vector2 GetPosition();
        public void OnCollision(ICollidable other, CollisionDirection direction);
    }
}
