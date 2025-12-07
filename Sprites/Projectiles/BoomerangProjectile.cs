using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using sprint0.Interfaces;
using sprint0.Collisions;
using sprint0.Classes;

namespace sprint0.Sprites
{
    public class BoomerangProjectile : ISprite, ICollidable
    {
        private Vector2 position;
        private Vector2 velocity;
        private Texture2D texture;
        private Rectangle sourceRectangle;
        private int damage;
        private bool shouldDestroy;
        private const int PROJECTILE_WIDTH = 16;
        private const int PROJECTILE_HEIGHT = 16;
        private float scale = 3.0f;
        
        private Vector2 startPosition;
        private Link link;
        private float maxDistance = 200f;
        private float currentDistance = 0f;
        private bool isReturning = false;
        private float rotation = 0f;
        private float rotationSpeed = 0.3f;

        public BoomerangProjectile(Texture2D texture, Rectangle sourceRect, Vector2 startPosition, 
                                   Vector2 initialVelocity, Link link)
        {
            this.texture = texture;
            this.sourceRectangle = sourceRect;
            this.position = startPosition;
            this.velocity = initialVelocity;
            this.damage = 1;
            this.shouldDestroy = false;
            this.startPosition = startPosition;
            this.link = link;
        }

        public bool ShouldDestroy => shouldDestroy;

        public void Update(GameTime gameTime)
        {
            if (link == null)
            {
                shouldDestroy = true;
                return;
            }

            Vector2 linkPosition = link.GetPosition();
            Vector2 toLink = linkPosition - position;
            float distanceToLink = toLink.Length();

            if (!isReturning)
            {
                currentDistance = Vector2.Distance(position, startPosition);
                
                if (currentDistance >= maxDistance)
                {
                    isReturning = true;
                }
            }
            else
            {
                if (distanceToLink < 20f)
                {
                    shouldDestroy = true;
                    return;
                }
                
                Vector2 returnDirection = Vector2.Normalize(toLink);
                velocity = returnDirection * 6f;
            }

            position += velocity;
            rotation += rotationSpeed;
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 drawPosition)
        {
            if (!shouldDestroy && texture != null && sourceRectangle.Width > 0 && sourceRectangle.Height > 0)
            {
                Vector2 origin = new Vector2(sourceRectangle.Width / 2, sourceRectangle.Height / 2);
                spriteBatch.Draw(texture, drawPosition + origin, sourceRectangle, 
                               Color.White, rotation, origin, scale, 
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
                case Link link when !isReturning:
                    break;

                case Link link when isReturning:
                    shouldDestroy = true;
                    break;

                case IEnemy enemy:
                    sprint0.Sounds.SoundStorage.LOZ_Enemy_Hit.Play();
                    enemy.TakeDamage();
                    if (!isReturning)
                    {
                        isReturning = true;
                    }
                    break;

                case IBlock block when block.BlocksProjectiles():
                    if (!isReturning)
                    {
                        isReturning = true;
                    }
                    break;

                case ICollidable wall when wall.BlocksProjectiles():
                    if (!isReturning)
                    {
                        isReturning = true;
                    }
                    break;
            }
        }

        public void Destroy()
        {
            shouldDestroy = true;
        }
    }
}

