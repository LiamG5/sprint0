using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using sprint0.Interfaces;
using sprint0.Collisions;
using sprint0.Classes;

namespace sprint0.Sprites
{
    public class BombProjectile : ISprite, ICollidable
    {
        private Vector2 position;
        private Texture2D texture;
        private Texture2D explosionTexture;
        private Rectangle sourceRectangle;
        private Rectangle explosionRectangle;
        private bool shouldDestroy;
        private const int BOMB_WIDTH = 16;
        private const int BOMB_HEIGHT = 16;
        private float scale = 3.0f;
        
        private const int TILE_SIZE = 16;
        private const int EXPLOSION_ROW = 23;
        private const int EXPLOSION_COL = 0;
        
        private float timer = 0f;
        private const float EXPLODE_TIME = 1.0f;
        private const float EXPLOSION_DURATION = 0.3f;
        private bool hasExploded = false;

        public BombProjectile(Texture2D texture, Rectangle sourceRect, Vector2 startPosition)
        {
            this.texture = texture;
            this.sourceRectangle = sourceRect;
            this.position = startPosition;
            this.shouldDestroy = false;
            this.explosionTexture = Texture2DStorage.GetEnemiesSpriteSheet();
            this.explosionRectangle = new Rectangle(
                EXPLOSION_COL * TILE_SIZE,
                EXPLOSION_ROW * TILE_SIZE,
                TILE_SIZE,
                TILE_SIZE
            );
        }

        public bool ShouldDestroy => shouldDestroy;

        public void Update(GameTime gameTime)
        {
            float elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;
            timer += elapsed;

            if (timer >= EXPLODE_TIME && !hasExploded)
            {
                hasExploded = true;
                sprint0.Sounds.SoundStorage.LOZ_Bomb_Blow.Play();
            }
            
            if (hasExploded && timer >= EXPLODE_TIME + EXPLOSION_DURATION)
            {
                shouldDestroy = true;
            }
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 drawPosition)
        {
            if (shouldDestroy) return;
            
            if (hasExploded)
            {
                if (explosionTexture != null && explosionRectangle.Width > 0 && explosionRectangle.Height > 0)
                {
                    spriteBatch.Draw(explosionTexture, drawPosition, explosionRectangle, 
                                   Color.White, 0f, Vector2.Zero, scale, 
                                   SpriteEffects.None, 0f);
                }
            }
            else
            {
                if (texture != null && sourceRectangle.Width > 0 && sourceRectangle.Height > 0)
                {
                    spriteBatch.Draw(texture, drawPosition, sourceRectangle, 
                                   Color.White, 0f, Vector2.Zero, scale, 
                                   SpriteEffects.None, 0f);
                }
            }
        }

        public Rectangle GetBounds()
        {
            return new Rectangle(
                (int)position.X, 
                (int)position.Y, 
                (int)(BOMB_WIDTH * scale), 
                (int)(BOMB_HEIGHT * scale)
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
                case Link link:
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

        public void Destroy()
        {
            shouldDestroy = true;
            hasExploded = true;
        }
    }
}

