using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using sprint0.Classes;
using sprint0.Collisions;
using sprint0.Interfaces;
using System;

namespace sprint0.Sprites
{
    public class EnemyBladeTrap : ISprite , IEnemy{

        private Texture2D enemySS;
        private Vector2 position;
        private static int enemyTypeX = 2;
        private static int enemyTypeY = 21;
        private static Rectangle enemy = new Rectangle(16 * enemyTypeX, 16 * enemyTypeY, 16, 16);
        private const int ENEMY_WIDTH = 48;  // 16 * 3.0f scale
        private const int ENEMY_HEIGHT = 48;
        public EnemyBladeTrap (Texture2D sheet, Vector2 startingPosition)
        {
            enemySS = sheet;
            position = startingPosition;
        }
        public EnemyBladeTrap(Texture2D sheet) : this(sheet, new Vector2(200, 100))
        {
        }
        public void Update(GameTime gameTime)
        {
            
        }
        public void Draw(SpriteBatch spriteBatch, Vector2 position)
        {
            spriteBatch.Draw(enemySS, position, enemy, Color.White, 0f, Vector2.Zero, 3.0f, SpriteEffects.None, 0f);
        }

        public void TakeDamage()
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

        public bool IsSolid()
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

                case DungeonLongWall wall when wall.IsSolid():
                    
                    break;

                case DungeonTallWall wall when wall.IsSolid():
                    
                    break;

                case IBlock block when block.IsSolid():
                    
                    break;
            }
        }

    }
}
