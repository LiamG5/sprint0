using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using sprint0.Interfaces;
using sprint0.Collisions;
using sprint0.Classes;

namespace sprint0.Sprites
{

    public class Projectile : ISprite, ICollidable
    {
        private Vector2 position;
        private Vector2 velocity;
        private Texture2D texture;
        private Rectangle sourceRectangle;
        private int damage;
        private bool isEnemyProjectile;
        private bool shouldDestroy;
        private const int PROJECTILE_WIDTH = 16;
        private const int PROJECTILE_HEIGHT = 16;
        private float scale = 2.0f;

        public Projectile(Texture2D texture, Rectangle sourceRect, Vector2 startPosition, 
                         Vector2 direction, float speed, int damage, bool isEnemyProjectile)
        {
            this.texture = texture;
            this.sourceRectangle = sourceRect;
            this.position = startPosition;
            this.damage = damage;
            this.isEnemyProjectile = isEnemyProjectile;
            this.shouldDestroy = false;

            if (direction != Vector2.Zero)
            {
                direction.Normalize();
            }
            this.velocity = direction * speed;
        }

        public bool IsEnemyProjectile => isEnemyProjectile;
        public bool ShouldDestroy => shouldDestroy;

        public void Update(GameTime gameTime)
        {
            position += velocity;
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 drawPosition)
        {
            if (!shouldDestroy)
            {
                spriteBatch.Draw(texture, drawPosition, sourceRectangle, 
                               Color.White, 0f, Vector2.Zero, scale, 
                               SpriteEffects.None, 0f);
            }
        }

        public Rectangle GetBounds()
        {
            return new Rectangle(
                (int)position.X, 
                (int)position.Y, 
                (int)(PROJECTILE_WIDTH * scale), 
                (int)(PROJECTILE_HEIGHT * scale)
            );
        }

        public bool IsSolid()
        {
            return false;
        }

        public Vector2 GetPosition()
        {
            return position;
        }

        public void OnCollision(ICollidable other, CollisionDirection direction)
        {
            switch (other)
            {
                case Link link when isEnemyProjectile:
                    link.TakeDamage();
                    shouldDestroy = true;
                    break;

                case IEnemy enemy when !isEnemyProjectile:
                    enemy.TakeDamage();
                    shouldDestroy = true;
                    break;

                case IBlock block when block.IsSolid():
                    shouldDestroy = true;
                    break;
            }
        }
 
        public void Destroy()
        {
            shouldDestroy = true;
        }
    }
}

