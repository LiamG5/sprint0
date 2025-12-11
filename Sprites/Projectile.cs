using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using sprint0.Interfaces;
using sprint0.Collisions;
using sprint0.Classes;

namespace sprint0.Sprites
{

    public class Projectile : ISprite, ICollidable
    {
        protected Vector2 position;
        protected Vector2 velocity;
        protected Texture2D texture;
        protected Rectangle sourceRectangle;
        public int damage;
        protected bool isEnemyProjectile;
        protected bool shouldDestroy;
        protected SpriteEffects spriteEffects;
        protected const int PROJECTILE_WIDTH = 16;
        protected const int PROJECTILE_HEIGHT = 16;
        protected float scale = 3.0f;
        protected int collisionWidth;
        protected int collisionHeight;

        public Projectile(Texture2D texture, Rectangle sourceRect, Vector2 startPosition, 
                         Vector2 velocity, int damage, bool isEnemyProjectile)
            : this(texture, sourceRect, startPosition, velocity, damage, isEnemyProjectile, SpriteEffects.None)
        {
        }

        public Projectile(Texture2D texture, Rectangle sourceRect, Vector2 startPosition, 
                         Vector2 velocity, int damage, bool isEnemyProjectile, SpriteEffects spriteEffects)
            : this(texture, sourceRect, startPosition, velocity, damage, isEnemyProjectile, spriteEffects, PROJECTILE_WIDTH, PROJECTILE_HEIGHT)
        {
        }

        public Projectile(Texture2D texture, Rectangle sourceRect, Vector2 startPosition, 
                         Vector2 velocity, int damage, bool isEnemyProjectile, SpriteEffects spriteEffects,
                         int collisionWidth, int collisionHeight)
        {
            this.texture = texture;
            this.sourceRectangle = sourceRect;
            this.position = startPosition;
            this.velocity = velocity;
            this.damage = damage;
            this.isEnemyProjectile = isEnemyProjectile;
            this.shouldDestroy = false;
            this.spriteEffects = spriteEffects;
            this.collisionWidth = collisionWidth;
            this.collisionHeight = collisionHeight;
        }

        public bool IsEnemyProjectile => isEnemyProjectile;
        public bool ShouldDestroy => shouldDestroy;

        public virtual void Update(GameTime gameTime)
        {
            position += velocity;
        }

        public virtual void Draw(SpriteBatch spriteBatch, Vector2 drawPosition)
        {
            if (!shouldDestroy && texture != null && sourceRectangle.Width > 0 && sourceRectangle.Height > 0)
            {
                spriteBatch.Draw(texture, drawPosition, sourceRectangle, 
                               Color.White, 0f, Vector2.Zero, scale, 
                               spriteEffects, 0f);
            }
        }

        public virtual Rectangle GetBounds()
        {
            int xOffset = (int)((PROJECTILE_WIDTH - collisionWidth) * scale / 2);
            int yOffset = (int)((PROJECTILE_HEIGHT - collisionHeight) * scale / 2);
            
            return new Rectangle(
                (int)position.X + xOffset, 
                (int)position.Y + yOffset, 
                (int)(collisionWidth * scale), 
                (int)(collisionHeight * scale)
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

        public virtual void OnCollision(ICollidable other, CollisionDirection direction)
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
 
        public virtual void Destroy()
        {
            shouldDestroy = true;
        }
    }
}

