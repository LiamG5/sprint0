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
    public class EnemyKeese : ISprite, IEnemy
    {
        private Texture2D enemySS;
        private Rectangle frame1 = new Rectangle(16 * 6, 16 * 17, 16, 16);
        private Rectangle frame2 = new Rectangle(16 * 7, 16 * 17, 16, 16);
        private EnemyAnimationHelper animation;
        private EnemyMovementCycle movement;
        private bool isDead = false;
        private const int ENEMY_WIDTH = 48;  // 16 * 3.0f scale
        private const int ENEMY_HEIGHT = 48;

        // NEW: with target provider
        public EnemyKeese(Texture2D sheet, Vector2 startPosition, Func<Vector2> targetProvider)
        {
            enemySS = sheet;
            movement = new EnemyMovementCycle(startPosition, targetProvider);
            animation = new EnemyAnimationHelper(frame1, frame2);
        }

        public EnemyKeese(Texture2D sheet, Vector2 startPosition)
            : this(sheet, startPosition, null)
        {
        }

        public EnemyKeese(Texture2D sheet)
            : this(sheet, new Vector2(200, 100), null)
        {
        }

        public void Update(GameTime gameTime)
        {
            movement.Move();
            animation.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 position)
        {
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

                case DungeonLongWall:
                    movement.ChangeDirectionCol();
                    break;

                case DungeonTallWall:
                    movement.ChangeDirectionCol();
                    break;
                case TransitionZone:
                    movement.ChangeDirectionCol();
                    break;

                case IBlock block when block.BlocksMovement():
                    movement.ChangeDirectionCol();
                    break;
                case IAttack attack when attack.BlocksMovement():
                    TakeDamage();
                    System.Diagnostics.Debug.WriteLine("Keese took damage");
                    break;
            }
        }
    }
}



