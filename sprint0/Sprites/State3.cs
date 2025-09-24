using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using sprint0.Interfaces;
using sprint0;
using System;

namespace sprint0.Classes
{
    public class State3 : ISprite
    {
        public Texture2D texture;
        public Vector2 position;
        private bool up;
    public State3(Texture2D texture1, Vector2 position)
        {
            texture = texture1;
            this.position = new Vector2(100, 300);
            up = true;
        }

        public void Update(GameTime gameTime)
        {
            if(up){
                position = new Vector2(position.X, position.Y + 10);
            }else{
                position = new Vector2(position.X, position.Y - 10);
            }
            if(position.Y <= 0){
                up = true;
            }
            if(position.Y >= 450){
                up = false;
            }
        
        }

        public void Draw(SpriteBatch spriteBatch)
        {
        spriteBatch.Draw(texture, position, Color.White);
        }
    }
}