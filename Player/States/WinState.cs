using Microsoft.Xna.Framework;
using sprint0.Classes;
using sprint0.Interfaces;

namespace sprint0.PlayerStates
{
    public class WinState : IPlayerState
    {
        private Link player;
        private LinkAnimation linkAnimation;

        public WinState(Link player, LinkAnimation linkAnimation)
        {
            this.player = player;
            this.linkAnimation = linkAnimation;
        }

        public void Enter()
        {
            player.velocity = new Vector2(0, 0);
        }

        public void Update(GameTime gameTime)
        {
        }

        public void UseState()
        {
            linkAnimation.LinkHoldingItem();
        }

        public void Exit() { }
    }
}

