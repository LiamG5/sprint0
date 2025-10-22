using Microsoft.Xna.Framework;

namespace sprint0.Interfaces
{
    public interface ICollidable
    {
        public Rectangle GetBounds();
        public bool IsSolid();
        public Vector2 GetPosition();

    }
}
