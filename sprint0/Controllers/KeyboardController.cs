using sprint0.Interfaces;
using sprint0;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using sprint0.Classes;
using sprint0.Sprites;
using sprint0.Commands;
using sprint0.PlayerStates;


namespace sprint0.Classes
{

    public class KeyboardController : IController
    {
        private KeyboardState previousState;
        private Game1 game;
        private Dictionary<Keys, ICommand> pressOnceCommands;
        private Dictionary<Keys, ICommand> holdCommands;
        

        public KeyboardController(Game1 game, ISprite linkSprite)
        {
            this.game = game;
            InitializeKeyMappings();
        }

        private void InitializeKeyMappings()
        {
            pressOnceCommands = new Dictionary<Keys, ICommand>();
            holdCommands = new Dictionary<Keys, ICommand>();
            
            pressOnceCommands[Keys.Z] = new AttackCommand(game.link);
            pressOnceCommands[Keys.N] = new AttackCommand(game.link);
            pressOnceCommands[Keys.D1] = new UseItemCommand(game.link, 1);
            pressOnceCommands[Keys.D2] = new UseItemCommand(game.link, 2);
            pressOnceCommands[Keys.D3] = new UseItemCommand(game.link, 3);
            pressOnceCommands[Keys.E] = new DamageCommand(game.link);
            pressOnceCommands[Keys.T] = new PrevCommand(game.blockCarousel);
            pressOnceCommands[Keys.Y] = new NextCommand(game.blockCarousel);
            pressOnceCommands[Keys.O] = new PrevCommand(game.enemyCarousel);
            pressOnceCommands[Keys.P] = new NextCommand(game.enemyCarousel);
            pressOnceCommands[Keys.U] = new PrevCommand(game.itemCarousel);
            pressOnceCommands[Keys.I] = new NextCommand(game.itemCarousel);
            pressOnceCommands[Keys.Q] = new QuitCommand(game);
            pressOnceCommands[Keys.R] = new ResetCommand(game);
            
            holdCommands[Keys.Left] = new MoveLeftCommand(game.link);
            holdCommands[Keys.A] = new MoveLeftCommand(game.link);
            holdCommands[Keys.Right] = new MoveRightCommand(game.link);
            holdCommands[Keys.D] = new MoveRightCommand(game.link);
            holdCommands[Keys.Up] = new MoveUpCommand(game.link);
            holdCommands[Keys.W] = new MoveUpCommand(game.link);
            holdCommands[Keys.Down] = new MoveDownCommand(game.link);
            holdCommands[Keys.S] = new MoveDownCommand(game.link);
        }

        public void Update()
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
                    else if (key == Keys.O || key == Keys.P)
                    {
                        game.enemy = game.enemyCarousel.GetCurrentEnemy();
                    }else if(key == Keys.U || key == Keys.I)
                    {
                        game.item = game.itemCarousel.GetCurrentItem();
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

            if (!anyMovementKeyPressed)
            {
                if (game.link.GetCurrentState() is IdleState || game.link.GetCurrentState() is MoveState)
                {
                    new IdleCommand(game.link).Execute();
                }
            }

            previousState = currentState;
        }
    }

}
