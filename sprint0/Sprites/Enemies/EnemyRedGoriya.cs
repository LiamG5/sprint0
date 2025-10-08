using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using sprint0.Interfaces;
using sprint0.Classes;
using System;
using Microsoft.Xna.Framework.Content;

namespace sprint0.Sprites
{
    public class EnemyRedGoriya : ISprite {

        private Texture2D enemySS;
        // private static int enemyTypeX = 1;
        // private static int enemyTypeY = 2;
        private int count = 0;
        private static Vector2 enemyPos = new Vector2(200, 100);
        private static Rectangle frame1 = new Rectangle(16 * 0, 16 * 2, 16, 16);
        private static Rectangle frame2 = new Rectangle(16 * 1, 16 * 2, 16, 16);
        private static Rectangle temp = frame1;
        public EnemyRedGoriya (Texture2D sheet)
        {
            enemySS = sheet;
        }
        public void Update(GameTime gameTime)
        {
            count++;
            if (count > 9)
            {
                count = 0;
                if (temp == frame1)
                {
                    temp = frame2;
                }
                else
                {
                    temp = frame1;
                }
            }

        }
        public void Draw(SpriteBatch sprite)
        {
            sprite.Draw(enemySS, enemyPos, temp, Color.White);
        }
    }
}
