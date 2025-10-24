using sprint0.Interfaces;

namespace sprint0.Commands
{
    public sealed class GoToRoom10Command : ICommand
    {
        private readonly Game1 game;
        public GoToRoom10Command(Game1 game) { this.game = game; }
        public void Execute() => game.GoToRoom10();
    }
}