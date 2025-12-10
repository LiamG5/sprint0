using sprint0;
using sprint0.PlayerStates;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using sprint0.Sprites;
using System.Collections.Generic;
using sprint0.Interfaces;

namespace sprint0.Classes
{
	public class Portal : ISprite, ICollidable
	{
		private Texture2D portals;
		public readonly Rectangle startPortalSrc = new Rectangle(16 * 5, 16 * 22, 16, 16);
		public readonly Rectangle endPortalSrc = new Rectangle(16 * 5, 16 * 23, 16, 16);
		public Vector2 position;
		public static Portal StartPortalInstance { get; private set; }
		public static Portal EndPortalInstance { get; private set; }

		private static KeyboardState previousKeyState = Keyboard.GetState();

		public Portal(Vector2 position)
		{
			this.portals = sprint0.Sprites.Texture2DStorage.enemiesSpriteSheet;
			this.position = position;
		}
		public void Update(GameTime gameTime)
		{
		}
		public void Draw(SpriteBatch spriteBatch, Vector2 position)
		{
			if (portals == null) return;
			Rectangle src = (this == StartPortalInstance) ? startPortalSrc : endPortalSrc;
			spriteBatch.Draw(portals, position, src, Color.White, 0f, Vector2.Zero, 3.0f, SpriteEffects.None, 0f);
		}
		public static void DrawAll(SpriteBatch spriteBatch)
		{
			if (StartPortalInstance != null)
			{
				StartPortalInstance.Draw(spriteBatch, StartPortalInstance.position);
			}
			if (EndPortalInstance != null)
			{
				EndPortalInstance.Draw(spriteBatch, EndPortalInstance.position);
			}
		}

		public Rectangle GetBounds()
		{
			return new Rectangle((int)position.X, (int)position.Y, 44, 44);
		}

		public bool BlocksMovement()
		{
			return true;
		}

		public bool BlocksProjectiles()
		{
			return false;
		}

		public Vector2 GetPosition()
		{
			return position;
		}

		public void OnCollision(ICollidable other, Collisions.CollisionDirection direction)
		{
			switch (other)
			{
				case Link link:
					if (StartPortalInstance == null || EndPortalInstance == null) return;
					Portal target = (this == StartPortalInstance) ? EndPortalInstance : StartPortalInstance;
					if (target == null) return;
					Vector2 teleportPosition = target.position;
					teleportPosition.Y += 45;
					link.HandleCollisionResponse(teleportPosition);
					break;
			}
		}
		public static void HandleInput(KeyboardState currentState, KeyboardState? previousStateNullable, Game1 game, SpriteBatch spriteBatch)
		{
			KeyboardState prev = previousStateNullable ?? previousKeyState;

			if (currentState.IsKeyDown(Keys.K) && prev.IsKeyUp(Keys.K))
			{
				// if start portal not set -> save current Link position as start and spawn a portal there
				if (StartPortalInstance == null)
				{
					Vector2 startPos = game.link.GetPosition();
					StartPortalInstance = new Portal(startPos);
				}
				// else if end portal not set -> create end portal at Link's current position
				else if (EndPortalInstance == null)
				{
					Vector2 endPos = game.link.GetPosition();
					EndPortalInstance = new Portal(endPos);
				}
				else
				{
					// both exist: start a new pair by replacing start and clearing end
					StartPortalInstance = new Portal(game.link.GetPosition());
					EndPortalInstance = null;
				}
			}

			if (!previousStateNullable.HasValue)
			{
				previousKeyState = currentState;
			}
		}
	}
}