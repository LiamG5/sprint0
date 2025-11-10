using sprint0.Interfaces;

namespace sprint0.Commands
{
    public class InventorySelectCommand : ICommand
    {
        private readonly Game1 game;
        
        public InventorySelectCommand(Game1 game)
        {
            this.game = game;
        }
        
        public void Execute()
        {
            game.SelectInventoryItem();
        }
    }
}

