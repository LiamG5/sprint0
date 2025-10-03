using sprint0.Interfaces;
using sprint0;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using System;


namespace sprint0.Classes
{
	class Link : ILink
	{
		
		

		private SpriteBatch spriteBatch;
        private IPlayerState state;
        private Vector2 position = new Vector2(20, 100);
        private Vector2 velocity = new Vector2(0, 0);

		private LinkAnimation linkAnimation;

        public Direction direction { get; private set; } = Direction.Right;

		public Link(SpriteBatch spriteBatch)
		{
			
			this.spriteBatch = _spriteBatch;
			this.linkAnimation = new LinkAnimation();
		}

		public void Update()
		{
			state.Update();
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
            ChangeState(new IdleState());
        }

        public void MoveLeft()
		{
			velocity = new Vector2(-5,0);
			linkAnimation.LinkWalkingLeft();
			direction = Direction.Left;
        }
		public void MoveRight()
		{
			velocity = new Vector2(5, 0);
			linkAnimation.LinkWalkingRight();
			direction = Direction.Right;
        }
		public void MoveUp()
		{
            velocity = new Vector2(0, -5);
			linkAnimation.LinkWalkingUp
			direction = Direction.Up;
        }
		public void MoveDown()
		{
            velocity = new Vector2(0, 5);
			linkAnimation.LinkWalkingDown();
			direction = Direction.Down;
        }
		public void Attack()
		{
			ChangeState(new AttackState());
        }
        public void UseItem1()
		{
            ChangeState(new Item1State());
        }
        public void UseItem2()
		{
			ChangeState(new Item2State());
        }
		public void UseItem3()
		{
			ChangeState(new Item3State());
        }

		public void TakeDamage()
		{
			ChangeState(new DamagedState);
		}
	}

}