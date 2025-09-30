using sprint0.Interfaces;
using sprint0.Classes;
using sprint0;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using Microsoft.Xna.Framework.Graphics;


namespace sprint0.Player
{
	class Link : ILink
	{
		private LinkSprite linkSprite;
		SpriteBatch spriteBatch;
		Vector2 position = new Vector2(20, 100);
		
		enum Direction { Up, Down, Left, Right };
		private Direction currentDirection;

		public Link(SpriteBatch _spriteBatch, ISprite linkSprite)
		{

			this.linkSprite = (LinkSprite)linkSprite;
			this.spriteBatch = _spriteBatch;
		}

		public void Update()
		{
			linkSprite.Draw(spriteBatch, position);
		}

		public void Idle()
		{
			if (currentDirection == Direction.Left)
			{
				linkSprite.LinkDrawStandingLeft();
			} else if(currentDirection == Direction.Right){
				linkSprite.LinkDrawStandingRight();
			} else if(currentDirection == Direction.Down){
				linkSprite.LinkDrawStandingDown();
			} else if(currentDirection == Direction.Up) {
				linkSprite.LinkDrawStandingUp();
			}
		}

		public void MoveLeft()
		{
			position += new Vector2(-5, 0);
			linkSprite.LinkDrawWalkingLeft();
			currentDirection = Direction.Left;
		}
		public void MoveRight()
		{
			position += new Vector2(5, 0);
			linkSprite.LinkDrawWalkingRight();
			currentDirection = Direction.Right;
        }
		public void MoveUp()
		{
			position += new Vector2(0, -5);
			linkSprite.LinkDrawWalkingUp();
			currentDirection = Direction.Up;
        }
		public void MoveDown()
		{
			position += new Vector2(0, 5);
			linkSprite.LinkDrawWalkingDown();
			currentDirection = Direction.Down;
        }
		public void Attack()
		{
			if (currentDirection == Direction.Left)
			{
				linkSprite.LinkDrawAttackingLeft();
                
            }
            else if (currentDirection == Direction.Right)
			{
				linkSprite.LinkDrawAttackingRight();
                
            }
            else if (currentDirection == Direction.Up)
			{
				linkSprite.LinkDrawAttackingUp();
                
            }
            else if (currentDirection == Direction.Down)
			{
				linkSprite.LinkDrawAttackingDown();
                
            }
        }
		public void Magic()
		{
			if (currentDirection == Direction.Left)
			{
				linkSprite.LinkDrawMagicLeft();
                
            }
            else if (currentDirection == Direction.Right)
			{
				linkSprite.LinkDrawMagicRight();
                
            }
            else if (currentDirection == Direction.Up)
			{
				linkSprite.LinkDrawMagicUp();
                
            }
            else if (currentDirection == Direction.Down)
			{
				linkSprite.LinkDrawMagicDown();
                
            }
        }
		public void UseItem1()
		{
			linkSprite.LinkDrawUseItem1();
		}
		public void UseItem2()
		{
			linkSprite.LinkDrawUseItem1();
		}
		public void UseItem3()
		{
			linkSprite.LinkDrawUseItem1();
        
		}
		public void TakeDamage()
		{
			linkSprite.LinkDrawUseItem1(); // should change color of sprite, this ok for now just need something up
		}
	}

}