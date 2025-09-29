using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using sprint0.Interfaces;
using sprint0;
using System;

namespace sprint0.Classes
{
    public class State1 : ISprite
    {
        Texture2D texture;
    public State1(Texture2D texture1)
        {
            texture = texture1;
        }

        public void Update(GameTime gameTime)
        {
        
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, new Vector2(100, 300), Color.White);
        }
    }
}