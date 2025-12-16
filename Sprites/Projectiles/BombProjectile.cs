using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using sprint0.Interfaces;
using sprint0.Collisions;
using sprint0.Classes;

namespace sprint0.Sprites
{
    public class BombProjectile : Projectile
    {
        private Texture2D explosionTexture;
        private Rectangle explosionRectangle;
        
        private const int TILE_SIZE = 16;
        private const int EXPLOSION_ROW = 23;
        private const int EXPLOSION_COL = 0;
        
        private float timer = 0f;
        private const float EXPLODE_TIME = 1.0f;
        private const float EXPLOSION_DURATION = 0.3f;
        private bool hasExploded = false;
        private bool hasDamagedLink = false;

        public BombProjectile(Texture2D texture, Rectangle sourceRect, Vector2 startPosition)
            : base(texture, sourceRect, startPosition, Vector2.Zero, 0, false, SpriteEffects.None, PROJECTILE_WIDTH, PROJECTILE_HEIGHT)
        {
            explosionTexture = Texture2DStorage.GetEnemiesSpriteSheet();
            explosionRectangle = new Rectangle(
                EXPLOSION_COL * TILE_SIZE,
                EXPLOSION_ROW * TILE_SIZE,
                TILE_SIZE,
                TILE_SIZE
            );
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            
            float elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;
            timer += elapsed;

            if (timer >= EXPLODE_TIME && !hasExploded)
            {
                hasExploded = true;
                hasDamagedLink = false;
                sprint0.Sounds.SoundStorage.LOZ_Bomb_Blow.Play();
            }
            
            if (hasExploded && timer >= EXPLODE_TIME + EXPLOSION_DURATION)
            {
                shouldDestroy = true;
            }
        }

        public override void Draw(SpriteBatch spriteBatch, Vector2 drawPosition)
        {
            if (shouldDestroy) return;
            
            if (hasExploded)
            {
                if (explosionTexture != null && explosionRectangle.Width > 0 && explosionRectangle.Height > 0)
                {
                    spriteBatch.Draw(explosionTexture, drawPosition, explosionRectangle, 
                                   Color.White, 0f, Vector2.Zero, scale * 2, 
                                   SpriteEffects.None, 0f);
                }
            }
            else
            {
                if (texture != null && sourceRectangle.Width > 0 && sourceRectangle.Height > 0)
                {
                    spriteBatch.Draw(texture, drawPosition, sourceRectangle, 
                                   Color.White, 0f, Vector2.Zero, scale, 
                                   spriteEffects, 0f);
                }
            }
        }

        public override Rectangle GetBounds()
        {
            if (hasExploded)
            {
                int explosionSize = (int)(PROJECTILE_WIDTH * scale * 2);
                int offset = (int)(PROJECTILE_WIDTH * scale / 2);
                return new Rectangle(
                    (int)position.X - offset, 
                    (int)position.Y - offset, 
                    explosionSize, 
                    explosionSize
                );
            }
            
            return new Rectangle(
                (int)position.X, 
                (int)position.Y, 
                (int)(PROJECTILE_WIDTH * scale), 
                (int)(PROJECTILE_HEIGHT * scale)
            );
        }

        public override void OnCollision(ICollidable other, CollisionDirection direction)
        {
            switch (other)
            {
                case Link link:
                    if (hasExploded && !hasDamagedLink && !Inventory.GetSuperLink())
                    {
                        link.TakeDamage(2);
                        hasDamagedLink = true;
                    }
                    break;

                case IEnemy enemy:
                    if (hasExploded)
                    {
                        sprint0.Sounds.SoundStorage.LOZ_Enemy_Hit.Play();
                        enemy.TakeDamage(1);
                    }
                    break;

                case IBlock block when block.BlocksProjectiles():
                    if (hasExploded)
                    {
                        shouldDestroy = true;
                    }
                    break;

                case ICollidable wall when wall.BlocksProjectiles():
                    if (hasExploded)
                    {
                        shouldDestroy = true;
                    }
                    break;
            }
        }

        public override void Destroy()
        {
            shouldDestroy = true;
            hasExploded = true;
        }
    }
}

