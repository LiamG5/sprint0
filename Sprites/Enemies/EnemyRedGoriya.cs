using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using sprint0.Classes;
using sprint0.Interfaces;
using sprint0.Collisions;
using sprint0.Sprites.Enemies;
using System;

namespace sprint0.Sprites
{
    public class EnemyRedGoriya : ISprite, IEnemy
    {
        private Texture2D enemySS;
        private Rectangle frame1 = new Rectangle(16 * 0, 16 * 2, 16, 16);
        private Rectangle frame2 = new Rectangle(16 * 1, 16 * 2, 16, 16);
        private EnemyAnimationHelper animation;
        private EnemyMovementCycle movement;
        private bool isDead = false;
        private const int ENEMY_WIDTH = 48;  // 16 * 3.0f scale
        private const int ENEMY_HEIGHT = 48;

        int health = 5;

        private enum EnemyState { Normal, Knockback, Invulnerable }
        private EnemyState currentState = EnemyState.Normal;
        private float knockbackTimer = 0f;
        private float invulnerabilityTimer = 0f;
        private const float KNOCKBACK_DURATION = 250f;
        private const float INVULNERABILITY_DURATION = 500f;
        private Vector2 knockbackVelocity = Vector2.Zero;
        private const float KNOCKBACK_SPEED = 5f;

        // NEW: with target provider
        public EnemyRedGoriya(Texture2D sheet, Vector2 startPosition, Func<Vector2> targetProvider)
        {
            enemySS = sheet;
            movement = new EnemyMovementCycle(startPosition, targetProvider);
            animation = new EnemyAnimationHelper(frame1, frame2);
        }

        public EnemyRedGoriya(Texture2D sheet, Vector2 startPosition)
            : this(sheet, startPosition, null)
        {
        }

        public EnemyRedGoriya(Texture2D sheet)
            : this(sheet, new Vector2(200, 100), null)
        {
        }

        public void Update(GameTime gameTime)
        {
            if (isDead) return;

            float elapsedMs = (float)gameTime.ElapsedGameTime.TotalMilliseconds;

            switch (currentState)
            {
                case EnemyState.Knockback:
                    Vector2 currentPos = movement.GetPosition();
                    float knockbackDistance = KNOCKBACK_SPEED * (elapsedMs / 16.67f);
                    Vector2 knockbackDirection = knockbackVelocity;
                    if (knockbackDirection.LengthSquared() > 0)
                    {
                        knockbackDirection.Normalize();
                        currentPos += knockbackDirection * knockbackDistance;
                    }
                    movement.SetPosition(currentPos);
                    knockbackTimer += elapsedMs;
                    if (knockbackTimer >= KNOCKBACK_DURATION)
                    {
                        knockbackTimer = 0f;
                        knockbackVelocity = Vector2.Zero;
                        currentState = EnemyState.Invulnerable;
                        invulnerabilityTimer = 0f;
                    }
                    animation.Update(gameTime);
                    break;

                case EnemyState.Invulnerable:
                    movement.Move();
                    animation.Update(gameTime);
                    invulnerabilityTimer += elapsedMs;
                    if (invulnerabilityTimer >= INVULNERABILITY_DURATION)
                    {
                        invulnerabilityTimer = 0f;
                        currentState = EnemyState.Normal;
                    }
                    break;

                case EnemyState.Normal:
                default:
                    movement.Move();
                    animation.Update(gameTime);
                    break;
            }
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 position)
        {
            if (!isDead)
            {
                Color drawColor = Color.White;
                if (currentState == EnemyState.Invulnerable || currentState == EnemyState.Knockback)
                {
                    int flashFrame = (int)(invulnerabilityTimer / 50f) % 2;
                    drawColor = flashFrame == 0 ? Color.Red : Color.White;
                }
                spriteBatch.Draw(enemySS, movement.GetPosition(), animation.GetFrame(), drawColor, 0f, Vector2.Zero, 3.0f, SpriteEffects.None, 0f);
            }
        }

