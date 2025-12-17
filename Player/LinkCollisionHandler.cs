using Microsoft.Xna.Framework;
using sprint0.Interfaces;
using sprint0.Sprites;

namespace sprint0.Classes
{
	public class LinkCollisionHandler
	{
		private Link link;
		private Game1 game;

		public LinkCollisionHandler(Link link, Game1 game)
		{
			this.link = link;
			this.game = game;
		}

		public void HandleCollision(ICollidable other, Collisions.CollisionDirection direction)
		{
			switch (other)
			{
				case EnemyWallmaster enemy:
					HandleWallmasterCollision(enemy);
					break;
				case EnemyFlame flame:
					HandleBlockCollision(flame, direction);
					break;
				case IEnemy enemy:
					HandleEnemyCollision(enemy, direction);
					break;
				case IBlock block when block.BlocksMovement():
					HandleBlockCollision(block, direction);
					break;
				case ICollidable doorwall when doorwall.BlocksMovement():
					HandleBlockCollision(doorwall, direction);
					break;
				case IItem item:
					break;
			}
		}

		private void HandleWallmasterCollision(EnemyWallmaster enemy)
		{
			if (!Inventory.GetSuperLink())
			{
				game.GoToRoom2();
			}
			else
			{
				enemy.TakeDamage(100);
			}
		}

		private void HandleBlockCollision(ICollidable block, Collisions.CollisionDirection direction)
		{
			var collisionResponse = new Collisions.CollisionResponse();
			Vector2 resolvedPosition = collisionResponse.ResolveCollisionDirection(
				link.GetBounds(), block.GetBounds(), direction);
			link.HandleCollisionResponse(resolvedPosition);

			Vector2 newVelocity = link.velocity;
			if (direction == Collisions.CollisionDirection.Left || direction == Collisions.CollisionDirection.Right)
				newVelocity.X = 0;
			if (direction == Collisions.CollisionDirection.Up || direction == Collisions.CollisionDirection.Down)
				newVelocity.Y = 0;
			link.velocity = newVelocity;
		}

	private void HandleEnemyCollision(IEnemy enemy, Collisions.CollisionDirection direction)
	{
		if (link.IsInvulnerable())
			return;

		if (Inventory.GetSuperLink())
		{
			enemy.TakeDamage(100);
			return;
		}

		link.TakeDamage(1);
		
		if (link.IsInKnockbackPhase())
		{
			link.velocity = CalculateKnockbackVelocity(direction);
		}
	}

		private Vector2 CalculateKnockbackVelocity(Collisions.CollisionDirection direction)
		{
			switch (direction)
			{
				case Collisions.CollisionDirection.Left:
					return new Vector2(3, 0);
				case Collisions.CollisionDirection.Right:
					return new Vector2(-3, 0);
				case Collisions.CollisionDirection.Up:
					return new Vector2(0, 3);
				case Collisions.CollisionDirection.Down:
					return new Vector2(0, -3);
				default:
					return Vector2.Zero;
			}
		}
	}
}

