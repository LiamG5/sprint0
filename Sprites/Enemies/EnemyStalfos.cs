using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using sprint0.Classes;
using sprint0.Collisions;
using sprint0.Commands;
using sprint0.Interfaces;
using sprint0.Sprites.Enemies;
using System;

namespace sprint0.Sprites
{
    public class EnemyStalfos : ISprite, IEnemy
    {
        private Texture2D enemySS;
        private Rectangle frame1 = new Rectangle(16 * 0, 16 * 12, 16, 16);
        private Rectangle frame2 = new Rectangle(16 * 1, 16 * 12, 16, 16);
        private EnemyAnimationHelper animation;
        private EnemyMovementCycle movement;
        private bool isDead = false;
        private const int ENEMY_WIDTH = 48;
        private const int ENEMY_HEIGHT = 48;

        int health = 5;

        private EnemyStateMachine stateMachine;
        
        public EnemyStalfos(Texture2D sheet, Vector2 startPosition, Func<Vector2> targetProvider)
        {
            enemySS = sheet;
            movement = new EnemyMovementCycle(startPosition, targetProvider);
            animation = new EnemyAnimationHelper(frame1, frame2);
            stateMachine = new EnemyStateMachine
            {
                KnockbackDuration = 250f,
                InvulnerabilityDuration = 500f,
                KnockbackSpeed = 5f
            };
        }

        public EnemyStalfos(Texture2D sheet, Vector2 startPosition)
            : this(sheet, startPosition, null)
        {
        }

        public EnemyStalfos(Texture2D sheet)
            : this(sheet, new Vector2(200, 100), null)
        {
        }

        public void Update(GameTime gameTime)
        {
            if (isDead) return;

            float elapsedMs = (float)gameTime.ElapsedGameTime.TotalMilliseconds;

            Vector2 knockbackDelta = stateMachine.UpdateKnockback(elapsedMs);
            
            if (stateMachine.GetCurrentState() == EnemyStateMachine.State.Knockback)
            {
                Vector2 currentPos = movement.GetPosition();
                movement.SetPosition(currentPos + knockbackDelta);
                animation.Update(gameTime);
            }
            else
            {
                movement.Move();
                animation.Update(gameTime);
            }
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 position)
        {
            if (!isDead)
            {
                Color drawColor = stateMachine.GetDrawColor();
                spriteBatch.Draw(enemySS, movement.GetPosition(), animation.GetFrame(), drawColor, 0f, Vector2.Zero, 3.0f, SpriteEffects.None, 0f);
            }
        }

        public void TakeDamage(int damage)
        {
            if (!stateMachine.IsInvulnerable())
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
            if (!isDead)
            {
                stateMachine.StartKnockback(direction);
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
                        if (!isDead) {
                            stateMachine.SetInvulnerableState();
                        }
                    }
                    break;

                case Projectile projectile when !projectile.IsEnemyProjectile && projectile.damage != 0:
                    if (!Classes.Inventory.AreEnemiesFrozen()) {
                        TakeKnockback(direction);
                    } else {
                        if (!isDead) {
                            stateMachine.SetInvulnerableState();
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
            if (stateMachine.GetCurrentState() == EnemyStateMachine.State.Knockback)
            {
                stateMachine.CancelKnockback();
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
