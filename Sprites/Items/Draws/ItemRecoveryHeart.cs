using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using sprint0.Interfaces;
using sprint0.Classes;
using sprint0.Collisions;
using System;
using Microsoft.Xna.Framework.Content;

namespace sprint0.Sprites
{
    public class ItemRecoveryHeart : IItem {

        private Vector2 position;
        private bool isCollected = false;
        private ItemRecoveryHeartRed heartRed;
        private ItemRecoveryHeartBlue heartBlue;
        private bool isRedFrame = true;
        private float colorTimer = 0f;
        private const float COLOR_SWITCH_INTERVAL = 0.3f;
        private const int ITEM_WIDTH = 45;
        private const int ITEM_HEIGHT = 48;

        public ItemRecoveryHeart(Texture2D sheet, Vector2 startPosition)
        {
            position = startPosition;
            heartRed = new ItemRecoveryHeartRed(sheet, startPosition);
            heartBlue = new ItemRecoveryHeartBlue(sheet, startPosition);
        }

        public ItemRecoveryHeart(Texture2D sheet) : this(sheet, new Vector2(200, 100))
        {
        }

        public void Update(GameTime gameTime)
        {
            if (!isCollected)
            {
                colorTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;
                if (colorTimer >= COLOR_SWITCH_INTERVAL)
                {
                    isRedFrame = !isRedFrame;
                    colorTimer = 0f;
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 drawPosition)
        {
            if (!isCollected)
            {
                if (isRedFrame)
                {
                    heartRed.Draw(spriteBatch, drawPosition, 1.0f);
                }
                else
                {
                    heartBlue.Draw(spriteBatch, drawPosition + new Vector2(-8, -8), 2.0f);
                }
            }
        }

        public Rectangle GetBounds()
        {
            return new Rectangle((int)position.X, (int)position.Y, ITEM_WIDTH, ITEM_HEIGHT);
        }

        public bool BlocksMovement()
        {
            return false;
        }
        
        public bool BlocksProjectiles()
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
                    Inventory.AddHealth(2);
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
            heartRed.Collect();
            heartBlue.Collect();
        }
    }
}
