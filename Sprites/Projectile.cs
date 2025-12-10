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
        private SpriteEffects spriteEffects;
        private const int PROJECTILE_WIDTH = 16;
        private const int PROJECTILE_HEIGHT = 16;
        private float scale = 3.0f;

        public Projectile(Texture2D texture, Rectangle sourceRect, Vector2 startPosition, 
                         Vector2 velocity, int damage, bool isEnemyProjectile)
            : this(texture, sourceRect, startPosition, velocity, damage, isEnemyProjectile, SpriteEffects.None)
        {
        }

        public Projectile(Texture2D texture, Rectangle sourceRect, Vector2 startPosition, 
                         Vector2 velocity, int damage, bool isEnemyProjectile, SpriteEffects spriteEffects)
        {
            this.texture = texture;
            this.sourceRectangle = sourceRect;
            this.position = startPosition;
            this.velocity = velocity;
            this.damage = damage;
            this.isEnemyProjectile = isEnemyProjectile;
            this.shouldDestroy = false;
            this.spriteEffects = spriteEffects;
        }

        public bool IsEnemyProjectile => isEnemyProjectile;
        public bool ShouldDestroy => shouldDestroy;

        public void Update(GameTime gameTime)
        {
            position += velocity;
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 drawPosition)
        {
            if (!shouldDestroy && texture != null && sourceRectangle.Width > 0 && sourceRectangle.Height > 0)
            {
                spriteBatch.Draw(texture, drawPosition, sourceRectangle, 
                               Color.White, 0f, Vector2.Zero, scale, 
                               spriteEffects, 0f);
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

        public bool BlocksMovement()
        {
            return false;
        }
        
        public bool BlocksProjectiles()
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
                    link.TakeDamage(damage);
                    shouldDestroy = true;
                    break;

                case Link link when !isEnemyProjectile:
                    break;

                case IEnemy enemy when !isEnemyProjectile:
                    enemy.TakeDamage(damage);
                    shouldDestroy = true;
                    break;

                case IBlock block when block.BlocksProjectiles():
                    shouldDestroy = true;
                    break;

                case ICollidable wall when wall.BlocksProjectiles():
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

