using sprint0.Interfaces;
using sprint0;
using sprint0.PlayerStates;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using sprint0.Sprites;
using System.IO;

namespace sprint0.Classes
{
	public class GhostLink
	{
		private LinkAnimation linkAnimation;
		private Vector2 position;
		private Link.Direction direction;
		private int currentRoomNumber;
		private string lastStateName = "";
		private string currentStateName = "IdleState";
		
	private StreamReader replayReader;
	
	private float animationTime = 0f;
	private float animationDuration = 1000f;
	private bool isDone = false;
		
		public GhostLink()
		{
			this.linkAnimation = new LinkAnimation();
			this.position = Vector2.Zero;
			this.direction = Link.Direction.Right;
			this.currentRoomNumber = -1;
			
			try
			{
				if (File.Exists("link_replay.txt"))
				{
					FileStream fileStream = new FileStream("link_replay.txt", FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
					replayReader = new StreamReader(fileStream);

                    string firstLine = replayReader.ReadLine();
                    if (!string.IsNullOrEmpty(firstLine))
                    {
                        string[] parts = firstLine.Split(',');

                        this.position = new Vector2(float.Parse(parts[0]), float.Parse(parts[1]));
                        this.direction = (Link.Direction)int.Parse(parts[2]);
                        this.currentRoomNumber = int.Parse(parts[3]);
                        this.currentStateName = parts[4];
                        this.lastStateName = parts[4];
                        
                        UpdateAnimation();
                    }
				}
			}
			catch (System.Exception ex) {}
		}
		
	public void Update(GameTime gameTime, int currentGameRoom)
	{
		if (replayReader == null)
			return;
		
		if (replayReader.EndOfStream)
		{
			isDone = true;
			return;
		}
		
		try
		{
			string line = replayReader.ReadLine();
			if (string.IsNullOrEmpty(line))
			{
				isDone = true;
				return;
			}
			
			isDone = replayReader.EndOfStream;
			
			string[] parts = line.Split(',');
			if (parts.Length < 5)
				return;
			
			float posX = float.Parse(parts[0]);
			float posY = float.Parse(parts[1]);
			int directionValue = int.Parse(parts[2]);
			int roomNumber = int.Parse(parts[3]);
			string stateName = parts[4];
			
			position = new Vector2(posX, posY);
			direction = (Link.Direction)directionValue;
			currentRoomNumber = roomNumber;
			
			if (stateName != lastStateName)
			{
				lastStateName = stateName;
				currentStateName = stateName;
				animationTime = 0f;
			}
			
			animationTime += (float)gameTime.ElapsedGameTime.Milliseconds;
			if (animationTime > animationDuration)
			{
				animationTime -= animationDuration;
			}
			
			UpdateAnimation();
			linkAnimation.Update(gameTime);
		}
		catch (System.Exception) {}
	}
		
	private void UpdateAnimation()
	{
		switch (currentStateName)
		{
			case "IdleState":
				UpdateIdleAnimation();
				break;
			case "MoveState":
				UpdateMoveAnimation();
				break;
			case "AttackState":
				UpdateAttackAnimation();
				break;
			case "WinState":
				UpdateWinAnimation();
				break;
			case "DamagedState":
				UpdateDamagedAnimation();
				break;
			case "KnockbackState":
				UpdateKnockbackAnimation();
				break;
			case "MagicState":
				UpdateMagicAnimation();
				break;
			case "ItemState":
				UpdateItemAnimation();
				break;
			default:
				UpdateIdleAnimation();
				break;
		}
	}
		
		private void UpdateIdleAnimation()
		{
			switch (direction)
			{
				case Link.Direction.Up:
					linkAnimation.LinkStandingUp();
					break;
				case Link.Direction.Down:
					linkAnimation.LinkStandingDown();
					break;
				case Link.Direction.Left:
					linkAnimation.LinkStandingLeft();
					break;
				case Link.Direction.Right:
					linkAnimation.LinkStandingRight();
					break;
			}
		}
		
		private void UpdateMoveAnimation()
		{
			switch (direction)
			{
				case Link.Direction.Up:
					linkAnimation.LinkWalkingUp(animationDuration, animationTime);
					break;
				case Link.Direction.Down:
					linkAnimation.LinkWalkingDown(animationDuration, animationTime);
					break;
				case Link.Direction.Left:
					linkAnimation.LinkWalkingLeft(animationDuration, animationTime);
					break;
				case Link.Direction.Right:
					linkAnimation.LinkWalkingRight(animationDuration, animationTime);
					break;
			}
		}
		
	private void UpdateAttackAnimation()
	{
		switch (direction)
		{
			case Link.Direction.Up:
				linkAnimation.LinkAttackingUp(animationDuration, animationTime);
				break;
			case Link.Direction.Down:
				linkAnimation.LinkAttackingDown(animationDuration, animationTime);
				break;
			case Link.Direction.Left:
				linkAnimation.LinkAttackingLeft(animationDuration, animationTime);
				break;
			case Link.Direction.Right:
				linkAnimation.LinkAttackingRight(animationDuration, animationTime);
				break;
		}
	}
	
	private void UpdateWinAnimation()
	{
		linkAnimation.LinkHoldingItem();
	}
	
	private void UpdateDamagedAnimation()
	{
		float damageDuration = 750f;
		switch (direction)
		{
			case Link.Direction.Up:
				linkAnimation.LinkDamagedUp(damageDuration, animationTime);
				break;
			case Link.Direction.Down:
				linkAnimation.LinkDamagedDown(damageDuration, animationTime);
				break;
			case Link.Direction.Left:
				linkAnimation.LinkDamagedLeft(damageDuration, animationTime);
				break;
			case Link.Direction.Right:
				linkAnimation.LinkDamagedRight(damageDuration, animationTime);
				break;
		}
	}
	
	private void UpdateKnockbackAnimation()
	{
		UpdateIdleAnimation();
	}
	
	private void UpdateMagicAnimation()
	{
		switch (direction)
		{
			case Link.Direction.Up:
				linkAnimation.LinkMagicUp(animationDuration, animationTime);
				break;
			case Link.Direction.Down:
				linkAnimation.LinkMagicDown(animationDuration, animationTime);
				break;
			case Link.Direction.Left:
				linkAnimation.LinkMagicLeft(animationDuration, animationTime);
				break;
			case Link.Direction.Right:
				linkAnimation.LinkMagicRight(animationDuration, animationTime);
				break;
		}
	}
	
	private void UpdateItemAnimation()
	{
		linkAnimation.LinkHoldingItem();
	}
		
		public void Draw(SpriteBatch spriteBatch, int currentGameRoom)
		{
			if (replayReader == null)
				return;
			
			if (currentRoomNumber != currentGameRoom)
				return;
			
		try
		{
			Color ghostColor = Color.White;
			if (currentStateName == "KnockbackState")
			{
				ghostColor = Color.Red;
			}
			else if (isDone)
			{
				ghostColor = Color.Blue;
			}
			
			linkAnimation.ChangeColor(ghostColor * 0.5f);
			linkAnimation.Draw(spriteBatch, position);
			linkAnimation.ChangeColor(Color.White);
		}
		catch (System.Exception ex) {}
		}
		
	public void Reset()
	{
		isDone = false;
		
		if (replayReader != null)
		{
			replayReader.Close();
			replayReader = null;
		}
		
		try
		{
			if (File.Exists("link_replay.txt"))
			{
				FileStream fileStream = new FileStream("link_replay.txt", FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
				replayReader = new StreamReader(fileStream);

				string firstLine = replayReader.ReadLine();
				if (!string.IsNullOrEmpty(firstLine))
				{
					string[] parts = firstLine.Split(',');

                    this.position = new Vector2(float.Parse(parts[0]), float.Parse(parts[1]));
                    this.direction = (Link.Direction)int.Parse(parts[2]);
                    this.currentRoomNumber = int.Parse(parts[3]);
                    this.currentStateName = parts[4];
                    this.lastStateName = parts[4];
                    
                    animationTime = 0f;
                    UpdateAnimation();
				}
			}
		}
		catch (System.Exception ex) {}
	}
	
	public void Close()
	{
		if (replayReader != null)
		{
			replayReader.Close();
			replayReader = null;
		}
	}
}
}

