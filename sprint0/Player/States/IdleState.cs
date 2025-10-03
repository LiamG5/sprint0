using Microsoft.Xna.Framework;

namespace sprint0.PlayerStates
{
    public class IdleState : IPlayerState
    {

        public void Enter(Direction direction, LinkSprite linksprite) {
            if (direction == Direction.Up)
            {
                linkSprite.IdleUp();
            }
            else if (direction == Direction.Down)
            {
                linkSprite.IdleDown();
            }
            else if (direction == Direction.Left)
            {
                linkSprite.IdleLeft();
            }
            else if (direction == Direction.Right)
            {
                linkSprite.IdleRight();
            }
        }
        public void UseState(Direction direction, LinkSprite linksprite) { }
        public void Exit() { }
        public void Update(GameTime gameTime) { }
    }

}
