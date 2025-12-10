using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using sprint0.Interfaces;
using sprint0.Classes;
using sprint0.Collisions;
using System;
using Microsoft.Xna.Framework.Content;
using sprint0.Sprites.Enemies;

namespace sprint0.Sprites
{
    public class ItemFairy : IItem {

        private Texture2D itemSS;
        private bool isCollected = false;
        private static int ItemRow = 1;
        private static int ItemCol = 3;
        private Rectangle frame1 = new Rectangle(40 * ItemCol, 40 * ItemRow, 15, 16);
        private Rectangle frame2 = new Rectangle(40 * (ItemCol + 1), 40 * ItemRow, 15, 16);
        private EnemyAnimationHelper animation;
        private EnemyMovementCycle movement;
        private const int ITEM_WIDTH = 45;
        private const int ITEM_HEIGHT = 48;

        // NEW: constructor with target provider (for chasing player)
        public ItemFairy(Texture2D sheet, Vector2 startPosition, Func<Vector2> targetProvider)
        {
            itemSS = sheet;
            movement = new EnemyMovementCycle(startPosition, targetProvider);
            animation = new EnemyAnimationHelper(frame1, frame2);
        }

        // OLD constructors now call the new one with null (random movement)
        public ItemFairy(Texture2D sheet, Vector2 startPosition)
            : this(sheet, startPosition, null)
        {
        }

        public ItemFairy(Texture2D sheet) : this(sheet, new Vector2(200, 100))
        {
        }

        public void Update(GameTime gameTime)
        {
            if (!isCollected)
            {
                movement.Move();
                animation.Update(gameTime);
            }
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 drawPosition)
        {
            if (!isCollected)
            {
                spriteBatch.Draw(itemSS, movement.GetPosition(), animation.GetFrame(), Color.White, 0f, Vector2.Zero, 3.0f, SpriteEffects.None, 0f);
            }
        }

        public Rectangle GetBounds()
        {
            return new Rectangle((int)movement.GetPosition().X, (int)movement.GetPosition().Y, ITEM_WIDTH, ITEM_HEIGHT);
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
            return movement.GetPosition();
        }

        public void OnCollision(ICollidable other, CollisionDirection direction)
        {
            switch (other)
            {
                case Link link:
                    Inventory.GetFairy();
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
