using Microsoft.Xna.Framework;
using sprint0.Interfaces;

namespace sprint0.Commands
{
    public sealed class QuitCommand : ICommand
    {
        private readonly Game game;
        public QuitCommand(Game game) { this.game = game; }
        public void Execute() => game.Exit();
    }
}
