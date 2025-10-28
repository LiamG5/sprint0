using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using sprint0.Classes;
using sprint0.Collisions;
using sprint0.Interfaces;
using sprint0.Sprites.Enemies;
using System;
using static sprint0.Sprites.EnemySpriteFactory;

namespace sprint0.Sprites
{
    public class EnemyKeese : ISprite, IEnemy {

        private Texture2D enemySS;
        private static Rectangle frame1 = new Rectangle(16 * 6, 16 * 17, 16, 16);
        private static Rectangle frame2 = new Rectangle(16 * 7, 16 * 17, 16, 16);
        private EnemyAnimationHelper animation = new EnemyAnimationHelper(frame1, frame2);
<<<<<<< HEAD
        public EnemyKeese (Texture2D sheet)
=======
        private EnemyMovementCycle movement;
        private bool isDead = false;
        private const int ENEMY_WIDTH = 48;  // 16 * 3.0f scale
        private const int ENEMY_HEIGHT = 48;
        public EnemyKeese (Texture2D sheet, Vector2 startPosition)
>>>>>>> 98ffd68f04505072a92d6ad779354f7b6a1172af
        {
            enemySS = sheet;
            movement = new EnemyMovementCycle(startPosition);
        }
        public EnemyKeese(Texture2D sheet) : this(sheet, new Vector2(200, 100))
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
        public void Draw(SpriteBatch spriteBatch, Vector2 position)
        {
<<<<<<< HEAD
            spriteBatch.Draw(enemySS, position, animation.GetFrame(), Color.White, 0f, Vector2.Zero, 3.0f, SpriteEffects.None, 0f);
=======
            if (!isDead)
            {
                spriteBatch.Draw(enemySS, movement.GetPosition(), animation.GetFrame(), Color.White, 0f, Vector2.Zero, 3.0f, SpriteEffects.None, 0f);
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
            return new Rectangle((int)movement.GetPosition().X, (int)movement.GetPosition().Y, ENEMY_WIDTH, ENEMY_HEIGHT);
        }

        public bool IsSolid()
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
                    link.TakeDamage();
                    break;

                case DungeonLongWall wall when wall.IsSolid():
                    movement.ChangeDirection();
                    break;

                case DungeonTallWall wall when wall.IsSolid():
                    movement.ChangeDirection();
                    break;

                case IBlock block when block.IsSolid():
                    movement.ChangeDirection();
                    break;
            }
>>>>>>> 98ffd68f04505072a92d6ad779354f7b6a1172af
        }
    }
}
