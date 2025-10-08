using sprint0.Interfaces;
using sprint0;
using sprint0.PlayerStates;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;


namespace sprint0.Classes
{
	public class Link : ILink
	{

		private SpriteBatch spriteBatch;

		private IPlayerState state;

        private Vector2 position = new Vector2(20, 100);
        private Vector2 velocity = new Vector2(0, 0);

		private LinkAnimation linkAnimation = new LinkAnimation();

		private GameTime time;

		public enum Direction { Up, Down, Left, Right };
        public Direction direction { get; private set; } = Direction.Right;

		public Link(SpriteBatch spriteBatch)
		{
			this.spriteBatch = spriteBatch;
			
		}

		public void Update(GameTime gameTime)
		{
			state.Update(gameTime);
			state.UseState();
			position += velocity;

            linkAnimation.Draw(spriteBatch, position);
		}

        public void ChangeState(IPlayerState newState)
        {
            state.Exit();
            state = newState;
            state.Enter();
        }

        public void Idle()
        {
			velocity = new Vector2(0, 0);
            ChangeState(new IdleState(this, linkAnimation));
        }

        //Do not allow movement if player is acting or taking damage
        public void MoveLeft()
		{
			if (state is IdleState)
			{
				velocity = new Vector2(-5, 0);
                direction = Direction.Left;
                ChangeState(new MoveState(this, linkAnimation));
            }
        }

		public void MoveRight()
		{
			if (state is IdleState)
			{
				velocity = new Vector2(5, 0);
				direction = Direction.Right;
                ChangeState(new MoveState(this, linkAnimation));
            }
        }

		public void MoveUp()
		{
			if (state is IdleState)
			{
				velocity = new Vector2(0, -5);
				direction = Direction.Up;
                ChangeState(new MoveState(this, linkAnimation));
            }
        }

		public void MoveDown()
		{
			if (state is IdleState)
			{
				velocity = new Vector2(0, 5);
				direction = Direction.Down;
                ChangeState(new MoveState(this, linkAnimation));
            }
        }

		public void Attack()
		{
			ChangeState(new AttackState(this, linkAnimation));
        }
        public void UseItem1()
		{
            ChangeState(new ItemState(this, linkAnimation));
        }
        public void UseItem2()
		{
			ChangeState(new ItemState(this, linkAnimation));
        }
		public void UseItem3()
		{
			ChangeState(new ItemState(this, linkAnimation));
        }
		public void TakeDamage()
		{
			ChangeState(new DamagedState(this, linkAnimation));
		}
		public void UseMagic()
		{ 			
			ChangeState(new MagicState(this, linkAnimation));
		}
    }

}