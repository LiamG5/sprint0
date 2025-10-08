using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using sprint0.Interfaces;
using sprint0.Classes;
using System;
using Microsoft.Xna.Framework.Content;

namespace sprint0.Sprites
{
    public class BlockGrate : IBlock, ISprite {

        private Texture2D blockSS;
        private static int blockType = 9;
        private static Vector2 blockPos = new Vector2(100, 100);
        private static Rectangle block = new Rectangle(16 * blockType, 0, 16, 16);

        public BlockGrate(Texture2D sheet)
        {
            blockSS = sheet;
        }
        public void Update(GameTime gameTime)
        {
            
        }
        public void Draw(SpriteBatch spriteBatch, Vector2 position)
        {
            float scale = 3.0f; // Make sprites 3x bigger
            spriteBatch.Draw(blockSS, position, block, Color.White, 0f, Vector2.Zero, scale, SpriteEffects.None, 0f);
        }
    }
}
