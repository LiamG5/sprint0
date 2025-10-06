using Microsoft.Xna.Framework;
using sprint0.Classes;

namespace sprint0.PlayerStates
{
    public class IdleState : IPlayerState
    {

        private Direction direction;
        private LinkAnimation linkAnimation;

        public IdleState(Direction direction, LinkAnimation linkAnimation) 
        {
            this.direction = direction;
            this.linkAnimation = linkAnimation;
        }

        public void Enter() {
            if (direction == Direction.Up)
            {
                linkAnimation.LinkStandingUp();
            }
            else if (direction == Direction.Down)
            {
                linkAnimation.LinkStandingDown();
            }
            else if (direction == Direction.Left)
            {
                linkAnimation.LinkStandingLeft();
            }
            else if (direction == Direction.Right)
            {
                linkAnimation.LinkStandingRight();
            }
        }
        public void UseState() { }
        public void Exit() { }
        public void Update(GameTime gameTime) { }
    }

}
