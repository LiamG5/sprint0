using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using sprint0.Interfaces;
using sprint0;
using System;

namespace sprint0.Classes
{
    public class State1 : ISprite, ILinkState
    {
        Texture2D texture;
        private Link Link;
        private Vector2 position;

        public State1(Texture2D texture1)
        {
            texture = texture1;
            position = new Vector2(100, 300);
        }

        public State1(Link Link, Texture2D texture1, Vector2 position)
        {
            this.Link = Link;
            texture = texture1;
            this.position = position;
        }

        public void Update(GameTime gameTime)
        {
        
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, Color.White);
        }

        public void Update()
        {
        
        }

        public void MoveLeft()
        {
            if (Link != null)
            {
                Link.ChangeState(new State4(Link, texture, position));
            }
        }

        public void MoveRight()
        {
            if (Link != null)
            {
                Link.ChangeState(new State4(Link, texture, position));
            }
        }

        public void MoveUp()
        {
            if (Link != null)
            {
                Link.ChangeState(new State4(Link, texture, position));
            }
        }

        public void MoveDown()
        {
            if (Link != null)
            {
                Link.ChangeState(new State4(Link, texture, position));
            }
        }

        public void AttackLeft()
        {
            if (Link != null)
            {
                Link.SetDirection(Direction.Left);
                Link.Attack();
                Link.ChangeState(new State2(Link, texture, position));
            }
        }

        public void AttackRight()
        {
            if (Link != null)
            {
                Link.SetDirection(Direction.Right);
                Link.Attack();
                Link.ChangeState(new State2(Link, texture, position));
            }
        }

        public void AttackUp()
        {
            if (Link != null)
            {
                Link.SetDirection(Direction.Up);
                Link.Attack();
                Link.ChangeState(new State2(Link, texture, position));
            }
        }

        public void AttackDown()
        {
            if (Link != null)
            {
                Link.SetDirection(Direction.Down);
                Link.Attack();
                Link.ChangeState(new State2(Link, texture, position));
            }
        }

        public void UseItem1Left()
        {
            if (Link != null)
            {
                Link.SetDirection(Direction.Left);
                Link.UseItem1();
                Link.ChangeState(new State2(Link, texture, position));
            }
        }

        public void UseItem1Right()
        {
            if (Link != null)
            {
                Link.SetDirection(Direction.Right);
                Link.UseItem1();
                Link.ChangeState(new State2(Link, texture, position));
            }
        }

        public void UseItem1Up()
        {
            if (Link != null)
            {
                Link.SetDirection(Direction.Up);
                Link.UseItem1();
                Link.ChangeState(new State2(Link, texture, position));
            }
        }

        public void UseItem1Down()
        {
            if (Link != null)
            {
                Link.SetDirection(Direction.Down);
                Link.UseItem1();
                Link.ChangeState(new State2(Link, texture, position));
            }
        }

        public void UseItem2Left()
        {
            if (Link != null)
            {
                Link.SetDirection(Direction.Left);
                Link.UseItem2();
                Link.ChangeState(new State2(Link, texture, position));
            }
        }

        public void UseItem2Right()
        {
            if (Link != null)
            {
                Link.SetDirection(Direction.Right);
                Link.UseItem2();
                Link.ChangeState(new State2(Link, texture, position));
            }
        }

        public void UseItem2Up()
        {
            if (Link != null)
            {
                Link.SetDirection(Direction.Up);
                Link.UseItem2();
                Link.ChangeState(new State2(Link, texture, position));
            }
        }

        public void UseItem2Down()
        {
            if (Link != null)
            {
                Link.SetDirection(Direction.Down);
                Link.UseItem2();
                Link.ChangeState(new State2(Link, texture, position));
            }
        }

        public void UseItem3Left()
        {
            if (Link != null)
            {
                Link.SetDirection(Direction.Left);
                Link.UseItem3();
                Link.ChangeState(new State2(Link, texture, position));
            }
        }

        public void UseItem3Right()
        {
            if (Link != null)
            {
                Link.SetDirection(Direction.Right);
                Link.UseItem3();
                Link.ChangeState(new State2(Link, texture, position));
            }
        }

        public void UseItem3Up()
        {
            if (Link != null)
            {
                Link.SetDirection(Direction.Up);
                Link.UseItem3();
                Link.ChangeState(new State2(Link, texture, position));
            }
        }

        public void UseItem3Down()
        {
            if (Link != null)
            {
                Link.SetDirection(Direction.Down);
                Link.UseItem3();
                Link.ChangeState(new State2(Link, texture, position));
            }
        }
    }
}