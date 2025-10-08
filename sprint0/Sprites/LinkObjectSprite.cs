using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using sprint0.Interfaces;
using System;
using Color = Microsoft.Xna.Framework.Color;
using Rectangle = Microsoft.Xna.Framework.Rectangle;

namespace sprint0.Classes
{
    public class LinkObjectSprite : ISprite
    {
        public Texture2D spriteSheet;
        public Rectangle sourceRectangle;
        public Rectangle nextRectangle;

        

        
        public LinkObjectSprite()
        {
            this.spriteSheet = Texture2DStorage.GetLinkSpriteSheet();
            this.sourceRectangle = new Rectangle(0, 0, 16, 16);
            this.nextRectangle = sourceRectangle;
        }

        //sword
        public void LinkObjDrawItem1()
        {
            nextRectangle = new Rectangle(30, 60, 16, 16);
        }
        //heart
         public void LinkObjDrawItem2()
        {
            nextRectangle = new Rectangle(300, 195, 16, 16);
        }
        //rupee
         public void LinkObjDrawItem3()
        {
            nextRectangle = new Rectangle(240, 225, 16, 16);
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