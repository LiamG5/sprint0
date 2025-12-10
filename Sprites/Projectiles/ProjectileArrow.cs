using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using sprint0.Classes;
using sprint0.Sprites;

namespace sprint0.Sprites.Projectiles
{
    public class ProjectileArrow
    {
        private const int speed = 8;
        private const int damage = 1;
        
        private const int TileW = 15;
        private const int TileH = 16;
        private const int ColRight = 0; private const int RowRight = 1;
        private const int ColDown  = 0; private const int RowDown  = 0;
        private const int ColLeft  = 1; private const int RowLeft  = 0;
        private const int ColUp    = 1; private const int RowUp    = 1;

        private static readonly Rectangle ArrowRightRect = new Rectangle(40 * ColRight, 40 * RowRight, TileW, TileH);
        private static readonly Rectangle ArrowLeftRect  = new Rectangle(40 * ColLeft,  40 * RowLeft,  TileW, TileH);
        private static readonly Rectangle ArrowUpRect    = new Rectangle(40 * ColUp,    40 * RowUp,    TileW, TileH);
        private static readonly Rectangle ArrowDownRect  = new Rectangle(40 * ColDown,  40 * RowDown,  TileW, TileH);

        public static Projectile Create(Vector2 position, Link.Direction direction)
        {
            Rectangle sourceRect = ArrowRightRect;
            Vector2 velocity = new Vector2(0, 0);

            switch (direction)
            {
                case Link.Direction.Up:
                    sourceRect = ArrowUpRect;
                    velocity = new Vector2(0, -speed);
                    break;
                case Link.Direction.Down:
                    sourceRect = ArrowDownRect;
                    velocity = new Vector2(0, speed);
                    break;
                case Link.Direction.Left:
                    sourceRect = ArrowLeftRect;
                    velocity = new Vector2(-speed, 0);
                    break;
                case Link.Direction.Right:
                default:
                    sourceRect = ArrowRightRect;
                    velocity = new Vector2(speed, 0);
                    break;
            }

            return new Projectile(
                Texture2DStorage.GetItemSpriteSheet(),
                sourceRect,
                position,
                velocity,
                damage,
                isEnemyProjectile: false
            );
        }
    }
}

