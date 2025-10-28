using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using sprint0.Classes;
using sprint0.Interfaces;
using sprint0.Sprites.Enemies;
using sprint0.Collisions;
using System;

namespace sprint0.Sprites
{
    public class EnemyGel : ISprite, IEnemy
    {
        private Texture2D enemySS;
<<<<<<< HEAD
        private Vector2 position;
        private static Rectangle frame1 = new Rectangle(16 * 4, 16 * 18, 16, 16);
        private static Rectangle frame2 = new Rectangle(16 * 5, 16 * 18, 16, 16);
        private EnemyAnimationHelper animation = new EnemyAnimationHelper(frame1, frame2);
=======
        private static Rectangle frame1 = new Rectangle(16 * 4, 16 * 18, 16, 16);
        private static Rectangle frame2 = new Rectangle(16 * 5, 16 * 18, 16, 16);
        private EnemyAnimationHelper animation = new EnemyAnimationHelper(frame1, frame2);
        private EnemyMovementCycle movement;
>>>>>>> 98ffd68f04505072a92d6ad779354f7b6a1172af
        private bool isDead = false;
        private const int ENEMY_WIDTH = 48;  // 16 * 3.0f scale
        private const int ENEMY_HEIGHT = 48;

        public EnemyGel(Texture2D sheet, Vector2 startPosition)
        { 
            enemySS = sheet;
<<<<<<< HEAD
            position = startPosition;
=======
            movement = new EnemyMovementCycle(startPosition);
>>>>>>> 98ffd68f04505072a92d6ad779354f7b6a1172af
        }

        public EnemyGel(Texture2D sheet) : this(sheet, new Vector2(200, 100))
        {
        }

        public void Update(GameTime gameTime)
        {
<<<<<<< HEAD
=======
            movement.Move();
>>>>>>> 98ffd68f04505072a92d6ad779354f7b6a1172af
            animation.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 drawPosition)
        {
            if (!isDead)
            {
<<<<<<< HEAD
                spriteBatch.Draw(enemySS, drawPosition, animation.GetFrame(), Color.White, 0f, Vector2.Zero, 3.0f, SpriteEffects.None, 0f);
=======
                spriteBatch.Draw(enemySS, movement.GetPosition(), animation.GetFrame(), Color.White, 0f, Vector2.Zero, 3.0f, SpriteEffects.None, 0f);
>>>>>>> 98ffd68f04505072a92d6ad779354f7b6a1172af
            }
        }

        public void TakeDamage()
        {
            isDead = true;
        }

        public bool IsDead()
        {
            return isDead;
        }

        public Rectangle GetBounds()
        {
<<<<<<< HEAD
            return new Rectangle((int)position.X, (int)position.Y, ENEMY_WIDTH, ENEMY_HEIGHT);
=======
            return new Rectangle((int)movement.GetPosition().X, (int)movement.GetPosition().Y, ENEMY_WIDTH, ENEMY_HEIGHT);
>>>>>>> 98ffd68f04505072a92d6ad779354f7b6a1172af
        }

        public bool IsSolid()
        {
            return true;
        }

        public Vector2 GetPosition()
        {
<<<<<<< HEAD
            return position;
=======
            return movement.GetPosition();
>>>>>>> 98ffd68f04505072a92d6ad779354f7b6a1172af
        }

        public void OnCollision(ICollidable other, CollisionDirection direction)
        {
            switch (other)
            {
                case Link link:
                    link.TakeDamage();
                    break;

<<<<<<< HEAD
                case IBlock block when block.IsSolid():
=======
                case DungeonLongWall wall when wall.IsSolid():
                    movement.ChangeDirection();
                    break;

                case DungeonTallWall wall when wall.IsSolid():
                    movement.ChangeDirection();
                    break;

                case IBlock block when block.IsSolid():
                    movement.ChangeDirection();
>>>>>>> 98ffd68f04505072a92d6ad779354f7b6a1172af
                    break;
            }
        }
    }
}
