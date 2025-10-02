using sprint0.Interfaces;
using sprint0;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;


namespace sprint0.Classes
{
	class Link : ILink
	{
		
		private LinkSprite linkSprite;

        private IPlayerState state;
        private Vector2 position = new Vector2(20, 100);
        private Vector2 velocity = new Vector2(0, 0);

        public Direction direction { get; private set; } = Direction.Right;

        public Link(ISprite linkSprite)
		{
			this.linkSprite = (SpriteMain)linkSprite;
		}

		public void Update()
		{
			state.Update();
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
			link.DrawWalkLeft();
			direction = Direction.Left;
        }
		public void MoveRight()
		{
			velocity = new Vector2(5, 0);
			link.DrawWalkDown();
			direction = Direction.Right;
        }
		public void MoveUp()
		{
            velocity = new Vector2(0, -5);
			linkWalkUp.Draw();
			direction = Direction.Up;
        }
		public void MoveDown()
		{
            velocity = new Vector2(0, 5);
			linkWalkDown.Draw();
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