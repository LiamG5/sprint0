using sprint0;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using sprint0.Classes;
using sprint0.Commands;
using sprint0.Interfaces;
using sprint0.PlayerStates;

namespace sprint0.Classes
{
    public class KeyboardController : BaseController
    {
        private KeyboardState previousState;
        private Dictionary<Keys, ICommand> pressOnceCommands;
        private Dictionary<Keys, ICommand> holdCommands;

        public KeyboardController(Game1 game, ISprite linkSprite, Func<bool> isInventoryOpen = null)
            : base(game, isInventoryOpen)
        {
            previousState = Keyboard.GetState();
            InitializeKeyMappings();
        }

        private void InitializeKeyMappings()
        {
            pressOnceCommands = new Dictionary<Keys, ICommand>();
            holdCommands = new Dictionary<Keys, ICommand>();
            
            pressOnceCommands[Keys.B] = new OpenInventoryCommand(game);
            
            pressOnceCommands[Keys.D1] = new AttackCommand(game.link);
            pressOnceCommands[Keys.D2] = new UseItemCommand(game.link, 2);
            
            pressOnceCommands[Keys.Z] = new AttackCommand(game.link);
            pressOnceCommands[Keys.N] = new AttackCommand(game.link);
            pressOnceCommands[Keys.E] = new DamageCommand(game.link, 1);
            pressOnceCommands[Keys.T] = new PrevCommand(game.blockCarousel);
            pressOnceCommands[Keys.Y] = new NextCommand(game.blockCarousel);
            pressOnceCommands[Keys.O] = new PrevCommand(game.enemyCarousel);
            pressOnceCommands[Keys.P] = new NextCommand(game.enemyCarousel);
            pressOnceCommands[Keys.U] = new PrevCommand(game.itemCarousel);
            pressOnceCommands[Keys.I] = new NextCommand(game.itemCarousel);
            pressOnceCommands[Keys.Q] = new QuitCommand(game);
            pressOnceCommands[Keys.R] = new ResetCommand(game);
            pressOnceCommands[Keys.Escape] = new PauseCommand(game);
            
            holdCommands[Keys.Left] = new MoveLeftCommand(game.link);
            holdCommands[Keys.A] = new MoveLeftCommand(game.link);
            holdCommands[Keys.Right] = new MoveRightCommand(game.link);
            holdCommands[Keys.D] = new MoveRightCommand(game.link);
            holdCommands[Keys.Up] = new MoveUpCommand(game.link);
            holdCommands[Keys.W] = new MoveUpCommand(game.link);
            holdCommands[Keys.Down] = new MoveDownCommand(game.link);
            holdCommands[Keys.S] = new MoveDownCommand(game.link);
        }

        public override void Update()
        {
            KeyboardState currentState = Keyboard.GetState();
            
            base.Update();
            
            previousState = currentState;
        }

        protected override void HandleGameOverOrWinState()
        {
            KeyboardState currentState = Keyboard.GetState();
            if (currentState.IsKeyDown(Keys.R) && previousState.IsKeyUp(Keys.R))
            {
                new ResetCommand(game).Execute();
            }
        }

        protected override void HandleInventoryNavigation()
        {
            KeyboardState currentState = Keyboard.GetState();
            
            if (currentState.IsKeyDown(Keys.Up) && previousState.IsKeyUp(Keys.Up))
            {
                NavigateInventory(InventoryNavigateCommand.Direction.Up);
            }
            if (currentState.IsKeyDown(Keys.Down) && previousState.IsKeyUp(Keys.Down))
            {
                NavigateInventory(InventoryNavigateCommand.Direction.Down);
            }
            if (currentState.IsKeyDown(Keys.Left) && previousState.IsKeyUp(Keys.Left))
            {
                NavigateInventory(InventoryNavigateCommand.Direction.Left);
            }
            if (currentState.IsKeyDown(Keys.Right) && previousState.IsKeyUp(Keys.Right))
            {
                NavigateInventory(InventoryNavigateCommand.Direction.Right);
            }
            if (currentState.IsKeyDown(Keys.Enter) && previousState.IsKeyUp(Keys.Enter))
            {
                SelectInventoryItem();
            }
            
            if (currentState.IsKeyDown(Keys.B) && previousState.IsKeyUp(Keys.B))
            {
                CloseInventory();
            }
        }

        protected override void HandleGameplayInput()
        {
            KeyboardState currentState = Keyboard.GetState();
            
            foreach (var kvp in pressOnceCommands)
            {
                Keys key = kvp.Key;
                ICommand command = kvp.Value;
                
                if (currentState.IsKeyDown(key) && previousState.IsKeyUp(key))
                {
                    command.Execute();
                    
                    if (key == Keys.T || key == Keys.Y)
                    {
                        game.tile = game.blockCarousel.GetCurrentBlock();
                    }
                }
            }

            bool anyMovementKeyPressed = false;

            foreach (var kvp in holdCommands)
            {
                Keys key = kvp.Key;
                ICommand command = kvp.Value;
                
                if (currentState.IsKeyDown(key))
                {
                    command.Execute();
                    anyMovementKeyPressed = true;
                }
            }
            game.KeyboardMovementActive = anyMovementKeyPressed;

            if (!anyMovementKeyPressed)
            {
                if (game.link.GetCurrentState() is IdleState || game.link.GetCurrentState() is MoveState)
                {
                    new IdleCommand(game.link).Execute();
                }
            }
        }
    }

}
