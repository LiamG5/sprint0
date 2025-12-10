using sprint0.Interfaces;
using sprint0;
using sprint0.PlayerStates;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using sprint0.Sprites;
using System.IO;
using System.Collections.Generic;


namespace sprint0.Classes
{
	public class Link : ILink, ICollidable
	{

		private SpriteBatch spriteBatch;
		private Game1 game;

		private IPlayerState state;

		private LinkAnimation linkAnimation = new LinkAnimation();

		private GameTime time;
		
		private System.Collections.Generic.List<string> replayData;
	

		public enum Direction { Up, Down, Left, Right };
		public Direction direction { get; set; } = Direction.Up;

		public Vector2 position { get; set; } = new Vector2(384, 440);
		public Vector2 velocity { get; set; } = new Vector2(0, 0);

		private const int PLAYER_WIDTH = 44;
		private const int PLAYER_HEIGHT = 44;
		public LinkAttackHitbox linkAttackHitbox;
		public Link(SpriteBatch spriteBatch, Game1 game, Vector2 position)
		{
			this.spriteBatch = spriteBatch;
			this.game = game;
			this.linkAnimation = new LinkAnimation();
			this.linkAnimation.SetPlayer(this);
			this.state = new IdleState(this, linkAnimation);
			this.linkAttackHitbox = new LinkAttackHitbox();
			
			replayData = new List<string>();
		}

		public void Update(GameTime gameTime)
		{
			state.Update(gameTime);
			state.UseState();
			position += velocity;
			linkAnimation.Update(gameTime);
			
			try
			{
				int roomNumber = game.GetCurrentRoomIndex();
				string stateName = state.GetType().Name;
				replayData.Add($"{position.X},{position.Y},{(int)direction},{roomNumber},{stateName}");

			}
			catch (System.Exception ex) {}
		}
		
		
		public void SaveReplay()
		{
			try
			{
				System.IO.File.WriteAllLines("link_replay.txt", replayData);
				replayData.Clear();
			}
			catch (System.Exception ex) {}
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
		var swordBeam = sprint0.Sprites.Projectiles.ProjectileSwordBeam.Create(position, direction);
		game.AddProjectile(swordBeam);
		sprint0.Sounds.SoundStorage.LOZ_Sword_Shoot.Play();
	}

	private void FireArrow()
	{
		Vector2 startPosition = position + new Vector2(PLAYER_WIDTH / 2, PLAYER_HEIGHT / 2);
		var arrow = sprint0.Sprites.Projectiles.ProjectileArrow.Create(startPosition, direction);
		game.AddProjectile(arrow);
		sprint0.Sounds.SoundStorage.LOZ_Arrow_Boomerang.Play();
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
				else if (itemInSlotB.Value == sprint0.Sprites.ItemFactory.ItemType.Bow
				      || itemInSlotB.Value == sprint0.Sprites.ItemFactory.ItemType.SilverArrow)
				{
					if (Inventory.HasBow() && Inventory.SpendRupees(1))
					{
						FireArrow();
						ChangeState(new ItemState(this, linkAnimation, 2));
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
		public void TakeDamage(int damage)
		{
			Inventory.TakeDamage(damage);

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

		public void Win()
		{
			ChangeState(new WinState(this, linkAnimation));
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
			case EnemyWallmaster wallmaster:
				game.GoToRoom2();
				break;
			case EnemyFlame flame:
				HandleBlockCollision(flame, direction);
				break;
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
		
		private void HandleEnemyCollision(IEnemy enemy, Collisions.CollisionDirection direction)
		{
            if (Inventory.GetSuperLink())
            {
				enemy.TakeDamage(100);
                return;
            }

			TakeDamage(1);

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