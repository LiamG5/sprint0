using System.Collections.Generic;
using Microsoft.Xna.Framework;
using sprint0.Interfaces;

namespace sprint0.Classes
{
	public class LinkReplayRecorder
	{
		private List<string> replayData;

		public LinkReplayRecorder()
		{
			replayData = new List<string>();
		}

		public void RecordFrame(Vector2 position, Link.Direction direction, int roomNumber, string stateName)
		{
			try
			{
				replayData.Add($"{position.X},{position.Y},{(int)direction},{roomNumber},{stateName}");
			}
			catch (System.Exception) { }
		}

		public void SaveReplay()
		{
			try
			{
				System.IO.File.WriteAllLines("link_replay.txt", replayData);
				replayData.Clear();
			}
			catch (System.Exception) { }
		}
	}
}

