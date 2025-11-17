using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using sprint0.Classes;
using sprint0.Interfaces;

namespace sprint0.PlayerStates
{
    public class KnockbackState : IPlayerState
    {
        private Link player;
        private LinkAnimation linkAnimation;
        private SoundEffectInstance sfx = sprint0.Sounds.SoundStorage.LOZ_Link_Hurt.CreateInstance();

        private float currentTime;
        private float knockbackDuration = 250;

        public KnockbackState(Link player, LinkAnimation linkAnimation)
        {
            this.player = player;
            this.linkAnimation = linkAnimation;
        }

        public void Enter()
        {
            if (sfx.State != SoundState.Playing)
            {
                sfx.Play();
            }
        }

        public void Update(GameTime gameTime)
        {
            currentTime += (float)gameTime.ElapsedGameTime.Milliseconds;

            if (currentTime > knockbackDuration)
            {
                player.ChangeState(new DamagedState(player, linkAnimation));
            }
        }

        public void UseState()
        {
            linkAnimation.ChangeColor(Color.Red);
        }

        public void Exit() { 
            player.velocity = new Vector2(0, 0);
            linkAnimation.ChangeColor(Color.White);
        }
    }
}

