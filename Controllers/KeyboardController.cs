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
        
        private SpriteMain linkSprite; //replace spirtemain with linksprite
        private Game1 game;
        

        public KeyboardController(Game1 game, ISprite linkSprite)
        {
            this.game = game;
            
        }

        public void Update()
        {
            KeyboardState currentState = Keyboard.GetState();

    
        
            //call exacute to 
            if (currentState.IsKeyDown(Keys.D0) && previousState.IsKeyUp(Keys.D0))
            {
                game.Exit();
            }
            if (currentState.IsKeyDown(Keys.D1) && previousState.IsKeyUp(Keys.D1))
            {
               

            }
            if (currentState.IsKeyDown(Keys.D2) && previousState.IsKeyUp(Keys.D2))
            {
                
                
            }
            if (currentState.IsKeyDown(Keys.D3) && previousState.IsKeyUp(Keys.D3))
            {
                
                
            }
            if (currentState.IsKeyDown(Keys.D4) && previousState.IsKeyUp(Keys.D4))
            {
                
            }

            previousState = currentState;
        }
    }

}
