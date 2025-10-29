using sprint0.Interfaces;

namespace sprint0.Commands
{
    public sealed class GoToRoom4Command : ICommand
    {
        private readonly Game1 game;
        public GoToRoom4Command(Game1 game) { this.game = game; }
        public void Execute() => game.GoToRoom4();
    }
}