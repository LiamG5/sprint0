using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using sprint0.Classes;
using sprint0.Interfaces;
using System.Collections;
using System.Security.Cryptography.X509Certificates;

namespace sprint0.PlayerStates
{
    public class ItemState : IPlayerState
    {
        private Link player;
        private LinkAnimation linkAnimation;
        private SoundEffectInstance sfx = sprint0.Sounds.SoundStorage.LOZ_Get_Item.CreateInstance();

        private float currentTime = 0;
        private float duration = 750;
        private int itemNum = 0;

        public ItemState(Link player, LinkAnimation linkAnimation, int itemNum)
        {
            this.player = player;
            this.linkAnimation = linkAnimation;
            this.itemNum = itemNum;
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

<<<<<<< HEAD

=======
>>>>>>> 446bfa037c225b1b7a0341d109af49cc2cb96b6e
            if (itemNum != 2 && sfx.State != SoundState.Playing)
            {
                sfx.Play();
            }
        }

        public void UseState()
        {
            switch(itemNum)
            {
                case 1: linkAnimation.LinkUseItem1(duration, currentTime); break;
                case 2: linkAnimation.LinkUseItem2(duration, currentTime); break;
                case 3: linkAnimation.LinkUseItem3(duration, currentTime); break;
            }
        }

        public void Exit() { }
    }
}
