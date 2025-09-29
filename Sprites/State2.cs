using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using sprint0.Interfaces;
using sprint0;
using System;

namespace sprint0.Classes
{
    public class State2 : ISprite, ILinkState
    {
        public Texture2D[] textures;
        public Texture2D textureC;
        private int count = 0;
        private Link Link;
        private Vector2 position;

        public State2(Texture2D texture1, Texture2D texture2)
        {
            textures = new Texture2D[2] {texture1 ,texture2};
            textureC = texture1;
            position = new Vector2(100, 300);
        }

        public State2(Link Link, Texture2D texture1, Vector2 position)
        {
            this.Link = Link;
            textures = new Texture2D[2] {texture1 ,texture1};
            textureC = texture1;
            this.position = position;
        }

        public void Update(GameTime gameTime)
        {
            textureC = textures[count % 2];
            count++;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(textureC, position, Color.White);
        }

        public void Update()
        {
            textureC = textures[count % 2];
            count++;
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
            }
        }

        public void AttackRight()
        {
            if (Link != null)
            {
                Link.SetDirection(Direction.Right);
                Link.Attack();
            }
        }

        public void AttackUp()
        {
            if (Link != null)
            {
                Link.SetDirection(Direction.Up);
                Link.Attack();
            }
        }

        public void AttackDown()
        {
            if (Link != null)
            {
                Link.SetDirection(Direction.Down);
                Link.Attack();
            }
        }

        public void UseItem1Left()
        {
            if (Link != null)
            {
                Link.SetDirection(Direction.Left);
                Link.UseItem1();
            }
        }

        public void UseItem1Right()
        {
            if (Link != null)
            {
                Link.SetDirection(Direction.Right);
                Link.UseItem1();
            }
        }

        public void UseItem1Up()
        {
            if (Link != null)
            {
                Link.SetDirection(Direction.Up);
                Link.UseItem1();
            }
        }

        public void UseItem1Down()
        {
            if (Link != null)
            {
                Link.SetDirection(Direction.Down);
                Link.UseItem1();
            }
        }

        public void UseItem2Left()
        {
            if (Link != null)
            {
                Link.SetDirection(Direction.Left);
                Link.UseItem2();
            }
        }

        public void UseItem2Right()
        {
            if (Link != null)
            {
                Link.SetDirection(Direction.Right);
                Link.UseItem2();
            }
        }

        public void UseItem2Up()
        {
            if (Link != null)
            {
                Link.SetDirection(Direction.Up);
                Link.UseItem2();
            }
        }

        public void UseItem2Down()
        {
            if (Link != null)
            {
                Link.SetDirection(Direction.Down);
                Link.UseItem2();
            }
        }

        public void UseItem3Left()
        {
            if (Link != null)
            {
                Link.SetDirection(Direction.Left);
                Link.UseItem3();
            }
        }

        public void UseItem3Right()
        {
            if (Link != null)
            {
                Link.SetDirection(Direction.Right);
                Link.UseItem3();
            }
        }

        public void UseItem3Up()
        {
            if (Link != null)
            {
                Link.SetDirection(Direction.Up);
                Link.UseItem3();
            }
        }

        public void UseItem3Down()
        {
            if (Link != null)
            {
                Link.SetDirection(Direction.Down);
                Link.UseItem3();
            }
        }

        public void TakeDamage()
        {
            if (Link != null)
            {
                Link.TakeDamage();
            }
        }
    }
}