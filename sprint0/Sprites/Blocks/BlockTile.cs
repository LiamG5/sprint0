using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using sprint0.Interfaces;
using sprint0.Classes;
using System;
using Microsoft.Xna.Framework.Content;

namespace sprint0.Sprites
{
    public class BlockTile : IBlock, ISprite {

        private Texture2D blockSS;
        private static int blockType = 0;
        private Vector2 position = new Vector2(100, 100);
        private static Rectangle block = new Rectangle(16 * blockType, 0, 16, 16);
        private Vector2 position;

        public BlockTile (Texture2D sheet)
        {
            blockSS = sheet;
        }
        
        public void Update(GameTime gameTime)
        {
            
        }
        
        public void Draw(SpriteBatch spriteBatch, Vector2 position)
        {
            this.position = position;
            spriteBatch.Draw(blockSS, position, block, Color.White, 0f, Vector2.Zero, 3.0f, SpriteEffects.None, 0f);
        }
        
        public Rectangle GetBounds()
        {
<<<<<<< HEAD
            return new Rectangle((int)position.X, (int)position.Y, 48, 48);
=======
            return new Rectangle((int)position.X, (int)position.Y, 48, 48); // 16 * 3.0f scale
>>>>>>> CollisionDetection
        }
        
        public bool IsSolid()
        {
<<<<<<< HEAD
            return true;
=======
            return true; // Tile blocks are solid
>>>>>>> CollisionDetection
        }
        
        public Vector2 GetPosition()
        {
            return position;
        }
<<<<<<< HEAD
        
        public void SetPosition(Vector2 position)
        {
            this.position = position;
        }
=======
>>>>>>> CollisionDetection
    }
}
