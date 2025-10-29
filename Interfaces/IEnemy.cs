using Microsoft.Xna.Framework;

namespace sprint0.Interfaces
{
    public interface IEnemy : ISprite, ICollidable
    {
        void TakeDamage();
        bool IsDead();
    }
}

