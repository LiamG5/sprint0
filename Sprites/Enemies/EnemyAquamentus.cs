using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using sprint0.Classes;
using sprint0.Collisions;
using sprint0.Interfaces;
using sprint0.Loaders;
using sprint0.Sprites.Dungeon;
using sprint0.Sprites.Enemies;
using sprint0.Sprites.Projectiles;
using System;

namespace sprint0.Sprites
{
    public class EnemyAquamentus : ISprite, IEnemy
    {
        private Texture2D bossSpriteSheet;
        private Rectangle frame1 = new Rectangle(16 * 0, 16 * 0, 24, 32);
        private Rectangle frame2 = new Rectangle(16 * 0, 16 * 2, 24, 32);
        private EnemyAnimationHelper animation;
        private Vector2 position;
        private Vector2 velocity;
        private bool isDead = false;
        private int health = 10;
        private int invulnerabilityTimer = 0;
        private const int invulnerabilityDuration = 30;
        
        private EnemyStateMachine stateMachine;
        private const int BOSS_WIDTH = 72;
        private const int BOSS_HEIGHT = 96;
        private int attackTimer = 0;
        private const int attackInterval = 120;
        private int magicAttackTimer = 0;
        private const int magicAttackInterval = 240; // Magic attack every 4 seconds
        private int moveTimer = 0;
        private const int moveInterval = 60; // Change direction every 1 second
        private int moveDirection = 0; // 0=left, 1=right, 2=up, 3=down
        private const float moveSpeed = 2.5f; // Much faster - increased from 2.0f
        private int moveDuration = 0;
        private const int maxMoveDuration = 999999; // Effectively infinite - boss always moves
        private int wallCollisionCount = 0; // Track wall hits
        private Vector2 lastPosition; // Track if boss is stuck
        private int stuckTimer = 0; // Count frames boss hasn't moved
        private const int stuckThreshold = 15; // Reduced for faster response
        private bool isChasing = false; // Whether boss is chasing player
        private Func<Vector2> playerPositionProvider; // Get player position
        private Random random = new Random();
        private DungeonLoader dungeonLoader;

        public EnemyAquamentus(Texture2D bossSpriteSheet, Vector2 startPosition, DungeonLoader dungeonLoader = null, Func<Vector2> playerPositionProvider = null)
        {
            this.bossSpriteSheet = bossSpriteSheet;
            this.position = startPosition;
            this.lastPosition = startPosition;
            this.dungeonLoader = dungeonLoader;
            this.playerPositionProvider = playerPositionProvider;
            animation = new EnemyAnimationHelper(frame1, frame2);
            stateMachine = new EnemyStateMachine
            {
                KnockbackDuration = 200f,
                InvulnerabilityDuration = 500f,  // Will use invulnerabilityTimer instead
                KnockbackSpeed = 2f
            };
            
            // Start moving immediately in a random direction
            moveDuration = maxMoveDuration;
            ChooseNewDirection();
        }

