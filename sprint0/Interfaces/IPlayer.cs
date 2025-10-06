using Microsoft.Xna.Framework;

namespace sprint0.Interfaces
{
    public interface IPlayer
    {
        void Move(Vector2 direction);
        void Stop();
        void Attack();
        void UseItem(int slot);
        void TakeDamage();
    }
}
