using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using sprint0.Interfaces;
using sprint0.Sprites;
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

        private ItemFactory itemFactory;
        private ISprite item;
        private Boolean show;
        private static Vector2 positionOffset = new Vector2(0,-12);
        
        
        public LinkObjectSprite()
        {
            this.spriteSheet = sprint0.Sprites.Texture2DStorage.GetItemSpriteSheet();
            this.sourceRectangle = new Rectangle(0, 0, 16, 16);
            this.nextRectangle = sourceRectangle;
            this.itemFactory = ItemFactory.Instance;
            show = false;
            item = itemFactory.BuildSword();
            
        }

        //sword
        public void LinkObjDrawItem1()
        {
            item = itemFactory.BuildSmallKey();
            show = true;
        }
        //heart
         public void LinkObjDrawItem2()
        {
            item = itemFactory.BuildBoomerang();
            show = true;
        }
        //rupee
         public void LinkObjDrawItem3()
        {
            item = itemFactory.BuildCompass();
            show = true;
        }
        
        public void Update(GameTime gameTime)
        {
            
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 position)
        {
            if (show)
            {
                Vector2 drawPosition = position + (positionOffset * 3.0f);
                item.Draw(spriteBatch, drawPosition);
            }
            show = false;
        }
    }
}