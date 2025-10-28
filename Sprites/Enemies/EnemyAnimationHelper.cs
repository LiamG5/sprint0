using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using sprint0.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sprint0.Sprites.Enemies
{
    public class EnemyAnimationHelper : IAnimate
    {

        private int count = 0;
        private static Rectangle frame1;
        private static Rectangle frame2;
        private static Rectangle temp;

        public EnemyAnimationHelper(Rectangle frame1, Rectangle frame2)
        {
            EnemyAnimationHelper.frame1 = frame1;
            EnemyAnimationHelper.frame2 = frame2;
            temp = frame1;
        }

        private void DefaultAnimation()
        {
            count++;
            if (count > 9)
            {
                count = 0;
                if (temp == frame1)
                {
                    temp = frame2;
                }
                else
                {
                    temp = frame1;
                }
            }
        }
        public Rectangle GetFrame()
        {
            return temp;
        }
        public void Update(GameTime gameTime)
        {
            DefaultAnimation();
        }

    }
}
