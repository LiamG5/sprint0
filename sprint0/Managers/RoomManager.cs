using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using sprint0.Classes;
using sprint0.Sprites;
using System.Collections.Generic;
using System.IO;

namespace sprint0.Managers
{
    public class RoomManager
    {
        private Dictionary<int, string> roomPaths;
        private int currentRoomId;
        private DungeonLoader currentDungeon;
        private BlockFactory blocks;
        private Link link;
        
        public int CurrentRoomId => currentRoomId;
        
        public RoomManager(BlockFactory blockFactory, Link link)
        {
            this.blocks = blockFactory;
            this.link = link;
            this.currentRoomId = 1;
            
            roomPaths = new Dictionary<int, string>();
            for (int i = 1; i <= 17; i++)
            {
                roomPaths[i] = $"Dungeon/Room{i}.csv";
            }
        }
        
        public DungeonLoader LoadRoom(int roomId, string contentRoot)
        {
            if (!roomPaths.ContainsKey(roomId))
            {
                return currentDungeon;
            }
            
            currentRoomId = roomId;
            string roomPath = Path.Combine(contentRoot, roomPaths[roomId]);
            string csvContent = File.ReadAllText(roomPath);
            
            currentDungeon = new DungeonLoader(blocks, csvContent, roomId);
            currentDungeon.LoadRectangles();
            
            return currentDungeon;
        }
        
        public void TransitionToRoom(int newRoomId, TransitionDirection direction, string contentRoot)
        {
            if (newRoomId < 1 || newRoomId > 17)
                return;
            
            LoadRoom(newRoomId, contentRoot);
            currentDungeon.SetRoomManager(this, contentRoot);
            RepositionLink(direction);
        }
        
        private void RepositionLink(TransitionDirection direction)
        {
            Vector2 newPosition = direction switch
            {
                TransitionDirection.North => new Vector2(384, 420),
                TransitionDirection.South => new Vector2(384, 120),
                TransitionDirection.East => new Vector2(120, 240),
                TransitionDirection.West => new Vector2(620, 240),
                _ => new Vector2(384, 240)
            };
            
            link.position = newPosition;
            link.velocity = Vector2.Zero;
        }
        
        public DungeonLoader GetCurrentDungeon()
        {
            return currentDungeon;
        }
    }
    
    public enum TransitionDirection
    {
        North,
        South,
        East,
        West,
        None
    }
}