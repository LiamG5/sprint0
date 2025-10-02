using System;
using Microsoft.Xna.Framework;

namespace sprint0.Interfaces
{

	public interface IPlayerState
	{

		void UseState(Direction direction, LinkSprite linksprite);
		void Update(GameTime gameTime);
		void Enter(Direction direction, LinkSprite linksprite);
		void Exit();

	}
}
