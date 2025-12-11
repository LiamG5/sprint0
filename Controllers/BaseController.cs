using System;
using sprint0.Interfaces;
using sprint0.Commands;

namespace sprint0.Classes
{
    public abstract class BaseController : IController
    {
        protected Game1 game;
        protected Func<bool> isInventoryOpen;

        protected BaseController(Game1 game, Func<bool> isInventoryOpen = null)
        {
            this.game = game;
            this.isInventoryOpen = isInventoryOpen ?? (() => false);
        }

        public virtual void Update()
        {
            bool inventoryOpen = isInventoryOpen();

            if (game.currentState == Game1.GameState.GameOver || game.currentState == Game1.GameState.Win)
            {
                HandleGameOverOrWinState();
                return;
            }

            if (inventoryOpen)
            {
                HandleInventoryNavigation();
                return;
            }

            HandleGameplayInput();
        }

        protected virtual void HandleGameOverOrWinState()
        {
            // Override in derived classes if needed
        }

        protected virtual void HandleInventoryNavigation()
        {
            // Override in derived classes to handle inventory navigation
        }

        protected virtual void HandleGameplayInput()
        {
            // Override in derived classes to handle gameplay input
        }

        protected void NavigateInventory(InventoryNavigateCommand.Direction direction)
        {
            game.NavigateInventory(direction);
        }

        protected void SelectInventoryItem()
        {
            game.SelectInventoryItem();
        }

        protected void CloseInventory()
        {
            game.currentState = Game1.GameState.Gameplay;
        }
    }
}

