using sprint0.Interfaces;
using sprint0;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;


namespace sprint0.Classes
{

    public class KeyboardController : IController
    {
        private KeyboardState previousState;
        private MarioSprite marioSprite; 
        private Game1 game;
        

        public KeyboardController(Game1 game, MarioSprite marioSprite)
        {
            this.game = game;
            this.marioSprite = marioSprite;
        }

        public void Update()
        {
            KeyboardState currentState = Keyboard.GetState();

    
        
            //I dont have a numpad used keys instead
            if (currentState.IsKeyDown(Keys.D0) && previousState.IsKeyUp(Keys.D0))
            {
                game.Exit();
            }
            if (currentState.IsKeyDown(Keys.D1) && previousState.IsKeyUp(Keys.D1))
            {
                marioSprite.state = 1;

            }
            if (currentState.IsKeyDown(Keys.D2) && previousState.IsKeyUp(Keys.D2))
            {
                
                marioSprite.state = 2;
            }
            if (currentState.IsKeyDown(Keys.D3) && previousState.IsKeyUp(Keys.D3))
            {
                
                marioSprite.state = 3;
            }
            if (currentState.IsKeyDown(Keys.D4) && previousState.IsKeyUp(Keys.D4))
            {
                marioSprite.state = 4;
            }

            previousState = currentState;
        }
    }

}
