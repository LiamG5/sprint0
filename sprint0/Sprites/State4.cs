using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using sprint0.Interfaces;
using sprint0;
using System;

namespace sprint0.Classes
{
    public class State4 : ISprite
    {
        public Texture2D[] texture;
        public Vector2 position;
        public Texture2D textureC; 
        private int count = 0; 
    public State4(Texture2D texture1, Texture2D texture2, Texture2D texture3, Texture2D texture4, Vector2 position)
        {
            texture = new Texture2D[4] {texture1 ,texture2, texture3, texture4};
            this.position = new Vector2(100, 300);
            textureC = texture1;

        }

        public void Update(GameTime gameTime)
        {
            position = new Vector2(position.X + 10, position.Y);
            textureC = texture[count % 4];
            count++;

            if(position.X >= 800){
                position = new Vector2(0, 300);
            }
        
        }

        public void Draw(SpriteBatch spriteBatch)
        {
        spriteBatch.Draw(textureC, position, Color.White);
        }
    }
}