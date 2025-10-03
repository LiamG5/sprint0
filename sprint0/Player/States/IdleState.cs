using Microsoft.Xna.Framework;
using sprint0.Classes;

namespace sprint0.PlayerStates
{
    public class IdleState : IPlayerState
    {

        public void Enter(Direction direction, LinkAnimation linkAnimation) {
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
        public void UseState(Direction direction, LinkSprite linksprite) { }
        public void Exit() { }
        public void Update(GameTime gameTime) { }
    }

}
