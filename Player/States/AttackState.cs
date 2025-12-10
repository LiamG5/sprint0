using System.Runtime.Serialization.Formatters;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using sprint0.Classes;
using sprint0.Interfaces;

namespace sprint0.PlayerStates
{
    public class AttackState : IPlayerState
    {
        private Link player;
        private LinkAnimation linkAnimation;
        private SoundEffectInstance sfx = sprint0.Sounds.SoundStorage.LOZ_Sword_Slash.CreateInstance();

        private float currentTime = 0;
        private float duration = 500; // ms
        private LinkAttackHitbox linkAttackHitbox;

        public AttackState(Link player, LinkAnimation linkAnimation, LinkAttackHitbox linkAttackHitbox)
        {
            this.player = player;
            this.linkAttackHitbox = linkAttackHitbox;
            this.linkAnimation = linkAnimation;
        }

        public void Enter()
        {
            player.velocity = new Vector2(0, 0);
            currentTime = 0f;
            linkAttackHitbox.active = false;
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
            float startActive = duration * 0.4f;
            float endActive   = duration * 0.7f;

            if (currentTime >= startActive && currentTime <= endActive)
            {
                linkAttackHitbox.active = true;
            }
            else
            {
                linkAttackHitbox.active = false;
            }


            switch (player.direction)
            {
                case Link.Direction.Up:
                    linkAnimation.LinkAttackingUp(duration, currentTime);
                    linkAttackHitbox.AttackUp(player.position);
                    break;

                case Link.Direction.Down:
                    linkAnimation.LinkAttackingDown(duration, currentTime);
                    linkAttackHitbox.AttackDown(player.position);
                    break;

                case Link.Direction.Left:
                    linkAnimation.LinkAttackingLeft(duration, currentTime);
                    linkAttackHitbox.AttackLeft(player.position);
                    break;

                case Link.Direction.Right:
                    linkAnimation.LinkAttackingRight(duration, currentTime);
                    linkAttackHitbox.AttackRight(player.position);
                    break;
            }
        }

        public void Exit()
        {
            linkAttackHitbox.active = false;

            if (Inventory.GetHealth() == Inventory.GetMaxHealth())
            {
                player.FireSwordBeam();
            }
        }
    }
}

