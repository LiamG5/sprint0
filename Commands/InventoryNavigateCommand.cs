using Microsoft.Xna.Framework.Input;
using sprint0.Interfaces;

namespace sprint0.Commands
{
    public class InventoryNavigateCommand : ICommand
    {
        public enum Direction { Up, Down, Left, Right }
        
        private readonly Game1 game;
        private readonly Direction direction;
        
        public InventoryNavigateCommand(Game1 game, Direction direction)
        {
            this.game = game;
            this.direction = direction;
        }
        
        public void Execute()
        {
            game.NavigateInventory(direction);
        }
    }
}

