using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using sprint0.Classes;
using sprint0.Interfaces;

namespace sprint0.PlayerStates
{
    public class DamagedState : IPlayerState
    {

        private Link player;
        private LinkAnimation linkAnimation;
        private SoundEffectInstance sfx = sprint0.Sounds.SoundStorage.LOZ_Link_Hurt.CreateInstance();

        private float currentTime;
        private float duration = 1500;

        public DamagedState(Link player, LinkAnimation linkAnimation)
        {
            this.player = player;
            this.linkAnimation = linkAnimation;
        }

        public void Enter() { 
            player.velocity = new Vector2(0, 0);
        }

        public void Update(GameTime gameTime)
        {
            currentTime += (float)gameTime.ElapsedGameTime.Milliseconds;

            if (currentTime > duration)
            {
                player.Idle();
            }

            if (sfx.State != SoundState.Playing)
            {
                sfx.Play();
            }

        }

        public void UseState()
        {
            switch (player.direction)
            {
                case Link.Direction.Up:
                    linkAnimation.LinkDamagedUp(duration, currentTime);
                    break;
                case Link.Direction.Down:
                    linkAnimation.LinkDamagedDown(duration, currentTime);
                    break;
                case Link.Direction.Left:
                    linkAnimation.LinkDamagedLeft(duration, currentTime);
                    break;
                case Link.Direction.Right:
                    linkAnimation.LinkDamagedRight(duration, currentTime);
                    break;
            }
        }

        public void Exit() { }
    }

}
