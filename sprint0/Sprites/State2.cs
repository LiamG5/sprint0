using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using sprint0.Interfaces;
using sprint0;
using System;

namespace sprint0.Classes
{
    public class State2 : ISprite
    {
        
        public Texture2D[] textures;
        public Texture2D textureC;
        private int count = 0;
    public State2(Texture2D texture1, Texture2D texture2)
        {
            textures = new Texture2D[2] {texture1 ,texture2};
            textureC = texture1;
        }

        public void Update(GameTime gameTime)
        {
        textureC = textures[count % 2];
        count++;
        
        }

        public void Draw(SpriteBatch spriteBatch)
        {
        spriteBatch.Draw(textureC, new Vector2(100, 300), Color.White);
        }
    }
}