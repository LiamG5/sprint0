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
        private bool showTriforce;
        private int triforceFrameNum = 0;
        private static Vector2 positionOffset = new Vector2(0,-12);
        private Link.Direction bowDirection = Link.Direction.Down;
        
        
        public LinkObjectSprite()
        {
            this.spriteSheet = sprint0.Sprites.Texture2DStorage.GetItemSpriteSheet();
            this.sourceRectangle = new Rectangle(0, 0, 16, 16);
            this.nextRectangle = sourceRectangle;
            this.itemFactory = ItemFactory.Instance;
            show = false;
            showTriforce = false;
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
            item = itemFactory.BuildBow();
            show = true;
        }
        // bow with direction
        public void LinkObjDrawBow(Link.Direction direction)
        {
            item = itemFactory.BuildBow();
            show = true;
            bowDirection = direction;
        }
        //rupee
         public void LinkObjDrawItem3()
        {
            item = itemFactory.BuildCompass();
            show = true;
        }

        public void LinkObjDrawTriforce()
        {
            showTriforce = true;
        }
        
        public void Update(GameTime gameTime)
        {
            
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 position)
        {
            if (show)
            {
                Vector2 drawPosition = position;
                switch (bowDirection)
                {
                    case Link.Direction.Up:
                        drawPosition += new Vector2(0, -8);
                        break;
                    case Link.Direction.Down:
                        drawPosition += new Vector2(0, 8);
                        break;
                    case Link.Direction.Left:
                        drawPosition += new Vector2(-8, 0);
                        break;
                    case Link.Direction.Right:
                        drawPosition += new Vector2(8, 0);
                        break;
                }
                item.Draw(spriteBatch, drawPosition);
            }
            if (showTriforce)
            {
                Vector2 drawPosition = position + (positionOffset * 3.0f);
                Rectangle triforceRect;
                
                triforceFrameNum++;
                if (triforceFrameNum < 6)
                {
                    triforceRect = new Rectangle(320, 120, 16, 16);
                }
                else if (triforceFrameNum < 12)
                {
                    triforceRect = new Rectangle(340, 120, 16, 16);
                }
                else
                {
                    triforceRect = new Rectangle(320, 120, 16, 16);
                    triforceFrameNum = 0;
                }
                
                spriteBatch.Draw(spriteSheet, drawPosition, triforceRect, Color.White, 0f, Vector2.Zero, 3.0f, SpriteEffects.None, 0f);
            }
            show = false;
            showTriforce = false;
        }
    }
}