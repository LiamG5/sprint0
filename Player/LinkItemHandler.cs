using Microsoft.Xna.Framework;
using sprint0.Factories;
using sprint0.Sprites;

namespace sprint0.Classes
{
	public class LinkItemHandler
	{
		private Link link;
		private Game1 game;
		private float lastArrowTime = 0;
		private const float ARROW_COOLDOWN = 500f;
		private const int PLAYER_WIDTH = 44;
		private const int PLAYER_HEIGHT = 44;

		public LinkItemHandler(Link link, Game1 game)
		{
			this.link = link;
			this.game = game;
		}

		public bool HandleItemUsage(GameTime gameTime)
		{
			if (game == null) return false;

			var itemInSlotB = game.GetItemInSlotB();
			if (!itemInSlotB.HasValue) return false;

			var itemType = itemInSlotB.Value;

			if (IsBoomerangType(itemType))
			{
				ThrowBoomerang();
				return true;
			}
			else if (itemType == ItemFactory.ItemType.Bomb)
			{
				return TryDropBomb();
			}
			else if (IsBowType(itemType))
			{
				return TryFireArrow(gameTime);
			}

			return false;
		}

		private bool IsBoomerangType(ItemFactory.ItemType itemType)
		{
			return itemType == ItemFactory.ItemType.Boomerang || 
			       itemType == ItemFactory.ItemType.MagicalBoomerang;
		}

		private bool IsBowType(ItemFactory.ItemType itemType)
		{
			return itemType == ItemFactory.ItemType.Bow || 
			       itemType == ItemFactory.ItemType.SilverArrow;
		}

		private bool TryFireArrow(GameTime gameTime)
		{
			float currentTime = (float)gameTime.TotalGameTime.TotalMilliseconds;
			
			if (currentTime - lastArrowTime < ARROW_COOLDOWN)
				return false;

			if (!Inventory.HasBow() || !Inventory.SpendRupees(1))
				return false;

			FireArrow();
			lastArrowTime = currentTime;
			return true;
		}

		private bool TryDropBomb()
		{
			if (!Inventory.UseBomb())
				return false;

			DropBomb();
			return true;
		}

		private void ThrowBoomerang()
		{
			if (game.HasActiveBoomerang())
				return;

			Vector2 velocity = GetDirectionalVelocity(6);
			Vector2 startPosition = link.position + new Vector2(PLAYER_WIDTH / 2, PLAYER_HEIGHT / 2);
			Rectangle sourceRect = new Rectangle(40 * 7, 40 * 0, 15, 16);

			var boomerang = new BoomerangProjectile(
				Texture2DStorage.GetItemSpriteSheet(),
				sourceRect,
				startPosition,
				velocity,
				link
			);

			game.AddBoomerangProjectile(boomerang);
		}

		private void DropBomb()
		{
			Rectangle sourceRect = new Rectangle(40 * 5, 40 * 0, 15, 16);
			Vector2 startPosition = link.position + new Vector2(PLAYER_WIDTH / 2, PLAYER_HEIGHT / 2);

			var bomb = new BombProjectile(
				Texture2DStorage.GetItemSpriteSheet(),
				sourceRect,
				startPosition
			);

			game.AddBombProjectile(bomb);
		}

		private void FireArrow()
		{
			var arrow = sprint0.Sprites.Projectiles.ProjectileArrow.Create(link.position, link.direction);
			game.AddProjectile(arrow);
			sprint0.Sounds.SoundStorage.LOZ_Arrow_Boomerang.Play();
		}

		private Vector2 GetDirectionalVelocity(float speed)
		{
			switch (link.direction)
			{
				case Link.Direction.Up:
					return new Vector2(0, -speed);
				case Link.Direction.Down:
					return new Vector2(0, speed);
				case Link.Direction.Left:
					return new Vector2(-speed, 0);
				case Link.Direction.Right:
					return new Vector2(speed, 0);
				default:
					return Vector2.Zero;
			}
		}
	}
}

