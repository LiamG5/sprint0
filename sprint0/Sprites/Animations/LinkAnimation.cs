using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using sprint0.Interfaces;
using sprint0.Classes;

namespace sprint0.Sprites.Animations
{
    public class LinkAnimation
    {
        private Color color = Color.White;
        private LinkSprite linkSprite;

        public LinkAnimation(LinkSprite linkSprite)
        {
            this.linkSprite = linkSprite;
        }
           
        public void ChangeColor(Color color)
        {
            this.color = color;
        }

        // Row 1 - Standing animations
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

        // Row 2 - Walking animations
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

        // Row 3&4 - Attacking animations
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

        // Row 3&5 - Magic animations
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

        public void LinkUseItem(float totalTime, float currentTime)
        {
            float progress = currentTime / totalTime;
            if (progress <= 0.5f)
            {
                linkSprite.LinkDrawUseItem1();
            }
            else
            {
                linkSprite.LinkDrawUseItem1();
            }
        }

        public void LinkDamagedUp(float totalTime, float currentTime)
        {
            float progress = currentTime / totalTime;
            if (progress <= 0.25f)
            {
                color = Color.White;
                linkSprite.LinkDrawStandingUp();
            }
            else if (progress <= 0.5f)
            {
                color = Color.Red;
                linkSprite.LinkDrawStandingUp();
            }
            else if (progress <= 0.75f)
            {
                color = Color.White;
                linkSprite.LinkDrawStandingUp();
            }
            else
            {
                color = Color.Red;
                linkSprite.LinkDrawStandingUp();
            }
        }

        public void LinkDamagedDown(float totalTime, float currentTime)
        {
            float progress = currentTime / totalTime;
            if (progress <= 0.25f)
            {
                color = Color.White;
                linkSprite.LinkDrawStandingDown();
            }
            else if (progress <= 0.5f)
            {
                color = Color.Red;
                linkSprite.LinkDrawStandingDown();
            }
            else if (progress <= 0.75f)
            {
                color = Color.White;
                linkSprite.LinkDrawStandingDown();
            }
            else
            {
                color = Color.Red;
                linkSprite.LinkDrawStandingDown();
            }
        }

        public void LinkDamagedLeft(float totalTime, float currentTime)
        {
            float progress = currentTime / totalTime;
            if (progress <= 0.25f)
            {
                color = Color.White;
                linkSprite.LinkDrawStandingLeft();
            }
            else if (progress <= 0.5f)
            {
                color = Color.Red;
                linkSprite.LinkDrawStandingLeft();
            }
            else if (progress <= 0.75f)
            {
                color = Color.White;
                linkSprite.LinkDrawStandingLeft();
            }
            else
            {
                color = Color.Red;
                linkSprite.LinkDrawStandingLeft();
            }
        }

        public void LinkDamagedRight(float totalTime, float currentTime)
        {
            float progress = currentTime / totalTime;
            if (progress <= 0.25f)
            {
                color = Color.White;
                linkSprite.LinkDrawStandingRight();
            }
            else if (progress <= 0.5f)
            {
                color = Color.Red;
                linkSprite.LinkDrawStandingRight();
            }
            else if (progress <= 0.75f)
            {
                color = Color.White;
                linkSprite.LinkDrawStandingRight();
            }
            else
            {
                color = Color.Red;
                linkSprite.LinkDrawStandingRight();
            }
        }


        public void Update(GameTime gameTime)
        {
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 position)
        {
            linkSprite.Draw(spriteBatch, position, color);
        }
    }
}