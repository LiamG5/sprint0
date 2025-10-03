using Microsoft.Xna.Framework;

namespace sprint0.PlayerStates
{
    public class IdleState : IPlayerState
    {
        private float timer = 1000;
        public void Enter(Direction direction, LinkSprite linksprite)
        {

            //No animation for damaged, just change color off charater
            if (direction == Direction.Up)
            {
                linkSprite.DamagedUp();
            }
            else if (direction == Direction.Down)
            {
                linkSprite.DamagedDown();
            }
            else if (direction == Direction.Left)
            {
                linkSprite.DamagedLeft();
            }
            else if (direction == Direction.Right)
            {
                linkSprite.DamagedRight();
            }
        }
        public void UseState(Direction direction, LinkSprite linksprite) { }
        public void Exit() { }
        public void Update(GameTime gameTime, Link player) 
        { 
            timer -= gameTime.ElapsedGameTime.Milliseconds;

            if (timer <= 0)
            {
                player.ChangeState(new IdleState());
            }
        }
    }

}