        public void TakeDamage(int damage)
        {
            if (!(currentState == EnemyState.Invulnerable || currentState == EnemyState.Knockback))
            {
                sprint0.Sounds.SoundStorage.LOZ_Enemy_Hit.Play();
                health -= damage;

                if (health <= 0)
                {
                    isDead = true;
                    sprint0.Sounds.SoundStorage.LOZ_Enemy_Die.Play();
                }
            }
        }

        public void TakeKnockback(CollisionDirection direction)
        {
            if (currentState == EnemyState.Normal && !isDead)
            {
                switch (direction)
                {
                    case CollisionDirection.Left:
                        knockbackVelocity = new Vector2(KNOCKBACK_SPEED, 0f);
                        break;
                    case CollisionDirection.Right:
                        knockbackVelocity = new Vector2(-KNOCKBACK_SPEED, 0f);
                        break;
                    case CollisionDirection.Up:
                        knockbackVelocity = new Vector2(0f, KNOCKBACK_SPEED);
                        break;
                    case CollisionDirection.Down:
                        knockbackVelocity = new Vector2(0f, -KNOCKBACK_SPEED);
                        break;
                    default:
                        knockbackVelocity = new Vector2(KNOCKBACK_SPEED, 0f);
                        break;
                }

                currentState = EnemyState.Knockback;
                knockbackTimer = 0f;
            }
        }

        public bool IsDead()
        {
            return isDead;
        }

        public Rectangle GetBounds()
        {
            return new Rectangle((int)movement.GetPosition().X, (int)movement.GetPosition().Y, ENEMY_WIDTH, ENEMY_HEIGHT);
        }

        public bool BlocksMovement()
        {
            return true;
        }

        public bool BlocksProjectiles()
        {
            return true;
        }

        public Vector2 GetPosition()
        {
            return movement.GetPosition();
        }

        public void OnCollision(ICollidable other, CollisionDirection direction)
        {
            switch (other)
            {
                case Link link:
                    break;

                case LinkAttackHitbox hitbox when hitbox.BlocksMovement():
                    if (!Classes.Inventory.AreEnemiesFrozen()) {
                        TakeKnockback(hitbox.GetKnockbackDirection());
                    } else {
                        if (currentState == EnemyState.Normal && !isDead) {
                            currentState = EnemyState.Invulnerable;
                            invulnerabilityTimer = 0f;
                        }
                    }
                    break;

                case Projectile projectile when !projectile.IsEnemyProjectile && projectile.damage != 0:
                    if (!Classes.Inventory.AreEnemiesFrozen()) {
                        TakeKnockback(direction);
                    } else {
                        if (currentState == EnemyState.Normal && !isDead) {
                            currentState = EnemyState.Invulnerable;
                            invulnerabilityTimer = 0f;
                        }
                    }
                    break;

                case DungeonLongWall wall when wall.BlocksMovement():
                    HandleBlockCollision(wall, direction);
                    break;

                case DungeonTallWall wall when wall.BlocksMovement():
                    HandleBlockCollision(wall, direction);
                    break;
                case TransitionZone zone:
                    HandleBlockCollision(zone, direction);
                    break;

                case IBlock block when block.BlocksMovement():
                    HandleBlockCollision(block, direction);
                    break;
            }
        }

        private void HandleBlockCollision(ICollidable block, CollisionDirection direction)
        {
            if (currentState == EnemyState.Knockback)
            {
                knockbackVelocity = Vector2.Zero;
                var collisionResponse = new CollisionResponse();
                Vector2 resolvedPosition = collisionResponse.ResolveCollisionDirection(
                    this.GetBounds(), block.GetBounds(), direction);
                movement.SetPosition(resolvedPosition);
            }
            else
            {
                movement.ChangeDirectionCol();
            }
        }
    }
}

