using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using sprint0.Interfaces;
using sprint0.Classes;
using System;
using Microsoft.Xna.Framework.Content;

namespace sprint0.Sprites
{
    public class ItemFairy : ISprite {

        private Texture2D itemSS;
        private static int ItemRow = 1;
        private static int ItemCol = 3;
        private static Rectangle frame1 = new Rectangle(40 * ItemCol, 40 * ItemRow, 15, 16);
        private static Rectangle frame2 = new Rectangle(40 * ItemCol, 40 * ItemRow, 15, 16);
        private static Rectangle temp = frame1;
        private int count = 0;
        

        public ItemFairy(Texture2D sheet)
        {
            itemSS = sheet;
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
            spriteBatch.Draw(itemSS, position, temp, Color.White, 0f, Vector2.Zero, 3.0f, SpriteEffects.None, 0f);
        }
    }
}
