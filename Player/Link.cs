using sprint0.Interfaces;
using sprint0;
using sprint0.PlayerStates;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;


namespace sprint0.Classes
{
	public class Link : ILink, ICollidable
	{

		private SpriteBatch spriteBatch;
		private Game1 game;

		private IPlayerState state;

		private LinkAnimation linkAnimation = new LinkAnimation();

		private GameTime time;

		public enum Direction { Up, Down, Left, Right };
        public Direction direction { get; private set; } = Direction.Right;

		public Vector2 position { get; set; } = new Vector2(400, 200);
        public Vector2 velocity { get; set; } = new Vector2(0, 0);
        
        private const int PLAYER_WIDTH = 48;
        private const int PLAYER_HEIGHT = 48; 

		public Link(SpriteBatch spriteBatch, Game1 game)
		{
			this.spriteBatch = spriteBatch;
			this.game = game;
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

        public void MoveLeft()
		{
			if (state is IdleState || state is MoveState || state is DamagedState)
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
			if (state is IdleState || state is MoveState || state is DamagedState)
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
			if (state is IdleState || state is MoveState || state is DamagedState)
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
			if (state is IdleState || state is MoveState || state is DamagedState)
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
            ChangeState(new ItemState(this, linkAnimation,1));
        }
        public void UseItem2()
		{
			ChangeState(new ItemState(this, linkAnimation,2));
        }
		public void UseItem3()
		{
			ChangeState(new ItemState(this, linkAnimation,3));
        }
		public void TakeDamage()
		{
			Inventory.TakeDamage(1);
			
			if (Inventory.IsDead())
			{
				game.currentState = Game1.GameState.GameOver;
			}
			else
			{
				ChangeState(new KnockbackState(this, linkAnimation));
			}
		}
		public void UseMagic()
		{ 			
			ChangeState(new MagicState(this, linkAnimation));
		}
		
		public Rectangle GetBounds()
		{
			return new Rectangle((int)position.X, (int)position.Y, PLAYER_WIDTH, PLAYER_HEIGHT);
		}
		
		public bool IsSolid()
		{
			return true;
		}
		
		public Vector2 GetPosition()
		{
			return position;
		}
		
		public void HandleCollisionResponse(Vector2 newPosition)
		{
			position = newPosition;
		}
	
		public void OnCollision(ICollidable other, Collisions.CollisionDirection direction)
		{
			switch (other)
			{
				case IEnemy enemy:
					if (!(state is DamagedState))
					{
						HandleEnemyCollision(enemy, direction);
					}
					break;
					
				case IBlock block when block.IsSolid():
					HandleBlockCollision(block, direction);
						break;
					case ICollidable doorwall when doorwall.IsSolid() :
					HandleBlockCollision(doorwall, direction);
					break;
				
				case IItem item:
						break;
					
			}
		}
		
		private void HandleBlockCollision(ICollidable block, Collisions.CollisionDirection direction)
		{
			var collisionResponse = new Collisions.CollisionResponse();
			Vector2 resolvedPosition = collisionResponse.ResolveCollisionDirection(
				this.GetBounds(), block.GetBounds(), direction);
			position = resolvedPosition;
			
			Vector2 newVelocity = velocity;
			if (direction == Collisions.CollisionDirection.Left || direction == Collisions.CollisionDirection.Right)
				newVelocity.X = 0;
			if (direction == Collisions.CollisionDirection.Up || direction == Collisions.CollisionDirection.Down)
				newVelocity.Y = 0;
			velocity = newVelocity;
		}
		
		private void HandleEnemyCollision(ICollidable enemy, Collisions.CollisionDirection direction)
		{
			TakeDamage();

			switch (direction)
			{
				case Collisions.CollisionDirection.Left:
					velocity = new Vector2(3, 0);
					break;
				case Collisions.CollisionDirection.Right:
					velocity = new Vector2(-3, 0);
					break;
				case Collisions.CollisionDirection.Up:
					velocity = new Vector2(0, 3);
					break;
				case Collisions.CollisionDirection.Down:
					velocity = new Vector2(0, -3);
					break;
			}
		}
	}
}