using sprint0.Interfaces;
using sprint0;
using sprint0.PlayerStates;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using sprint0.Sprites;


namespace sprint0.Classes
{
	public class Link : ILink, ICollidable
	{

	private SpriteBatch spriteBatch;
	private Game1 game;

	private IPlayerState state;

	private LinkAnimation linkAnimation = new LinkAnimation();
	private LinkReplayRecorder replayRecorder;
	private LinkItemHandler itemHandler;
	private LinkCollisionHandler collisionHandler;
	private LinkInvulnerabilityHandler invulnerabilityHandler;

	private GameTime currentGameTime;
	

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
		
	this.replayRecorder = new LinkReplayRecorder();
	this.itemHandler = new LinkItemHandler(this, game);
	this.collisionHandler = new LinkCollisionHandler(this, game);
	this.invulnerabilityHandler = new LinkInvulnerabilityHandler();
}

	public void Update(GameTime gameTime)
	{
		state.Update(gameTime);
		state.UseState();
		position += velocity;
		invulnerabilityHandler.Update(gameTime);
		Color invColor = invulnerabilityHandler.GetFlashColor();

		linkAnimation.Update(gameTime);
		currentGameTime = gameTime;

		if (!Inventory.GetSuperLink())
		{
			linkAnimation.ChangeColor(invColor);
		}
		
		int roomNumber = game.GetCurrentRoomIndex();
		string stateName = state.GetType().Name;
		replayRecorder.RecordFrame(position, direction, roomNumber, stateName);
	}
		
		
	public void SaveReplay()
	{
		replayRecorder.SaveReplay();
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

	public void UseItem1()
		{
			ChangeState(new ItemState(this, linkAnimation, 1));
		}
	public void UseItem2()
	{
		itemHandler.HandleItemUsage(currentGameTime);
		ChangeState(new ItemState(this, linkAnimation, 2));
	}

	public void UseItem3()
		{
			ChangeState(new ItemState(this, linkAnimation, 3));
		}

	public void UseItemInSlot(int itemSlot)
	{
		switch (itemSlot)
		{
			case 1:
				UseItem1();
				break;
			case 2:
				UseItem2();
				break;
			case 3:
				UseItem3();
				break;
		}
	}
	public void TakeDamage(int damage)
	{
		if (!invulnerabilityHandler.IsInvulnerable) {

			Inventory.TakeDamage(damage);
			invulnerabilityHandler.Activate();

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

	public bool IsInvulnerable()
	{
		return invulnerabilityHandler.IsInvulnerable;
	}

	public bool IsInKnockbackPhase()
	{
		return invulnerabilityHandler.IsInKnockbackPhase();
	}
	
	public void OnCollision(ICollidable other, Collisions.CollisionDirection direction)
	{
		collisionHandler.HandleCollision(other, direction);
	}
	}
}