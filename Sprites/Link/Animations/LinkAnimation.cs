using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using sprint0.Interfaces;
using System;
using Color = Microsoft.Xna.Framework.Color;


namespace sprint0.Classes
{
    public class LinkAnimation : ISprite
    {
        private Color color = Color.White;
        private LinkSprite linkSprite;

        private LinkObjectSprite linkObjSprite;



        public LinkAnimation()
        {
            this.linkSprite = new LinkSprite();
            this.linkObjSprite = new LinkObjectSprite();
        }
           
        public void ChangeColor(Color color)
        {
            this.color = color;
        }

        public void LinkStandingDown()
        {
            linkSprite.LinkDrawStandingDown();
        }

        public void LinkStandingLeft()
        {
            linkSprite.LinkDrawStandingLeft();
        }

        public void LinkStandingUp()
        {
            linkSprite.LinkDrawStandingUp();
        }

        public void LinkStandingRight()
        {
            linkSprite.LinkDrawStandingRight();
        }

        public void LinkWalkingDown(float totalTime, float currentTime)
        {
            float progress = currentTime / totalTime;
            if (progress <= 0.5f)
            {
                linkSprite.LinkDrawWalkingDown();
            }
            else
            {
                linkSprite.LinkDrawStandingDown();
            }
        }

        public void LinkWalkingLeft(float totalTime, float currentTime)
        {
            float progress = currentTime / totalTime;
            if (progress <= 0.5f)
            {
                linkSprite.LinkDrawWalkingLeft();
            }
            else
            {
                linkSprite.LinkDrawStandingLeft();
            }
        }

        public void LinkWalkingUp(float totalTime, float currentTime)
        {
            float progress = currentTime / totalTime;
            if (progress <= 0.5f)
            {
                linkSprite.LinkDrawWalkingUp();
            }
            else
            {
                linkSprite.LinkDrawStandingUp();
            }
        }

        public void LinkWalkingRight(float totalTime, float currentTime)
        {
            float progress = currentTime / totalTime;
            if (progress <= 0.5f)
            {
                linkSprite.LinkDrawWalkingRight();
            }
            else
            {
                linkSprite.LinkDrawStandingRight();
            }
        }

        public void LinkAttackingDown(float totalTime, float currentTime)
        {
            float progress = currentTime / totalTime;
            if (progress <= 0.5f)
            {
                linkSprite.LinkDrawAttacking0Down();
            }
            else
            {
                linkSprite.LinkDrawAttackingDown();
            }
        }

        public void LinkAttackingLeft(float totalTime, float currentTime)
        {
            float progress = currentTime / totalTime;
            if (progress <= 0.5f)
            {
                linkSprite.LinkDrawAttacking0Left();
            }
            else
            {
                linkSprite.LinkDrawAttackingLeft();
            }
        }

        public void LinkAttackingUp(float totalTime, float currentTime)
        {
            float progress = currentTime / totalTime;
            if (progress <= 0.5f)
            {
                linkSprite.LinkDrawAttacking0Up();
            }
            else
            {
                linkSprite.LinkDrawAttackingUp();
            }
        }

        public void LinkAttackingRight(float totalTime, float currentTime)
        {
            float progress = currentTime / totalTime;
            if (progress <= 0.5f)
            {
                linkSprite.LinkDrawAttacking0Right();
            }
            else
            {
                linkSprite.LinkDrawAttackingRight();
            }
        }
        public void LinkBoomerangDown(float totalTime, float currentTime)
        {
            float progress = currentTime / totalTime;
            if (progress <= 0.5f)
            {
                linkSprite.LinkDrawAttacking0Down();
            }
            
        }

        public void LinkBoomerangLeft(float totalTime, float currentTime)
        {
            float progress = currentTime / totalTime;
            if (progress <= 0.5f)
            {
                linkSprite.LinkDrawAttacking0Left();
            }
            
        }

        public void LinkBoomerangUp(float totalTime, float currentTime)
        {
            float progress = currentTime / totalTime;
            if (progress <= 0.5f)
            {
                linkSprite.LinkDrawAttacking0Up();
            }
            
        }

        public void LinkBoomerangRight(float totalTime, float currentTime)
        {
            float progress = currentTime / totalTime;
            if (progress <= 0.5f)
            {
                linkSprite.LinkDrawAttacking0Right();
            }
            
        }


