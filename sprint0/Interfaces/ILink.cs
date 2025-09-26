using System;
using Microsoft.Xna.Framework;

namespace sprint0.Interfaces
{

	public interface ILink
	{
		void Update();

		void MoveLeft();
		void MoveRight();
		void MoveUp();
		void MoveDown();

		void Attack();

		void UseItem1();
		void UseItem2();
		void UseItem3();

		void TakeDamage();
	}
}