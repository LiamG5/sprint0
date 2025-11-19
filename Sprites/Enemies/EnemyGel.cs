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
        public readonly Rectangle frame1 = new Rectangle(16 * 4, 16 * 18, 16, 16);
        public readonly Rectangle frame2 = new Rectangle(16 * 5, 16 * 18, 16, 16);
        private EnemyAnimationHelper animation;
        private EnemyMovementCycle movement;
        private bool isDead = false;
        private const int ENEMY_WIDTH = 48;  // 16 * 3.0f scale
        private const int ENEMY_HEIGHT = 48;

        // NEW: constructor with target provider (for chasing player)
        public EnemyGel(Texture2D sheet, Vector2 startPosition, Func<Vector2> targetProvider)
        {
            enemySS = sheet;
            movement = new EnemyMovementCycle(startPosition, targetProvider);
            animation = new EnemyAnimationHelper(frame1, frame2);
        }

        // OLD constructors now call the new one with null (random movement)
        public EnemyGel(Texture2D sheet, Vector2 startPosition)
            : this(sheet, startPosition, null)
        {
        }

        public EnemyGel(Texture2D sheet)
            : this(sheet, new Vector2(200, 100), null)
        {
        }

        public void Update(GameTime gameTime)
        {
            movement.Move();
            animation.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 drawPosition)
        {
            if (!isDead)
            {
                spriteBatch.Draw(enemySS, movement.GetPosition(), animation.GetFrame(), Color.White, 0f, Vector2.Zero, 3.0f, SpriteEffects.None, 0f);
            }
        }

        public void TakeDamage()
        {
            isDead = true;
            sprint0.Sounds.SoundStorage.LOZ_Enemy_Die.Play();
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

                case DungeonLongWall wall when wall.BlocksMovement():
                    movement.ChangeDirection();
                    break;

                case DungeonTallWall wall when wall.BlocksMovement():
                    movement.ChangeDirection();
                    break;

                case IBlock block when block.BlocksMovement():
                    movement.ChangeDirection();
                    break;
                case IAttack attack when attack.BlocksMovement():
                    TakeDamage();
                    break;
            }
        }
    }
}
