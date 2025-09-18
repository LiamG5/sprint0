using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using sprint0.Interfaces;
using sprint0.Classes;
using sprint0;
using System;

namespace sprint0.Classes
{
    public class MarioSprite : ISprite
    {
    public Texture2D[] textures; 
    public Vector2 position;
    public int state;
    public Texture2D textureC; 
    private int fcount;
    private int acount;
    public State1 state1;
    public State2 state2;
    public State3 state3;
    public State4 state4;


        public MarioSprite(Texture2D texture1, Texture2D texture2, Texture2D texture3, Texture2D texture4, Vector2 position)
        {
            textures = new Texture2D[4] { texture1, texture2, texture3, texture4};
            this.position = position;
            this.state = 1;
            state1 = new State1(texture1);
            state2 = new State2(texture2, texture3);
            state3 = new State3 (texture1, position);
            state4 = new State4 (texture1, texture2, texture3, texture4, position);
            textureC = texture1;
        }

        public void Update(GameTime gameTime)
        {
            
            fcount++;
            if(fcount % 10 == 0){
                acount++;
            
                if(state == 2){
                state2.Update(gameTime);
            }
                if(state == 3){
                state3.Update(gameTime);
            }
                 if(state == 4){
                state4.Update(gameTime);
            }

            }
            

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if(state == 1){
                state1.Draw(spriteBatch);
            }
            if(state == 2){
                state2.Draw(spriteBatch);
            }
            if(state == 3){
                state3.Draw(spriteBatch);
            }
            if(state == 4){
                state4.Draw(spriteBatch);
            }

        }
    }
}