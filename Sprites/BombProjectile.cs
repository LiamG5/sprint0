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
        private Rectangle sourceRectangle;
        private bool shouldDestroy;
        private const int BOMB_WIDTH = 16;
        private const int BOMB_HEIGHT = 16;
        private float scale = 3.0f;
        
        private float timer = 0f;
        private const float EXPLODE_TIME = 2.0f;
        private bool hasExploded = false;

        public BombProjectile(Texture2D texture, Rectangle sourceRect, Vector2 startPosition)
        {
            this.texture = texture;
            this.sourceRectangle = sourceRect;
            this.position = startPosition;
            this.shouldDestroy = false;
        }

        public bool ShouldDestroy => shouldDestroy;

        public void Update(GameTime gameTime)
        {
            float elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;
            timer += elapsed;

            if (timer >= EXPLODE_TIME && !hasExploded)
            {
                hasExploded = true;
                shouldDestroy = true;
            }
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 drawPosition)
        {
            if (!shouldDestroy && texture != null && sourceRectangle.Width > 0 && sourceRectangle.Height > 0)
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
                        enemy.TakeDamage();
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

