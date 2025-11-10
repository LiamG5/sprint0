using sprint0.Interfaces;

namespace sprint0.Commands
{
    public class PauseCommand : ICommand
    {
        private Game1 game;

        public PauseCommand(Game1 game)
        {
            this.game = game;
        }

        public void Execute()
        {
            if (game.currentState == Game1.GameState.Gameplay)
            {
                game.currentState = Game1.GameState.Pause;
            }
            else
            {
                game.currentState = Game1.GameState.Gameplay;
            }
        }
    }
}

