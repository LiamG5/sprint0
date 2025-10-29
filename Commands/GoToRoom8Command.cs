using sprint0.Interfaces;

namespace sprint0.Commands
{
    public sealed class GoToRoom8Command : ICommand
    {
        private readonly Game1 game;
        public GoToRoom8Command(Game1 game) { this.game = game; }
        public void Execute() => game.GoToRoom8();
    }
}