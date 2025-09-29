using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using sprint0.Interfaces;
using sprint0;
using System;

namespace sprint0.Classes
{
    public class State4 : ISprite, ILinkState
    {
        public Texture2D[] texture;
        public Vector2 position;
        public Texture2D textureC; 
        private int count = 0;
        private Link Link;

        public State4(Texture2D texture1, Texture2D texture2, Texture2D texture3, Texture2D texture4, Vector2 position)
        {
            texture = new Texture2D[4] {texture1 ,texture2, texture3, texture4};
            this.position = new Vector2(100, 300);
            textureC = texture1;
        }

        public State4(Link Link, Texture2D texture1, Vector2 position)
        {
            this.Link = Link;
            texture = new Texture2D[4] {texture1, texture1, texture1, texture1};
            this.position = position;
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

        public void Update()
        {
            position = new Vector2(position.X + 10, position.Y);
            textureC = texture[count % 4];
            count++;

            if(position.X >= 800){
                position = new Vector2(0, 300);
            }
        }

        public void MoveLeft()
        {
            if (Link != null)
            {
                Link.ChangeState(new State4(Link, textureC, position));
            }
        }

        public void MoveRight()
        {
            if (Link != null)
            {
                Link.ChangeState(new State4(Link, textureC, position));
            }
        }

        public void MoveUp()
        {
            if (Link != null)
            {
                Link.ChangeState(new State4(Link, textureC, position));
            }
        }

        public void MoveDown()
        {
            if (Link != null)
            {
                Link.ChangeState(new State4(Link, textureC, position));
            }
        }

        public void AttackLeft()
        {
            if (Link != null)
            {
                Link.SetDirection(Direction.Left);
                Link.Attack();
                Link.ChangeState(new State2(Link, textureC, position));
            }
        }

        public void AttackRight()
        {
            if (Link != null)
            {
                Link.SetDirection(Direction.Right);
                Link.Attack();
                Link.ChangeState(new State2(Link, textureC, position));
            }
        }

        public void AttackUp()
        {
            if (Link != null)
            {
                Link.SetDirection(Direction.Up);
                Link.Attack();
                Link.ChangeState(new State2(Link, textureC, position));
            }
        }

        public void AttackDown()
        {
            if (Link != null)
            {
                Link.SetDirection(Direction.Down);
                Link.Attack();
                Link.ChangeState(new State2(Link, textureC, position));
            }
        }

        public void UseItem1Left()
        {
            if (Link != null)
            {
                Link.SetDirection(Direction.Left);
                Link.UseItem1();
                Link.ChangeState(new State2(Link, textureC, position));
            }
        }

        public void UseItem1Right()
        {
            if (Link != null)
            {
                Link.SetDirection(Direction.Right);
                Link.UseItem1();
                Link.ChangeState(new State2(Link, textureC, position));
            }
        }

        public void UseItem1Up()
        {
            if (Link != null)
            {
                Link.SetDirection(Direction.Up);
                Link.UseItem1();
                Link.ChangeState(new State2(Link, textureC, position));
            }
        }

        public void UseItem1Down()
        {
            if (Link != null)
            {
                Link.SetDirection(Direction.Down);
                Link.UseItem1();
                Link.ChangeState(new State2(Link, textureC, position));
            }
        }

        public void UseItem2Left()
        {
            if (Link != null)
            {
                Link.SetDirection(Direction.Left);
                Link.UseItem2();
                Link.ChangeState(new State2(Link, textureC, position));
            }
        }

        public void UseItem2Right()
        {
            if (Link != null)
            {
                Link.SetDirection(Direction.Right);
                Link.UseItem2();
                Link.ChangeState(new State2(Link, textureC, position));
            }
        }

        public void UseItem2Up()
        {
            if (Link != null)
            {
                Link.SetDirection(Direction.Up);
                Link.UseItem2();
                Link.ChangeState(new State2(Link, textureC, position));
            }
        }

        public void UseItem2Down()
        {
            if (Link != null)
            {
                Link.SetDirection(Direction.Down);
                Link.UseItem2();
                Link.ChangeState(new State2(Link, textureC, position));
            }
        }

        public void UseItem3Left()
        {
            if (Link != null)
            {
                Link.SetDirection(Direction.Left);
                Link.UseItem3();
                Link.ChangeState(new State2(Link, textureC, position));
            }
        }

        public void UseItem3Right()
        {
            if (Link != null)
            {
                Link.SetDirection(Direction.Right);
                Link.UseItem3();
                Link.ChangeState(new State2(Link, textureC, position));
            }
        }

        public void UseItem3Up()
        {
            if (Link != null)
            {
                Link.SetDirection(Direction.Up);
                Link.UseItem3();
                Link.ChangeState(new State2(Link, textureC, position));
            }
        }

        public void UseItem3Down()
        {
            if (Link != null)
            {
                Link.SetDirection(Direction.Down);
                Link.UseItem3();
                Link.ChangeState(new State2(Link, textureC, position));
            }
        }

        public void TakeDamage()
        {
            if (Link != null)
            {
                Link.TakeDamage();
                Link.ChangeState(new State2(Link, textureC, position));
            }
        }
    }
}