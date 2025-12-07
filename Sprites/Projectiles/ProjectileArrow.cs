using Microsoft.Xna.Framework;
using sprint0.Classes;
using sprint0.Sprites;

namespace sprint0.Sprites.Projectiles
{
    public static class ProjectileArrow
    {
        private const int speed = 8;
        private const int damage = 2;

        private static Rectangle RightSourceRect = new Rectangle(0, 22, 22, 22);
        private static Rectangle LeftSourceRect = new Rectangle(22, 0 ,22, 22);
        private static Rectangle UpSourceRect = new Rectangle(22, 22, 22, 22);
        private static Rectangle DownSourceRect = new Rectangle(0, 0, 22, 22);

        public static Projectile Create(Vector2 position, Link.Direction direction)
        {
            Rectangle sourceRect = new Rectangle(0, 0, 0, 0);
            Vector2 velocity = new Vector2(0, 0);
            
            switch (direction)
            {
                case Link.Direction.Up:
                    sourceRect = UpSourceRect;
                    velocity = new Vector2(0, -speed);
                    break;
                case Link.Direction.Down:
                    sourceRect = DownSourceRect;
                    velocity = new Vector2(0, speed);
                    break;
                case Link.Direction.Left:
                    sourceRect = LeftSourceRect;
                    velocity = new Vector2(-speed, 0);
                    break;
                case Link.Direction.Right:
                    sourceRect = RightSourceRect;
                    velocity = new Vector2(speed, 0);
                    break;
            }
            
            return new Projectile(
                Texture2DStorage.GetLinkSpriteSheet(),
                sourceRect,
                position,
                velocity,
                damage,
                isEnemyProjectile: false
            );
        }
    }
}

