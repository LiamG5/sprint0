using Microsoft.Xna.Framework;
using sprint0.Classes;

namespace sprint0.Sprites.Projectiles
{
    public class ProjectileSwordBeam
    {
        private const int speed = 8;
        private const int damage = 2;

        private static Rectangle RightSourceRect = new Rectangle(85, 225, 22, 22);
        private static Rectangle LeftSourceRect = new Rectangle(26, 225, 22, 22);
        private static Rectangle UpSourceRect = new Rectangle(60, 220, 22, 22);
        private static Rectangle DownSourceRect = new Rectangle(0, 220, 22, 22);

        public static Projectile Create(Vector2 position, Link.Direction direction)
        {
            Rectangle sourceRect = new Rectangle(0, 0, 0, 0);
            Vector2 velocity = new Vector2(0, 0);
            int collisionWidth = 16;
            int collisionHeight = 16;
            
            switch (direction)
            {
                case Link.Direction.Up:
                    sourceRect = UpSourceRect;
                    velocity = new Vector2(0, -speed);
                    collisionWidth = 8;
                    break;
                case Link.Direction.Down:
                    sourceRect = DownSourceRect;
                    velocity = new Vector2(0, speed);
                    collisionWidth = 8;
                    break;
                case Link.Direction.Left:
                    sourceRect = LeftSourceRect;
                    velocity = new Vector2(-speed, 0);
                    collisionHeight = 8;
                    break;
                case Link.Direction.Right:
                    sourceRect = RightSourceRect;
                    velocity = new Vector2(speed, 0);
                    collisionHeight = 8;
                    break;
            }
            
            return new Projectile(
                Texture2DStorage.GetLinkSpriteSheet(),
                sourceRect,
                position,
                velocity,
                damage,
                isEnemyProjectile: false,
                Microsoft.Xna.Framework.Graphics.SpriteEffects.None,
                collisionWidth,
                collisionHeight
            );
        }
    }
}

