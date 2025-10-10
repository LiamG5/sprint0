using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using sprint0.Interfaces;
using sprint0.Classes;
using System;
using Microsoft.Xna.Framework.Content;

namespace sprint0.Sprites
{
    public class ItemCandleBlue : ISprite {

        private Texture2D itemSS;
        private static int ItemRow = 0;
        private static int ItemCol = 3;
       private static Rectangle block = new Rectangle(40*ItemCol, 40*ItemRow, 15, 16);

        public ItemCandleBlue(Texture2D sheet)
        {
            itemSS = sheet;
        }
        public void Update(GameTime gameTime)
        {
            
        }
        public void Draw(SpriteBatch spriteBatch, Vector2 position)
        {
            spriteBatch.Draw(itemSS, position, block, Color.White, 0f, Vector2.Zero, 3.0f, SpriteEffects.None, 0f);
        }
    }
}
