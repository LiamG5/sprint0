using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using sprint0.Interfaces;
using System;
using System.Drawing;
using Color = Microsoft.Xna.Framework.Color;
using Rectangle = Microsoft.Xna.Framework.Rectangle;

namespace sprint0.Classes
{
    public class LinkSprite : ISprite
    {
        public Texture2D spriteSheet;
        public Rectangle sourceRectangle;
        public Rectangle nextRectangle;
        private int linkColor = 1;
        private Vector2 positionOffset;
        private Color DamgColor;




        public LinkSprite()
        {
            this.spriteSheet = sprint0.Sprites.Texture2DStorage.GetLinkSpriteSheet();
            if (this.spriteSheet == null)
            {
                throw new InvalidOperationException("Link sprite sheet not loaded. Make sure Texture2DStorage.LoadAllTextures() is called before creating LinkSprite.");
            }
            this.sourceRectangle = new Rectangle(0 * linkColor, 0, 16, 16);
            this.nextRectangle = sourceRectangle;
            this.DamgColor = Color.White;
            this.positionOffset = Vector2.Zero;
        }

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

        public void LinkDrawAttackingDown()
        {
            nextRectangle = new Rectangle(0 * linkColor, 84, 16, 28);
        }

        public void LinkDrawAttackingLeft()
        {
            nextRectangle = new Rectangle(24 * linkColor, 90, 28, 16);
            positionOffset = new Vector2(-12, 0);
        }

        public void LinkDrawAttackingUp()
        {

            nextRectangle = new Rectangle(60 * linkColor, 84, 16, 28);
            positionOffset = new Vector2(0, -12);
        }

        public void LinkDrawAttackingRight()
        {
            nextRectangle = new Rectangle(84 * linkColor, 90, 28, 16);
        }

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

        public void UpdateColor(Color color)
        {
            DamgColor = color;
        }

        public void Update(GameTime gameTime)
        {
            sourceRectangle = nextRectangle;
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 position)
        {   
            if (spriteSheet != null)
            {
                Vector2 drawPosition = position + (positionOffset * 3.0f);
                spriteBatch.Draw(spriteSheet, drawPosition, sourceRectangle, DamgColor, 0f, Vector2.Zero, 3.0f, SpriteEffects.None, 0f);
            }
            positionOffset = Vector2.Zero;
        }
    }
}