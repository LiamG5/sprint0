using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using sprint0.Interfaces;
using sprint0.Classes;
using System;
using Microsoft.Xna.Framework.Content;

namespace sprint0.Sprites
{
    public class BlockDirt : IBlock, ISprite {

        private Texture2D blockSS;
        private static int blockType = 5;
        private static Vector2 blockPos = new Vector2(100, 100);
        private static Rectangle block = new Rectangle(16 * blockType, 0, 16, 16);
        private Vector2 position;

        public BlockDirt(Texture2D sheet, Vector2 pos)
        {
            blockSS = sheet;
            position = pos;
        }
        public void Update(GameTime gameTime)
        {
            
        }
        public void Draw(SpriteBatch spriteBatch, Vector2 position)
        {
            spriteBatch.Draw(blockSS, position, block, Color.White, 0f, Vector2.Zero, 3.0f, SpriteEffects.None, 0f);
        }
        
        public Rectangle GetBounds()
        {
            return new Rectangle((int)position.X, (int)position.Y, 48, 48);
        }
        
        public bool IsSolid()
        {
            return false;
        }
        
        public Vector2 GetPosition()
        {
            return position;
        }
    }
}
