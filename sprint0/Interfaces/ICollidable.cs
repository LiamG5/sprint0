using Microsoft.Xna.Framework;

namespace sprint0.Interfaces
{
    public interface ICollidable
    {
        Rectangle GetBounds();
        bool IsSolid();
        Vector2 GetPosition();
    }
}
