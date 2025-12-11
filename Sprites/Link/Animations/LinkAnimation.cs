using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using sprint0.Classes;
using sprint0.Interfaces;
using System;
using System.Collections.Generic;
using Color = Microsoft.Xna.Framework.Color;


namespace sprint0.Classes
{
    public class LinkAnimation : ISprite
    {
        private Color color = Color.White;

        private int superColorsIndex = 0;
        private LinkSprite linkSprite;
        private List<Color> superColors;
        private LinkObjectSprite linkObjSprite;
        private Link player;

        private float superLinkStartTime;



        public LinkAnimation()
        {
            this.linkSprite = new LinkSprite();
            this.linkObjSprite = new LinkObjectSprite();
            this.superColors = new List<Color>();
            this.superLinkStartTime = 0f;
            AddSuperColors();
        }
        private void AddSuperColors()
        {
            superColors.Add(Color.Red);
            superColors.Add(Color.Blue);
            superColors.Add(Color.Green);
            superColors.Add(Color.Yellow);
            superColors.Add(Color.Purple);
            superColors.Add(Color.Orange);
            superColors.Add(Color.Cyan);
            superColors.Add(Color.Magenta);
            superColors.Add(Color.Lime);
            superColors.Add(Color.Pink);
            superColors.Add(Color.Silver);
            superColors.Add(Color.Teal);
            superColors.Add(Color.Gray);
            
        }

        public void SetPlayer(Link player)
        {
            this.player = player;
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
            // Directional bow draw without overlaying bow sprite (body only)
            if (player != null)
            {
                switch (player.direction)
                {
                    case Link.Direction.Up:
                        linkSprite.LinkDrawStandingUp();
                        break;
                    case Link.Direction.Down:
                        linkSprite.LinkDrawStandingDown();
                        break;
                    case Link.Direction.Left:
                        linkSprite.LinkDrawStandingLeft();
                        break;
                    case Link.Direction.Right:
                    default:
                        linkSprite.LinkDrawStandingRight();
                        break;
                }
            }
            else
            {
                linkSprite.LinkDrawStandingRight();
            }
        }
        public void LinkUseItem3(float totalTime, float currentTime)
        {
            linkObjSprite.LinkObjDrawItem3();
                linkSprite.LinkDrawUseItem1();
            
        }

        public void LinkHoldingItem()
        {
            linkSprite.LinkDrawUseItem1();
            linkObjSprite.LinkObjDrawTriforce();
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
        private void SuperLink(GameTime gameTime)
        {
            if(superLinkStartTime == 0f)
            {
                superLinkStartTime = (float)gameTime.TotalGameTime.TotalSeconds;
                superColorsIndex = 0;
            }
            if (superColorsIndex >= superColors.Count)
            {
                superColorsIndex = 0;
            }
            color = superColors[superColorsIndex];
            superColorsIndex++;   

            float currentTime = (float)gameTime.TotalGameTime.TotalSeconds;
        if (superLinkStartTime + 30f < currentTime)
        {
        // End SuperLink
        Inventory.SetSuperLink(false);
        color = Color.White;
        superLinkStartTime = 0f;
        superColorsIndex = 0;
         }

        }


        public void Update(GameTime gameTime )
        {
            linkSprite.Update(gameTime);
            if (Inventory.GetSuperLink())
            {
                SuperLink(gameTime);
            }else{
                if (superLinkStartTime != 0f)
        {
            superLinkStartTime = 0f;
            superColorsIndex = 0;
            color = Color.White;
        }
        }
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 position)
        {
            linkSprite.UpdateColor(color);
            linkSprite.Draw(spriteBatch, position);
            linkObjSprite.Draw(spriteBatch, position);
        }
    }
}