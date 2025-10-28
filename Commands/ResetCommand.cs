using sprint0.Interfaces;

namespace sprint0.Commands
{
    public sealed class ResetCommand : ICommand
    {
        private readonly Game1 game;
        public ResetCommand(Game1 game) { this.game = game; }
        public void Execute() => game.ResetGame();
    }
}
