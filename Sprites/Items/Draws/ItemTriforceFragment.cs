using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using sprint0.Interfaces;
using sprint0.Classes;
using sprint0.Collisions;
using System;
using Microsoft.Xna.Framework.Content;

namespace sprint0.Sprites
{
    public class ItemTriforceFragment : IItem {

        private Texture2D itemSS;
        private Vector2 position;
        private bool isCollected = false;
        private static Rectangle block = new Rectangle(320, 120, 16, 16);
        private const int ITEM_WIDTH = 48;  // 16 * 3.0f
        private const int ITEM_HEIGHT = 48;
        private int frameNum = 0;

        public ItemTriforceFragment(Texture2D sheet, Vector2 startPosition)
        {
            itemSS = sheet;
            position = startPosition;
        }

        public ItemTriforceFragment(Texture2D sheet) : this(sheet, new Vector2(200, 100))
        {
        }

        public void Update(GameTime gameTime)
        {
            
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 drawPosition)
        {

            if (!isCollected)
            {

                spriteBatch.Draw(itemSS, drawPosition, block, Color.White, 0f, Vector2.Zero, 3.0f, SpriteEffects.None, 0f);
                frameNum++;
                if (frameNum == 6)
                {
                block = new Rectangle(340, 120, 16, 16);
                }
                else if(frameNum == 12)
                {
                    block = new Rectangle(320, 120, 16, 16);
                    frameNum = 0;
                }
            }
        }

        public Rectangle GetBounds()
        {
            return new Rectangle((int)position.X, (int)position.Y, ITEM_WIDTH, ITEM_HEIGHT);
        }

        public bool IsSolid()
        {
            return false;
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
                    Collect();
                    break;
            }
        }

        public bool IsCollected()
        {
            return isCollected;
        }

        public void Collect()
        {
            isCollected = true;
        }
    }
}
