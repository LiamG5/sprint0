using Microsoft.Xna.Framework;
using sprint0.Classes;

namespace sprint0.PlayerStates
{
    public class IdleState : IPlayerState
    {
        private Link player;
        private LinkAnimation linkAnimation;

        public IdleState(Link player, LinkAnimation linkAnimation) 
        {
            this.player = player;
            this.linkAnimation = linkAnimation;
        }

        public void Enter() { }

        public void Update(GameTime gameTime) { }

        public void UseState() 
        {
            switch (player.direction)
            {
                case Link.Direction.Up:
                    linkAnimation.LinkStandingUp();
                    break;
                case Link.Direction.Down:
                    linkAnimation.LinkStandingDown();
                    break;
                case Link.Direction.Left:
                    linkAnimation.LinkStandingLeft();
                    break;
                case Link.Direction.Right:
                    linkAnimation.LinkStandingRight();
                    break;
            }
        }

        public void Exit() { }
    }
}
