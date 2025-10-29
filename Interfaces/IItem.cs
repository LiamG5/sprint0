using Microsoft.Xna.Framework;

namespace sprint0.Interfaces
{
    public interface IItem : ISprite, ICollidable
    {
        bool IsCollected();
        void Collect();
    }
}

