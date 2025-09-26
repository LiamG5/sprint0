using sprint0.Interfaces;
using sprint0;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;


namespace sprint0.Classes
{
	class Link : ILink
	{
		private SpriteMain linkSprite;
		ILinkState state;
		Vector2 position = new Vector2(20, 100);
		
		enum Direction { Up, Down, Left, Right };

        public Link(ISprite linkSprite)
		{
			this.linkSprite = (SpriteMain)linkSprite;
		}

		public void Update()
		{
			state.Update();
		}
		public void MoveLeft()
		{
			position += new Vector2(-5,0);
			linkWalkLeft.Draw();
			Direction = Direction.Left;
        }
		public void MoveRight()
		{
			position += new Vector2(5, 0);
			linkWalkRight.Draw();
			Direction = Direction.Right;
        }
		public void MoveUp()
		{
			position += new Vector2(0, -5);
			linkWalkUp.Draw();
			Direction = Direction.Up;
        }
		public void MoveDown()
		{
			position += new Vector2(0, 5);
			linkWalkDown.Draw();
			Direction = Direction.Down;
        }
		public void Attack()
		{
			if (Direction == Direction.Left)
			{
				linkAttackLeft.Draw();
                state.AttackLeft();
            }
            else if (Direction == Direction.Right)
			{
				linkAttackRight.Draw();
                state.AttackRight();
            }
            else if (Direction == Direction.Up)
			{
				linkAttackUp.Draw();
                state.AttackUp();
            }
            else if (Direction == Direction.Down)
			{
				linkAttackDown.Draw();
                state.AttackDown();
            }
        }
		public void UseItem1()
		{
			if (Direction == Direction.Left)
			{
				linkUseItem1Left.Draw();
				state.UseItem1Left();
			}
			else if (Direction == Direction.Right)
			{
				linkUseItem1Right.Draw();
				state.UseItem1Right();
			}
			else if (Direction == Direction.Up)
			{
				linkUseItem1Up.Draw();
				state.UseItem1Up();
			}
			else if (Direction == Direction.Down)
			{
				linkUseItem1Down.Draw();
				state.UseItem1Down();
			}
		}
		public void UseItem2()
		{
			if (Direction == Direction.Left)
			{
				linkUseItem2Left.Draw();
				state.UseItem2Left();
			}
			else if (Direction == Direction.Right)
			{
				linkUseItem2Right.Draw();
				state.UseItem2Right();
			}
			else if (Direction == Direction.Up)
			{
				linkUseItem2Up.Draw();
				state.UseItem2Up();
			}
			else if (Direction == Direction.Down)
			{
				linkUseItem2Down.Draw();
				state.UseItem2Down();
            }
		}
		public void UseItem3()
		{
			if (Direction == Direction.Left)
			{
				linkUseItem3Left.Draw();
				state.UseItem3Left();
			}
			else if (Direction == Direction.Right)
			{
				linkUseItem3Right.Draw();
				state.UseItem3Right();
			}
			else if (Direction == Direction.Up)
			{
				linkUseItem3Up.Draw();
				state.UseItem3Up();
			}
			else if (Direction == Direction.Down)
			{
				linkUseItem3Down.Draw();
				state.UseItem3Down();
            }
		}
		public void TakeDamage()
		{
			state.TakeDamage();
		}
	}

}