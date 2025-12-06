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
		public LinkAttackHitbox linkAttackHitbox;
		public Link(SpriteBatch spriteBatch, Game1 game)
		{
			this.spriteBatch = spriteBatch;
			this.game = game;
			this.linkAnimation = new LinkAnimation();
			this.state = new IdleState(this, linkAnimation);
			this.linkAttackHitbox = new LinkAttackHitbox();
			
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
			if (state is IdleState || state is MoveState || state is DamagedState)
			{
				ChangeState(new AttackState(this, linkAnimation, linkAttackHitbox));
			}
		}
		public void FireSwordBeam()
		{			
			Rectangle sourceRect = new Rectangle(0, 0, 0, 0);
			Vector2 velocity = new Vector2(0, 0);
			Vector2 startPosition = position;
			
			switch (direction)
			{
				case Direction.Up:
					sourceRect = new Rectangle(60, 220, 22, 22); 
					velocity = new Vector2(0, -8);
					break;
				case Direction.Down:
					sourceRect = new Rectangle(0, 220, 22, 22); 
					velocity = new Vector2(0, 8);
					break;
				case Direction.Left:
					sourceRect = new Rectangle(26, 225, 22, 22);
					velocity = new Vector2(-8, 0);
					break;
				case Direction.Right:
					sourceRect = new Rectangle(85, 225, 22, 22);
					velocity = new Vector2(8, 0);
					break;
			}
						
			var swordBeam = new sprint0.Sprites.Projectile(
				sprint0.Sprites.Texture2DStorage.GetLinkSpriteSheet(), 
				sourceRect, 
				position, 
				velocity, 
				damage: 2, 
				isEnemyProjectile: false
			);			

			game.AddProjectile(swordBeam);
		}

		public void UseItem1()
		{
			ChangeState(new ItemState(this, linkAnimation, 1));
		}
	public void UseItem2()
	{
		if (game != null)
		{
			var itemInSlotB = game.GetItemInSlotB();
			if (itemInSlotB.HasValue)
			{
				if (itemInSlotB.Value == sprint0.Sprites.ItemFactory.ItemType.Boomerang || 
				    itemInSlotB.Value == sprint0.Sprites.ItemFactory.ItemType.MagicalBoomerang)
				{
					ThrowBoomerang();
					return;
				}
				else if (itemInSlotB.Value == sprint0.Sprites.ItemFactory.ItemType.Bomb)
				{
					if (Inventory.UseBomb())
					{
						DropBomb();
						return;
					}
				}
			}
		}
		ChangeState(new ItemState(this, linkAnimation, 2));
	}

	private void ThrowBoomerang()
	{
		if (game.HasActiveBoomerang())
		{
			return;
		}
		
		Rectangle sourceRect = new Rectangle(40 * 7, 40 * 0, 15, 16);
		Vector2 velocity = new Vector2(0, 0);
		Vector2 startPosition = position + new Vector2(PLAYER_WIDTH / 2, PLAYER_HEIGHT / 2);
		
		switch (direction)
		{
			case Direction.Up:
				velocity = new Vector2(0, -6);
				break;
			case Direction.Down:
				velocity = new Vector2(0, 6);
				break;
			case Direction.Left:
				velocity = new Vector2(-6, 0);
				break;
			case Direction.Right:
				velocity = new Vector2(6, 0);
				break;
		}
		
		var boomerang = new sprint0.Sprites.BoomerangProjectile(
			sprint0.Sprites.Texture2DStorage.GetItemSpriteSheet(),
			sourceRect,
			startPosition,
			velocity,
			this
		);
		
		game.AddBoomerangProjectile(boomerang);
	}

	private void DropBomb()
	{
		Rectangle sourceRect = new Rectangle(40 * 5, 40 * 0, 15, 16);
		Vector2 startPosition = position + new Vector2(PLAYER_WIDTH / 2, PLAYER_HEIGHT / 2);
		
		var bomb = new sprint0.Sprites.BombProjectile(
			sprint0.Sprites.Texture2DStorage.GetItemSpriteSheet(),
			sourceRect,
			startPosition
		);
		
		game.AddBombProjectile(bomb);
	}

		public void UseItem3()
		{
			ChangeState(new ItemState(this, linkAnimation, 3));
		}
		public void TakeDamage()
		{
			Inventory.TakeDamage(1);

			if (Inventory.IsDead())
			{
				sprint0.Sounds.SoundStorage.LOZ_Link_Die.Play();
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

		public bool BlocksMovement()
		{
			return true;
		}
		
		public bool BlocksProjectiles()
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
				if (!(state is DamagedState) && !(state is KnockbackState))
				{
					HandleEnemyCollision(enemy, direction);
				}
				break;
					
				case IBlock block when block.BlocksMovement():
					HandleBlockCollision(block, direction);
						break;
					case ICollidable doorwall when doorwall.BlocksMovement() :
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