using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using sprint0.Classes;
using sprint0.Collisions;
using sprint0.Interfaces;
using sprint0.Sprites.Enemies;
using System;

namespace sprint0.Sprites
{
    public class EnemyWallmaster : ISprite, IEnemy
    {
        private Texture2D enemySS;
        private Rectangle frame1 = new Rectangle(16 * 0, 16 * 14, 16, 16);
        private Rectangle frame2 = new Rectangle(16 * 1, 16 * 14, 16, 16);
        private EnemyAnimationHelper animation;
        private EnemyMovementCycle movement;
        private bool isDead = false;
        private float currentTime = 0;
        private float duration = 800; // ms
        private static List<EnemyWallmaster> allWallmasters = new List<EnemyWallmaster>();
        private const int ENEMY_WIDTH = 48;  // 16 * 3.0f scale
        private const int ENEMY_HEIGHT = 48;
        private bool isSpawning = true;

        private bool hidden = true;

        // NEW: with target provider
        public EnemyWallmaster(Texture2D sheet, Vector2 startPosition, Func<Vector2> targetProvider)
        {
            allWallmasters.Add(this);
            enemySS = sheet;
            movement = new EnemyMovementCycle(startPosition, targetProvider);
            animation = new EnemyAnimationHelper(frame1, frame2);
        }

        public EnemyWallmaster(Texture2D sheet, Vector2 startPosition)
            : this(sheet, startPosition, null)
        {
        }

        public EnemyWallmaster(Texture2D sheet)
            : this(sheet, new Vector2(200, 100), null)
        {
        }

        public void Update(GameTime gameTime)
        {
            if (isDead) return;
            
            currentTime += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            if (hidden)
            {
                if(currentTime >= duration* allWallmasters.IndexOf(this))
                {
                    movement.wallMasterSpawn();
                    hidden = false;
                    currentTime = 0;
                }
            }else if( isSpawning)
            {
                if (currentTime >= duration)
                {
                    isSpawning = false;
                    currentTime = 0;
                }
                movement.wallMasterSpawn();
                animation.Update(gameTime);
            }
            else
            {
            {
                movement.Move();
                animation.Update(gameTime);
            }
            
        }
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 position)
        {
            if (!isDead && !hidden)
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
        public static void ResetSpawning()
        {
            allWallmasters.Clear();
        }

        public void OnCollision(ICollidable other, CollisionDirection direction)
        {

            switch (other)
            {
                case Link link:
                    
                    break;

                case DungeonLongWall wall when wall.BlocksMovement():
                    movement.ChangeDirectionCol();
                    break;

                case DungeonTallWall wall when wall.BlocksMovement():
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
                    System.Diagnostics.Debug.WriteLine("Wallmaster took damage");
                    break;
            }
        }
    }
}
