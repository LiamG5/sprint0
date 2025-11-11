using sprint0.Interfaces;

namespace sprint0.Commands
{
    public class WinCommand : ICommand
    {
        private Game1 game;

        public WinCommand(Game1 game)
        {
            this.game = game;
        }

        public void Execute()
        {
            game.currentState = Game1.GameState.Win;
        }
    }
}

