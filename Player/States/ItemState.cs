using System.Collections;
using System.Security.Cryptography.X509Certificates;
using Microsoft.Xna.Framework;
using sprint0.Classes;
using sprint0.Interfaces;

namespace sprint0.PlayerStates
{
    public class ItemState : IPlayerState
    {
        private Link player;
        private LinkAnimation linkAnimation;

        private float currentTime = 0;
        private float duration = 750;
        private int itemNum = 0;

        public ItemState(Link player, LinkAnimation linkAnimation, int itemNum)
        {
            this.player = player;
            this.linkAnimation = linkAnimation;
            this.itemNum = itemNum;
        }

        public void Enter() { 
            player.velocity = new Vector2(0, 0);
        }

        public void Update(GameTime gameTime)
        {
            currentTime += (float)gameTime.ElapsedGameTime.TotalMilliseconds;

            if (currentTime > duration)
            {
                player.Idle();
            }
        }

        public void UseState()
        {
            switch(itemNum)
            {
                case 1: linkAnimation.LinkUseItem1(duration, currentTime); break;
                case 2: linkAnimation.LinkUseItem2(duration, currentTime); break;
                case 3: linkAnimation.LinkUseItem3(duration, currentTime); break;
            }
        }

        public void Exit() { }
    }
}
