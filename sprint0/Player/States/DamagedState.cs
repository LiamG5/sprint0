using Microsoft.Xna.Framework;

namespace sprint0.PlayerStates
{
    public class DamagedState : IPlayerState
    {
        private float timer = 1500;
        private Link player;
        private Direction direction;
        private LinkAnimation linkAnimation;

        public DamagedState(Link player, Direction direction, LinkAnimation linkAnimation)
        {
            this.player = player;
            this.direction = direction;
            this.linkAnimation = linkAnimation;
        }

        public void Enter() { }
        public void UseState() { }
        public void Exit() { }
        public void Update(GameTime gameTime)
        {
            timer -= gameTime.ElapsedGameTime.Milliseconds;

            if (timer <= 0)
            {
                player.ChangeColor(Color.White);
                player.ChangeState(new IdleState(direction, linkAnimation));
            }
            else if (timer <= 500)
            {
                player.ChangeColor(Color.Red);
            }
            else if (timer <= 1000)
            {
                player.ChangeColor(Color.White);
            }
            else if (timer <= 1500)
            {
                player.ChangeColor(Color.Red);
            }
        }
    }

}
