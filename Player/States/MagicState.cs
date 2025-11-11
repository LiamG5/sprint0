using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using sprint0.Classes;
using sprint0.Interfaces;

namespace sprint0.PlayerStates
{
	public class MagicState : IPlayerState
	{
		private Link player;
		private LinkAnimation linkAnimation;
        private SoundEffectInstance sfx = sprint0.Sounds.SoundStorage.LOZ_MagicalRod.CreateInstance();

        private float currentTime = 0;
		private float duration = 750;

		public MagicState(Link player, LinkAnimation linkAnimation)
		{
			this.player = player;
			this.linkAnimation = linkAnimation;
		}

		public void Enter() { 
			player.velocity = new Vector2(0, 0);
		}

		public void Update(GameTime gameTime)
		{
			currentTime += (float)gameTime.ElapsedGameTime.TotalMilliseconds;

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
					linkAnimation.LinkMagicUp(duration, currentTime);
					break;
				case Link.Direction.Down:
					linkAnimation.LinkMagicDown(duration, currentTime);
					break;
				case Link.Direction.Left:
					linkAnimation.LinkMagicLeft(duration, currentTime);
					break;
				case Link.Direction.Right:
					linkAnimation.LinkMagicRight(duration, currentTime);
					break;
			}
		}

		public void Exit() { }
	}
}
