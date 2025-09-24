using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using sprint0.Interfaces;

using sprint0;

namespace sprint0.Classes
{
    public class StringSprite : ISprite
    {
        public Texture2D texture;
        public Vector2 position;
        public string text;


        public StringSprite(Texture2D texture, Vector2 position)
        {
            this.texture = texture;
            this.position = position;
        }

        public void Update(GameTime gameTime)
        {
            
        }

        public void Draw(SpriteBatch spriteBatch)
        {

        }
    }
}