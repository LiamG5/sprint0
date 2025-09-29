using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using sprint0.Interfaces;
using sprint0.Classes;
using System;

namespace sprint0.Classes
{
    public class LinkSprite : ISprite
    {
        private Texture2D spriteSheet = Texture2DStorage.GetLinkSpriteSheet();
        private Vector2 position;
        private Direction currentDirection;
        public ILinkState State { get; set; }

        // Constructor for bridge functionality
        public LinkSprite(Vector2 startPosition)
        {
            position = startPosition;
            currentDirection = Direction.Down;
            State = new State1(this); // Start with State1
        }

        // Constructor for existing ISprite usage
        public LinkSprite()
        {
            position = new Vector2(100, 300); // Default position
            currentDirection = Direction.Down;
        }

        public void SetDirection(Direction direction)
        {
            currentDirection = direction;
        }

        public Direction GetDirection()
        {
            return currentDirection;
        }

        public Vector2 GetPosition()
        {
            return position;
        }

        public void SetPosition(Vector2 position)
        {
            position = position;
        }

        public void Update()
        {
            State.Update();
        }

        public void ChangeState(ILinkState newState)
        {
            State = newState;
        }

        // Movement methods for states to call
        public void MoveLeft() { position += new Vector2(-5, 0); currentDirection = Direction.Left; }
        public void MoveRight() { position += new Vector2(5, 0); currentDirection = Direction.Right; }
        public void MoveUp() { position += new Vector2(0, -5); currentDirection = Direction.Up; }
        public void MoveDown() {position += new Vector2(0, 5); currentDirection = Direction.Down; }

        public void Attack() { /* Your attack logic here */ }
        public void UseItem1() { /* Your item logic here */ }
        public void UseItem2() { /* Your item logic here */ }
        public void UseItem3() { /* Your item logic here */ }
        public void TakeDamage() { /* Your damage logic here */ }

        public void Update(GameTime gameTime)
        {
            Update();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            Rectangle sourceRect = GetCurrentFrame();
            spriteBatch.Draw(spriteSheet, _position, sourceRect, Color.White);
        }

        private Rectangle GetCurrentFrame()
        {
            return new Rectangle(1, 11, 16, 16); // Default frame
        }

        public Texture2D GetSpriteSheet()
        {
            return spriteSheet;
        }
    }
}