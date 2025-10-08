using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using sprint0.Interfaces;
using sprint0.Classes;
using System;
using Microsoft.Xna.Framework.Content;

namespace sprint0.Sprites
{
    public class BlockBrick : IBlock, ISprite {

        private Texture2D blockSS;
        private static int blockType = 8;
        private static Vector2 blockPos = new Vector2(100, 100);
        private static Rectangle block = new Rectangle(16 * blockType, 0, 16, 16);

        public BlockBrick(Texture2D sheet)
        {
            blockSS = sheet;
        }
        public void Update(GameTime gameTime)
        {
            
        }
        public void Draw(SpriteBatch spriteBatch, Vector2 position)
        {
            spriteBatch.Draw(blockSS, position, block, Color.White);
        }
    }
}
