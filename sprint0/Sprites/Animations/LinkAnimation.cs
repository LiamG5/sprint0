using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using sprint0.Interfaces;
using System;

namespace sprint0.Classes
{
    public class LinkAnimation : ISprite
    {
        int frameNum = 0; 
        private LinkSprite linkSprite;



        public LinkAnimation()
        {
            this.linkSprite = new LinkSprite();
            
        }

        // Row 1 - Standing animations
        public void LinkStandingDown()
        {
            linkSprite.LinkDrawStandingDown();
            frameNum = 0;
        }

        public void LinkStandingLeft()
        {
            linkSprite.LinkDrawStandingLeft();
            frameNum = 0;
        }

        public void LinkStandingUp()
        {
            linkSprite.LinkDrawStandingUp();
            frameNum = 0;
        }

        public void LinkStandingRight()
        {
            linkSprite.LinkDrawStandingRight();
            frameNum = 0;

        }

        // Row 2 - Walking animations
        public void LinkWalkingDown()
        {
            switch (frameNum) {
    case 0:
                    linkSprite.LinkDrawWalkingDown();
                    frameNum = 1;
        break;
    case 1:
                    linkSprite.LinkDrawStandingDown();
                    frameNum = 0;
        
        break;
                    linkSprite.LinkDrawStandingDown();
                    frameNum = 0;
    default: 
        
        break; // Optional after default
}
            
        }

        public void LinkWalkingLeft()
        {
            switch (frameNum)
            {
                case 0:
                    linkSprite.LinkDrawWalkingLeft();
                    frameNum = 1;
                    break;
                case 1:
                    linkSprite.LinkDrawStandingLeft();
                    frameNum = 0;
                    break;

                default:
                    linkSprite.LinkDrawStandingLeft();
                    frameNum = 0;
                    break;

            }
        }

        public void LinkWalkingUp()
        {
            switch (frameNum)
            {
                case 0:
                    linkSprite.LinkDrawWalkingUp();
                    frameNum = 1;
                    break;
                case 1:
                    linkSprite.LinkDrawStandingUp();
                    frameNum = 0;
                    break;

                default:
                    linkSprite.LinkDrawStandingUp();
                    frameNum = 0;
                    break;

            }

        }

        public void LinkWalkingRight()
        {
            switch (frameNum)
            {
                case 0:
                    linkSprite.LinkDrawWalkingRight();
                    frameNum = 1;
                    break;
                case 1:
                    linkSprite.LinkDrawStandingRight();
                    frameNum = 0;
                    break;


                default:
                    linkSprite.LinkDrawStandingRight();
                    frameNum = 0;
                    break;
                    
            }
            
        }




        // Row 3&4 - Attacking animations
        public void LinkAttackingDown()
        {
            switch (frameNum)
            {
                case 0:
                    linkSprite.LinkDrawAttacking0Down();
                    frameNum = 1;
                    break;
                case 1:
                    linkSprite.LinkDrawAttackingDown();
                    frameNum = 0;
                    break;


                default:
                    linkSprite.LinkDrawStandingDown();
                    frameNum = 0;
                    break;
 
            }
            
        }

        public void LinkAttackingLeft()
        {
            switch (frameNum)
            {
                case 0:
                    linkSprite.LinkDrawAttacking0Left();
                    frameNum = 1;
                    break;
                case 1:
                    linkSprite.LinkDrawAttackingLeft();
                    frameNum = 0;
                    break;


                default:
                    linkSprite.LinkDrawStandingLeft();
                    frameNum = 0;
                    break;
 
            }
        }

        public void LinkAttackingUp()
        {
switch (frameNum)
            {
                case 0:
                    linkSprite.LinkDrawAttacking0Up();
                    frameNum = 1;
                    break;
                case 1:
                    linkSprite.LinkDrawAttackingUp();
                    frameNum = 0;
                    break;


                default:
                    linkSprite.LinkDrawStandingUp();
                    frameNum = 0;
                    break;
 
            }   
        }

        public void LinkAttackingRight()
        {
           switch (frameNum)
            {
                case 0:
                    linkSprite.LinkDrawAttacking0Right();
                    frameNum = 1;
                    break;
                case 1:
                    linkSprite.LinkDrawAttackingRight();
                    frameNum = 0;
                    break;


                default:
                    linkSprite.LinkDrawStandingRight();
                    frameNum = 0;
                    break;
 
            }
        }

        // Row 3&5 - Magic animations
        public void LinkMagicDown()
        {
            switch (frameNum)
            {
                case 0:
                    linkSprite.LinkDrawAttacking0Down();
                    frameNum = 1;
                    break;
                case 1:
                    linkSprite.LinkDrawMagicDown();
                    frameNum = 0;
                    break;


                default:
                    linkSprite.LinkDrawStandingDown();
                    frameNum = 0;
                    break;
 
            }
            
        }

        public void LinkMagicLeft()
        {
           switch (frameNum)
            {
                case 0:
                    linkSprite.LinkDrawAttacking0Down();
                    frameNum = 1;
                    break;
                case 1:
                    linkSprite.LinkDrawMagicDown();
                    frameNum = 0;
                    break;


                default:
                    linkSprite.LinkDrawStandingDown();
                    frameNum = 0;
                    break;
 
            }
        }

        public void LinkMagicUp()
        {
            switch (frameNum)
            {
                case 0:
                    linkSprite.LinkDrawAttacking0Up();
                    frameNum = 1;
                    break;
                case 1:
                    linkSprite.LinkDrawMagicUp();
                    frameNum = 0;
                    break;


                default:
                    linkSprite.LinkDrawStandingUp();
                    frameNum = 0;
                    break;
 
            }
        }

        public void LinkMagicRight()
        {
            switch (frameNum)
            {
                case 0:
                    linkSprite.LinkDrawAttacking0Right();
                    frameNum = 1;
                    break;
                case 1:
                    linkSprite.LinkDrawMagicRight();
                    frameNum = 0;
                    break;


                default:
                    linkSprite.LinkDrawStandingRight();
                    frameNum = 0;
                    break;
 
            }
        }

        public void LinkUseItem()
        {
            switch (frameNum)
            {
                case 0:
                    linkSprite.LinkDrawUseItem1();
                    frameNum = 1;
                    break;
                case 1:
                    linkSprite.LinkDrawUseItem1();
                    frameNum = 0;
                    break;


                default:
                    linkSprite.LinkDrawStandingUp();
                    frameNum = 0;
                    break;
 
            }
            
        }
        

        public void Update(GameTime gameTime)
        {

        }

        public void Draw(SpriteBatch spriteBatch, Vector2 position)
        {
            linkSprite.Draw(spriteBatch, position);
            
        }
    }
}