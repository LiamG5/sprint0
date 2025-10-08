using Microsoft.Xna.Framework;
using sprint0.Classes;
using sprint0.Interfaces;

namespace sprint0.PlayerStates
{
    public class MoveState : IPlayerState
    {
        private Link player;
        private LinkAnimation linkAnimation;

        private float currentTime = 0;
        private float duration = 1000;

        public MoveState(Link player, LinkAnimation linkAnimation)
        {
            this.player = player;
            this.linkAnimation = linkAnimation;
        }

        public void Enter() { }

        public void Update(GameTime gameTime)
        {
            currentTime += (float)gameTime.ElapsedGameTime.Milliseconds;

            if (currentTime > duration)
            {
                currentTime -= duration;
            }
        }

        public void UseState()
        {
            switch (player.direction)
            {
                case Link.Direction.Up:
                    linkAnimation.LinkWalkingUp(duration, currentTime);
                    break;
                case Link.Direction.Down:
                    linkAnimation.LinkWalkingDown(duration, currentTime);
                    break;
                case Link.Direction.Left:
                    linkAnimation.LinkWalkingLeft(duration, currentTime);
                    break;
                case Link.Direction.Right:
                    linkAnimation.LinkWalkingRight(duration, currentTime);
                    break;
            }
        }

        public void Exit() { }
    }
}