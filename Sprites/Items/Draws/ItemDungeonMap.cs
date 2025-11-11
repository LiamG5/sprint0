using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using sprint0.Interfaces;
using sprint0.Classes;
using sprint0.Collisions;
using System;
using Microsoft.Xna.Framework.Content;

namespace sprint0.Sprites
{
    public class ItemDungeonMap : IItem {

        private Texture2D itemSS;
        private Vector2 position;
        private bool isCollected = false;
        private static int ItemRow = 2;
        private static int ItemCol = 6;
        private static Rectangle block = new Rectangle(40*ItemCol, 40*ItemRow, 15, 16);
        private const int ITEM_WIDTH = 45;
        private const int ITEM_HEIGHT = 48;

        public ItemDungeonMap(Texture2D sheet, Vector2 startPosition)
        {
            itemSS = sheet;
            position = startPosition;
        }

        public ItemDungeonMap(Texture2D sheet) : this(sheet, new Vector2(200, 100))
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
                    Inventory.AcquireMap();
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
