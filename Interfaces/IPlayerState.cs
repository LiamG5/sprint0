using System;
using Microsoft.Xna.Framework;

namespace sprint0.Interfaces
{

	public interface IPlayerState
	{
		void UseState();
		void Update(GameTime gameTime);
		void Enter();
		void Exit();

	}
}
