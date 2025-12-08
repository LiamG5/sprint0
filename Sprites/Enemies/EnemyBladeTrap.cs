using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using sprint0.Classes;
using sprint0.Collisions;
using sprint0.Interfaces;

namespace sprint0.Sprites
{
    public class EnemyBladeTrap : ISprite, IEnemy
    {
        private Texture2D enemySS;
        private Vector2 position;
        private Vector2 startPosition;
        private int enemyTypeX = 2;
        private int enemyTypeY = 21;
        private Rectangle enemy;
        private const int ENEMY_WIDTH = 48;
        private const int ENEMY_HEIGHT = 48;

        // Detection hitbox dimensions
        private const int DETECTION_LENGTH = 300;
        private const int DETECTION_WIDTH = 48;

        private const int SCREEN_CENTER_X = 384;
        private const int SCREEN_CENTER_Y = 264;

        // Movement
        private Vector2 velocity;
        private const float ATTACK_SPEED = 4f;
        private const float RETURN_SPEED = 1.5f;

        private enum TrapState { Idle, Attacking, Returning }
        private TrapState currentState = TrapState.Idle;
        private Direction attackDirection;

        private enum Direction { None, Up, Down, Left, Right }

        private Link link;

        public EnemyBladeTrap(Texture2D sheet, Vector2 startingPosition, Link link = null)
        {
            enemySS = sheet;
            position = startingPosition;
            startPosition = startingPosition;
            enemy = new Rectangle(16 * enemyTypeX, 16 * enemyTypeY, 16, 16);
            this.link = link;
        }

        public EnemyBladeTrap(Texture2D sheet) : this(sheet, new Vector2(200, 100), null)
        {
        }

        // Detection hitboxes for each direction
        private Rectangle GetUpDetectionZone()
        {
            return new Rectangle(
                (int)position.X,
                (int)position.Y - DETECTION_LENGTH,
                DETECTION_WIDTH,
                DETECTION_LENGTH
            );
        }

        private Rectangle GetDownDetectionZone()
        {
            return new Rectangle(
                (int)position.X,
                (int)position.Y + ENEMY_HEIGHT,
                DETECTION_WIDTH,
                DETECTION_LENGTH
            );
        }

        private Rectangle GetLeftDetectionZone()
        {
            return new Rectangle(
                (int)position.X - DETECTION_LENGTH,
                (int)position.Y,
                DETECTION_LENGTH,
                DETECTION_WIDTH
            );
        }

        private Rectangle GetRightDetectionZone()
        {
            return new Rectangle(
                (int)position.X + ENEMY_WIDTH,
                (int)position.Y,
                DETECTION_LENGTH,
                DETECTION_WIDTH
            );
        }

        public void Update(GameTime gameTime)
        {
            switch (currentState)
            {
                case TrapState.Idle:
                    CheckForLinkDetection();
                    break;

                case TrapState.Attacking:
                    position += velocity;
                    CheckIfReachedCenter();
                    break;

                case TrapState.Returning:
                    MoveTowardStart();
                    break;
            }
        }

        private void CheckForLinkDetection()
        {
            if (link == null) return;

            Rectangle linkBounds = link.GetBounds();

            if (linkBounds.Intersects(GetLeftDetectionZone()))
            {
                StartAttack(Direction.Left);
            }
            else if (linkBounds.Intersects(GetRightDetectionZone()))
            {
                StartAttack(Direction.Right);
            }
            else if (linkBounds.Intersects(GetUpDetectionZone()))
            {
                StartAttack(Direction.Up);
            }
            else if (linkBounds.Intersects(GetDownDetectionZone()))
            {
                StartAttack(Direction.Down);
            }
        }

        private void StartAttack(Direction direction)
        {
            currentState = TrapState.Attacking;
            attackDirection = direction;

            velocity = direction switch
            {
                Direction.Up => new Vector2(0, -ATTACK_SPEED),
                Direction.Down => new Vector2(0, ATTACK_SPEED),
                Direction.Left => new Vector2(-ATTACK_SPEED, 0),
                Direction.Right => new Vector2(ATTACK_SPEED, 0),
                _ => Vector2.Zero
            };
        }

        private void CheckIfReachedCenter()
        {
            bool reachedCenter = attackDirection switch
            {
                Direction.Left => position.X <= SCREEN_CENTER_X,
                Direction.Right => position.X >= SCREEN_CENTER_X,
                Direction.Up => position.Y <= SCREEN_CENTER_Y,
                Direction.Down => position.Y >= SCREEN_CENTER_Y,
                _ => false
            };

            if (reachedCenter)
            {
                StartReturn();
            }
        }

        private void StartReturn()
        {
            currentState = TrapState.Returning;
            velocity = Vector2.Zero;
        }

        private void MoveTowardStart()
        {
            Vector2 toStart = startPosition - position;
            float distance = toStart.Length();

            if (distance < RETURN_SPEED)
            {
                position = startPosition;
                currentState = TrapState.Idle;
                velocity = Vector2.Zero;
            }
            else
            {
                toStart.Normalize();
                position += toStart * RETURN_SPEED;
            }
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 position)
        {
            spriteBatch.Draw(enemySS, this.position, enemy, Color.White, 0f, Vector2.Zero, 3.0f, SpriteEffects.None, 0f);
        }

        public void TakeDamage(int damage)
        {
            // Can't take damage
        }

        public bool IsDead()
        {
            return false; // Can't die
        }

        public Rectangle GetBounds()
        {
            return new Rectangle((int)position.X, (int)position.Y, ENEMY_WIDTH, ENEMY_HEIGHT);
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
                    if (currentState == TrapState.Attacking)
                    {
                        StartReturn();
                    }
                    break;

                case DungeonLongWall wall when wall.BlocksMovement():
                    if (currentState == TrapState.Attacking)
                    {
                        StartReturn();
                    }
                    break;

                case DungeonTallWall wall when wall.BlocksMovement():
                    if (currentState == TrapState.Attacking)
                    {
                        StartReturn();
                    }
                    break;

                case IBlock block when block.BlocksMovement():
                    if (currentState == TrapState.Attacking)
                    {
                        StartReturn();
                    }
                    break;
            }
        }
    }
}
