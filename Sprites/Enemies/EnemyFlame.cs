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
    public class EnemyFlame : ISprite, IEnemy
    {
        private Texture2D enemySS;
        public readonly Rectangle frame1 = new Rectangle(16 * 6, 16 * 11, 16, 16);
        public readonly Rectangle frame2 = new Rectangle(16 * 7, 16 * 11, 16, 16);
        private EnemyAnimationHelper animation;
        private Vector2 position;

        private bool isDead = false;
        private const int ENEMY_WIDTH = 48;  // 16 * 3.0f scale
        private const int ENEMY_HEIGHT = 48;

        // NEW: constructor with target provider (for chasing player)
        public EnemyFlame(Texture2D sheet, Vector2 startPosition, Func<Vector2> targetProvider)
        {
            enemySS = sheet;
            animation = new EnemyAnimationHelper(frame1, frame2);
            position = startPosition;
        }

        // OLD constructors now call the new one with null (random movement)
        public EnemyFlame(Texture2D sheet, Vector2 startPosition)
            : this(sheet, startPosition, null)
        {
        }

        public EnemyFlame(Texture2D sheet)
            : this(sheet, new Vector2(200, 100), null)
        {
        }

        public void Update(GameTime gameTime)
        {
            
            animation.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 drawPosition)
        {
            if (!isDead)
            {
                spriteBatch.Draw(enemySS, position, animation.GetFrame(), Color.White, 0f, Vector2.Zero, 3.0f, SpriteEffects.None, 0f);
            }
        }

        public void TakeDamage()
        {
            
        }

        public bool IsDead()
        {
            return isDead;
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
            
        }
    }
}
