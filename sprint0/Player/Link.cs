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

		private LinkAnimation linkAnimation = new LinkAnimation();

		private GameTime time;

		public enum Direction { Up, Down, Left, Right };
        public Direction direction { get; private set; } = Direction.Right;

		public Vector2 position { get; set; } = new Vector2(20, 100);
        public Vector2 velocity { get; set; } = new Vector2(0, 0);

		public Link(SpriteBatch spriteBatch)
		{
			this.spriteBatch = spriteBatch;
			this.linkAnimation = new LinkAnimation();
			this.state = new IdleState(this, linkAnimation);
		}

		public void Update(GameTime gameTime)
		{
			state.Update(gameTime);
			state.UseState();
			position += velocity;
			linkAnimation.Update(gameTime);
		}

        public void Draw(SpriteBatch spriteBatch)
        {
            linkAnimation.Draw(spriteBatch, position);
        }

        public IPlayerState GetCurrentState()
        {
            return state;
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

        //Allow movement if player is idle or already moving
        public void MoveLeft()
		{
			if (state is IdleState || state is MoveState)
			{
				velocity = new Vector2(-5, 0);
                direction = Direction.Left;
                if (state is IdleState)
                {
                    ChangeState(new MoveState(this, linkAnimation));
                }
            }
        }

		public void MoveRight()
		{
			if (state is IdleState || state is MoveState)
			{
				velocity = new Vector2(5, 0);
				direction = Direction.Right;
                if (state is IdleState)
                {
                    ChangeState(new MoveState(this, linkAnimation));
                }
            }
        }

		public void MoveUp()
		{
			if (state is IdleState || state is MoveState)
			{
				velocity = new Vector2(0, -5);
				direction = Direction.Up;
                if (state is IdleState)
                {
                    ChangeState(new MoveState(this, linkAnimation));
                }
            }
        }

		public void MoveDown()
		{
			if (state is IdleState || state is MoveState)
			{
				velocity = new Vector2(0, 5);
				direction = Direction.Down;
                if (state is IdleState)
                {
                    ChangeState(new MoveState(this, linkAnimation));
                }
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