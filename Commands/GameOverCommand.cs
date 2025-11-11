using sprint0.Interfaces;

namespace sprint0.Commands
{
    public class GameOverCommand : ICommand
    {
        private Game1 game;

        public GameOverCommand(Game1 game)
        {
            this.game = game;
        }

        public void Execute()
        {
            game.currentState = Game1.GameState.GameOver;
        }
    }
}

