using Microsoft.Xna.Framework;
using sprint0;
using sprint0.Interfaces;
using sprint0.Classes;


namespace sprint0.Commands
{
    public sealed class MoveLeftCommand : ICommand
    {
        private readonly Link player;
        public MoveLeftCommand(Link player) { this.player = player;}
        public void Execute() => player.MoveLeft();
    }
}