        public void Update(GameTime gameTime)
        {
            animation.Update(gameTime);
            
            float elapsedMs = (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            
            Vector2 knockbackDelta = stateMachine.UpdateKnockback(elapsedMs);
            
            if (stateMachine.GetCurrentState() == EnemyStateMachine.State.Knockback)
            {
                position += knockbackDelta;
                if (invulnerabilityTimer > 0)
                {
                    invulnerabilityTimer--;
                }
            }
            else if (stateMachine.GetCurrentState() == EnemyStateMachine.State.Invulnerable)
            {
                if (invulnerabilityTimer > 0)
                {
                    invulnerabilityTimer--;
                }
                else
                {
                    stateMachine.Reset();
                }
            }
            else
            {
                if (invulnerabilityTimer > 0)
                {
                    invulnerabilityTimer--;
                }
            }
            
            // Only do normal movement if not in knockback
            if (stateMachine.GetCurrentState() != EnemyStateMachine.State.Knockback)
            {
                // Check if boss is stuck (hasn't moved)
                if (Vector2.Distance(position, lastPosition) < 0.5f)
                {
                    stuckTimer++;
                }
                else
                {
                    stuckTimer = 0;
                    wallCollisionCount = 0; // Reset collision count if moving
                }
                lastPosition = position;
                
                // If stuck or hit walls too many times, change direction immediately
                if (stuckTimer >= stuckThreshold || wallCollisionCount >= 2)
                {
                    stuckTimer = 0;
                    wallCollisionCount = 0;
                    ChooseNewDirection();
                }
                
                // Handle movement - boss is always moving
                moveTimer++;
                
                // Always moving - just update position
                position += velocity;
                moveDuration--;
                
                // Change direction when timer expires or duration ends
                if (moveTimer >= moveInterval || moveDuration <= 0)
                {
                    // Time to change direction
                    moveTimer = 0;
                    moveDuration = maxMoveDuration;
                    
                    // 40% chance to chase player, 60% chance for random movement
                    if (playerPositionProvider != null && random.Next(100) < 40)
                    {
                        isChasing = true;
                        ChasePlayer();
                    }
                    else
                    {
                        isChasing = false;
                        ChooseNewDirection();
                    }
                }
            }
            
            // Attack timer
            attackTimer++;
            if (attackTimer >= attackInterval)
            {
                attackTimer = 0;
                ShootFireballs();
            }
            
            // Magic attack timer
            magicAttackTimer++;
            if (magicAttackTimer >= magicAttackInterval)
            {
                magicAttackTimer = 0;
                ShootMagicAttack();
            }
        }

        private void ChooseNewDirection()
        {
            moveDirection = random.Next(4);
            
            switch (moveDirection)
            {
                case 0: // Left
                    velocity = new Vector2(-moveSpeed, 0f);
                    break;
                case 1: // Right
                    velocity = new Vector2(moveSpeed, 0f);
                    break;
                case 2: // Up
                    velocity = new Vector2(0f, -moveSpeed);
                    break;
                case 3: // Down
                    velocity = new Vector2(0f, moveSpeed);
                    break;
            }
        }

        private void ChasePlayer()
        {
            if (playerPositionProvider == null) return;
            
            Vector2 playerPos = playerPositionProvider();
            Vector2 toPlayer = playerPos - position;
            
            // Move in dominant direction (no diagonals)
            if (Math.Abs(toPlayer.X) > Math.Abs(toPlayer.Y))
            {
                // Move horizontally
                velocity = new Vector2(Math.Sign(toPlayer.X) * moveSpeed, 0f);
                moveDirection = toPlayer.X > 0 ? 1 : 0; // Right or Left
            }
            else
            {
                // Move vertically
                velocity = new Vector2(0f, Math.Sign(toPlayer.Y) * moveSpeed);
                moveDirection = toPlayer.Y > 0 ? 3 : 2; // Down or Up
            }
        }

        private void ShootFireballs()
        {
            if (dungeonLoader == null) return;

            Vector2 fireballStartPos = new Vector2(position.X - 30, position.Y + 40);
            
            Vector2 velocity1 = new Vector2(-2f, -1f);
            Vector2 velocity2 = new Vector2(-2f, 0f);
            Vector2 velocity3 = new Vector2(-2f, 1f);

            Texture2D enemySpriteSheet = Texture2DStorage.GetEnemiesSpriteSheet();
            if (enemySpriteSheet != null)
            {
                Rectangle fireballRect = new Rectangle(16 * 5, 16 * 0, 16, 16);
                
                Projectile fireball1 = new Projectile(enemySpriteSheet, fireballRect, fireballStartPos, velocity1, 1, true);
                Projectile fireball2 = new Projectile(enemySpriteSheet, fireballRect, fireballStartPos, velocity2, 1, true);
                Projectile fireball3 = new Projectile(enemySpriteSheet, fireballRect, fireballStartPos, velocity3, 1, true);
                
                dungeonLoader.AddProjectile(fireball1);
                dungeonLoader.AddProjectile(fireball2);
                dungeonLoader.AddProjectile(fireball3);
            }
        }

        private void ShootMagicAttack()
        {
            if (dungeonLoader == null) return;

            // Center position of the boss for the magic attack
            Vector2 magicStartPos = new Vector2(position.X + BOSS_WIDTH / 2, position.Y + BOSS_HEIGHT / 2);
            
            // Shoot magic projectiles in 16 directions for denser pattern
            float magicSpeed = 3.0f; // Faster than fireballs
            Vector2[] directions = new Vector2[]
            {
                // Cardinal directions
                new Vector2(0, -1),           // Up
                new Vector2(1, 0),            // Right
                new Vector2(0, 1),            // Down
                new Vector2(-1, 0),           // Left
                // Main diagonals
                new Vector2(0.707f, -0.707f), // Up-Right
                new Vector2(0.707f, 0.707f),  // Down-Right
                new Vector2(-0.707f, 0.707f), // Down-Left
                new Vector2(-0.707f, -0.707f),// Up-Left
                // Additional angles for denser coverage
                new Vector2(0.383f, -0.924f), // Up-Right (30°)
                new Vector2(0.924f, -0.383f), // Right-Up (60°)
                new Vector2(0.924f, 0.383f),  // Right-Down (120°)
                new Vector2(0.383f, 0.924f),  // Down-Right (150°)
                new Vector2(-0.383f, 0.924f), // Down-Left (210°)
                new Vector2(-0.924f, 0.383f), // Left-Down (240°)
                new Vector2(-0.924f, -0.383f),// Left-Up (300°)
                new Vector2(-0.383f, -0.924f) // Up-Left (330°)
            };

            Texture2D enemySpriteSheet = Texture2DStorage.GetEnemiesSpriteSheet();
            if (enemySpriteSheet != null)
            {
                // Use the blue spinning magic effect - larger size for visibility
                Rectangle magicRect = new Rectangle(16 * 6, 16 * 8, 32, 32);
                
                foreach (Vector2 direction in directions)
                {
                    Vector2 velocity = direction * magicSpeed;
                    Projectile magic = new Projectile(enemySpriteSheet, magicRect, magicStartPos, velocity, 2, true);
                    dungeonLoader.AddProjectile(magic);
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 drawPosition)
        {
            if (!isDead && bossSpriteSheet != null)
            {
                Color drawColor = Color.White;
                if (stateMachine.IsInvulnerable())
                {
                    drawColor = (invulnerabilityTimer % 6 < 3) ? Color.Red : Color.White;
                }
                spriteBatch.Draw(bossSpriteSheet, position, animation.GetFrame(), drawColor, 0f, Vector2.Zero, 3.0f, SpriteEffects.None, 0f);
            }
        }

        public void TakeDamage(int damage)
        {
            if (!stateMachine.IsInvulnerable())
            {
                health -= damage;
                sprint0.Sounds.SoundStorage.LOZ_Enemy_Hit.Play();
                
                System.Diagnostics.Debug.WriteLine($"Boss took damage! Health remaining: {health}");
                
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
                invulnerabilityTimer = invulnerabilityDuration;
            }
        }

        public bool IsDead()
        {
            return isDead;
        }

        public Rectangle GetBounds()
        {
            return new Rectangle((int)position.X, (int)position.Y, BOSS_WIDTH, BOSS_HEIGHT);
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
            return position;
        }

        public void OnCollision(ICollidable other, CollisionDirection direction)
        {
            switch (other)
            {
                case Link link:
                    break;

                case LinkAttackHitbox hitbox when hitbox.BlocksMovement():
                    TakeKnockback(hitbox.GetKnockbackDirection());
                    break;

                case Projectile projectile when !projectile.IsEnemyProjectile && projectile.damage != 0:
                    TakeKnockback(direction);
                    break;
                    
                case DungeonLongWall wall when wall.BlocksMovement():
                    HandleBlockCollision(wall, direction);
                    break;

                case DungeonTallWall wall when wall.BlocksMovement():
                    HandleBlockCollision(wall, direction);
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
                position = resolvedPosition;
            }
            else
            {
                wallCollisionCount++;
                ChooseNewDirection();
                moveDuration = maxMoveDuration;
                moveTimer = 0;
            }
        }
    }
}