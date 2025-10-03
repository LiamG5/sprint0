using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using sprint0.Interfaces;
using System;

namespace sprint0.Classes
{
    public class LinkSprite : ISprite
    {
        public Texture2D spriteSheet;
        public Rectangle sourceRectangle;
        public Rectangle nextRectangle;
        private int linkColor = 1;

        

        
        public LinkSprite(Texture2D spriteSheet)
        {
            this.spriteSheet = spriteSheet;
            this.sourceRectangle = new Rectangle(0 * linkColor, 0, 16, 16);
            this.nextRectangle = sourceRectangle;
        }

        // Row 1 - Standing animations
        public void LinkDrawStandingDown()
        {
            nextRectangle = new Rectangle(0 * linkColor, 0, 16, 16);
        }

        public void LinkDrawStandingLeft()
        {
            nextRectangle = new Rectangle(30 * linkColor, 0, 16, 16);
        }

        public void LinkDrawStandingUp()
        {
            nextRectangle = new Rectangle(60 * linkColor, 0, 16, 16);
        }

        public void LinkDrawStandingRight()
        {
            nextRectangle = new Rectangle(90 * linkColor, 0, 16, 16);
        }

        // Row 2 - Walking animations
        public void LinkDrawWalkingDown()
        {
            nextRectangle = new Rectangle(0 * linkColor, 30, 16, 16);
        }

        public void LinkDrawWalkingLeft()
        {
            nextRectangle = new Rectangle(30 * linkColor, 30, 16, 16);
        }

        public void LinkDrawWalkingUp()
        {
            nextRectangle = new Rectangle(60 * linkColor, 30, 16, 16);
        }

        public void LinkDrawWalkingRight()
        {
            nextRectangle = new Rectangle(90 * linkColor, 30, 16, 16);
        }

        // Row 3 - Pushing animations
        public void LinkDrawAttacking0Down()
        {
            nextRectangle = new Rectangle(0 * linkColor, 60, 16, 16);
        }

        public void LinkDrawAttacking0Left()
        {
            nextRectangle = new Rectangle(30 * linkColor, 60, 16, 16);
        }

        public void LinkDrawAttacking0Up()
        {
            nextRectangle = new Rectangle(60 * linkColor, 60, 16, 16);
        }

        public void LinkDrawAttacking0Right()
        {
            nextRectangle = new Rectangle(90 * linkColor, 60, 16, 16);
        }

        // Row 4 - Attacking animations
        public void LinkDrawAttackingDown()
        {
            nextRectangle = new Rectangle(0 * linkColor, 90, 16, 16);
        }

        public void LinkDrawAttackingLeft()
        {
            nextRectangle = new Rectangle(30 * linkColor, 90, 16, 16);
        }

        public void LinkDrawAttackingUp()
        {
            nextRectangle = new Rectangle(60 * linkColor, 90, 16, 16);
        }

        public void LinkDrawAttackingRight()
        {
            nextRectangle = new Rectangle(90 * linkColor, 90, 16, 16);
        }

        // Row 5 - Magic animations
        public void LinkDrawMagicDown()
        {
            nextRectangle = new Rectangle(0 * linkColor, 120, 16, 16);
        }

        public void LinkDrawMagicLeft()
        {
            nextRectangle = new Rectangle(30 * linkColor, 120, 16, 16);
        }

        public void LinkDrawMagicUp()
        {
            nextRectangle = new Rectangle(60 * linkColor, 120, 16, 16);
        }

        public void LinkDrawMagicRight()
        {
            nextRectangle = new Rectangle(90 * linkColor, 120, 16, 16);
        }
        
        public void LinkDrawUseItem1()
        {
            nextRectangle = new Rectangle(0 * linkColor, 150, 16, 16);
        }
        public void LinkDrawUseItem2()
        {
            nextRectangle = new Rectangle(30 * linkColor, 150, 16, 16);
        }

        public void Update(GameTime gameTime)
        {

            sourceRectangle = nextRectangle;


        }

        public void Draw(SpriteBatch spriteBatch, Vector2 position)
        {
            
            spriteBatch.Draw(spriteSheet, position, sourceRectangle, Color.White);
        }
    }
}