        public void LinkMagicDown(float totalTime, float currentTime)
        {
            float progress = currentTime / totalTime;
            if (progress <= 0.5f)
            {
                linkSprite.LinkDrawAttacking0Down();
            }
            else
            {
                linkSprite.LinkDrawMagicDown();
            }
        }

        public void LinkMagicLeft(float totalTime, float currentTime)
        {
            float progress = currentTime / totalTime;
            if (progress <= 0.5f)
            {
                linkSprite.LinkDrawAttacking0Down();
            }
            else
            {
                linkSprite.LinkDrawMagicDown();
            }
        }

        public void LinkMagicUp(float totalTime, float currentTime)
        {
            float progress = currentTime / totalTime;
            if (progress <= 0.5f)
            {
                linkSprite.LinkDrawAttacking0Up();
            }
            else
            {
                linkSprite.LinkDrawMagicUp();
            }
        }

        public void LinkMagicRight(float totalTime, float currentTime)
        {
            float progress = currentTime / totalTime;
            if (progress <= 0.5f)
            {
                linkSprite.LinkDrawAttacking0Right();
            }
            else
            {
                linkSprite.LinkDrawMagicRight();
            }
        }

        public void LinkUseItem1(float totalTime, float currentTime)
        {
            
            linkSprite.LinkDrawUseItem1();
            linkObjSprite.LinkObjDrawItem1(); 
            
        }
        public void LinkUseItem2(float totalTime, float currentTime)
        {
            linkObjSprite.LinkObjDrawItem2();
                linkSprite.LinkDrawUseItem1();
            
        }
        public void LinkUseItem3(float totalTime, float currentTime)
        {
            linkObjSprite.LinkObjDrawItem3();
                linkSprite.LinkDrawUseItem1();
            
        }

        public void LinkDamagedUp(float totalTime, float currentTime)
        {
            float progress = currentTime / totalTime;
            if (progress <= 0.25f)
            {
                color = Color.Red;
                linkSprite.LinkDrawStandingUp();
            }
            else if (progress <= 0.5f)
            {
                color = Color.White;
                linkSprite.LinkDrawStandingUp();
            }
            else if (progress <= 0.75f)
            {
                color = Color.Red;
                linkSprite.LinkDrawStandingUp();
            }
            else
            {
                color = Color.White;
                linkSprite.LinkDrawStandingUp();
            }
        }

        public void LinkDamagedDown(float totalTime, float currentTime)
        {
            float progress = currentTime / totalTime;
            if (progress <= 0.25f)
            {
                color = Color.Red;
                linkSprite.LinkDrawStandingDown();
            }
            else if (progress <= 0.5f)
            {
                color = Color.White;
                linkSprite.LinkDrawStandingDown();
            }
            else if (progress <= 0.75f)
            {
                color = Color.Red;
                linkSprite.LinkDrawStandingDown();
            }
            else
            {
                color = Color.White;
                linkSprite.LinkDrawStandingDown();
            }
        }

        public void LinkDamagedLeft(float totalTime, float currentTime)
        {
            float progress = currentTime / totalTime;
            if (progress <= 0.25f)
            {
                color = Color.Red;
                linkSprite.LinkDrawStandingLeft();
            }
            else if (progress <= 0.5f)
            {
                color = Color.White;
                linkSprite.LinkDrawStandingLeft();
            }
            else if (progress <= 0.75f)
            {
                color = Color.Red;
                linkSprite.LinkDrawStandingLeft();
            }
            else
            {
                color = Color.White;
                linkSprite.LinkDrawStandingLeft();
            }
        }

        public void LinkDamagedRight(float totalTime, float currentTime)
        {
            float progress = currentTime / totalTime;
            if (progress <= 0.25f)
            {
                color = Color.Red;
                linkSprite.LinkDrawStandingRight();
            }
            else if (progress <= 0.5f)
            {
                color = Color.White;
                linkSprite.LinkDrawStandingRight();
            }
            else if (progress <= 0.75f)
            {
                color = Color.Red;
                linkSprite.LinkDrawStandingRight();
            }
            else
            {
                color = Color.White;
                linkSprite.LinkDrawStandingRight();
            }
        }


        public void Update(GameTime gameTime)
        {
            linkSprite.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 position)
        {
            linkSprite.UpdateColor(color);
            linkSprite.Draw(spriteBatch, position);
            linkObjSprite.Draw(spriteBatch, position);
            
        }
    }
}