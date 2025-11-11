using sprint0.Interfaces;

namespace sprint0.Commands
{
    public class OpenInventoryCommand : ICommand
    {
        private readonly Game1 game;
        
        public OpenInventoryCommand(Game1 game)
        {
            this.game = game;
        }
        
        public void Execute()
        {
            game.currentState = Game1.GameState.Inventory;
        }
    }
}

