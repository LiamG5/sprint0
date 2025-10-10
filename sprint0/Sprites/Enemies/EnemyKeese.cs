using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using sprint0.Classes;
using sprint0.Interfaces;
using System;
using static sprint0.Sprites.EnemySpriteFactory;

namespace sprint0.Sprites
{
    public class EnemyKeese : ISprite {

        private Texture2D enemySS;
        // private static int enemyTypeX = 6;
        // private static int enemyTypeY = 17;
        private int count = 0;
        private static Vector2 enemyPos = new Vector2(200, 100);
        private static Rectangle frame1 = new Rectangle(16 * 6, 16 * 17, 16, 16);
        private static Rectangle frame2 = new Rectangle(16 * 7, 16 * 17, 16, 16);
        private static Rectangle temp = frame1;
        public EnemyKeese (Texture2D sheet)
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
        public void Draw(SpriteBatch spriteBatch, Vector2 position)
        {
            spriteBatch.Draw(enemySS, position, temp, Color.White, 0f, Vector2.Zero, 3.0f, SpriteEffects.None, 0f);
        }
    }
}
