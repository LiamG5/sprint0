using Microsoft.Xna.Framework;
using sprint0.Classes;

namespace sprint0.PlayerStates
{
    public class ItemState : IPlayerState
    {
        private Link player;
        private LinkAnimation linkAnimation;

        private float currentTime = 0;
        private float duration = 750;

        public ItemState(Link player, LinkAnimation linkAnimation)
        {
            this.player = player;
            this.linkAnimation = linkAnimation;
        }

        public void Enter() { }

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
            linkAnimation.LinkUseItem1(duration, currentTime);
        }

        public void Exit() { }
    }
}
