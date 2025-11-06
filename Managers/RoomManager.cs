using Microsoft.Xna.Framework;
using sprint0.Classes;
using sprint0.Sprites;
using System.Collections.Generic;

namespace sprint0.Managers
{
    public class RoomManager
    {
        private Dictionary<int, RoomConnections> roomConnections;
        private int currentRoomId;
        private Link link;
        private Game1 game;
        
        public int CurrentRoomId => currentRoomId;
        
        public RoomManager(Link link, Game1 game)
        {
            this.link = link;
            this.game = game;
            this.currentRoomId = 1;
            SetupRoomConnections();
        }
        
        private void SetupRoomConnections()
        {
            roomConnections = new Dictionary<int, RoomConnections>();
            
            // Row 1: Rooms 1-6
            roomConnections[1] = new RoomConnections { North = -1, South = 7, East = 2, West = -1 };
            roomConnections[2] = new RoomConnections { North = -1, South = 8, East = 3, West = 1 };
            roomConnections[3] = new RoomConnections { North = -1, South = 9, East = 4, West = 2 };
            roomConnections[4] = new RoomConnections { North = -1, South = 10, East = 5, West = 3 };
            roomConnections[5] = new RoomConnections { North = -1, South = 11, East = 6, West = 4 };
            roomConnections[6] = new RoomConnections { North = -1, South = 12, East = -1, West = 5 };
            
            // Row 2: Rooms 7-12
            roomConnections[7] = new RoomConnections { North = 1, South = 13, East = 8, West = -1 };
            roomConnections[8] = new RoomConnections { North = 2, South = 14, East = 9, West = 7 };
            roomConnections[9] = new RoomConnections { North = 3, South = 15, East = 10, West = 8 };
            roomConnections[10] = new RoomConnections { North = 4, South = 16, East = 11, West = 9 };
            roomConnections[11] = new RoomConnections { North = 5, South = 17, East = 12, West = 10 };
            roomConnections[12] = new RoomConnections { North = 6, South = -1, East = -1, West = 11 };
            
            // Row 3: Rooms 13-17
            roomConnections[13] = new RoomConnections { North = 7, South = -1, East = 14, West = -1 };
            roomConnections[14] = new RoomConnections { North = 8, South = -1, East = 15, West = 13 };
            roomConnections[15] = new RoomConnections { North = 9, South = -1, East = 16, West = 14 };
            roomConnections[16] = new RoomConnections { North = 10, South = -1, East = 17, West = 15 };
            roomConnections[17] = new RoomConnections { North = 11, South = -1, East = -1, West = 16 };
        }
        
        public int GetConnectedRoom(int fromRoomId, TransitionDirection direction)
        {
            if (!roomConnections.ContainsKey(fromRoomId))
                return -1;
            
            RoomConnections connections = roomConnections[fromRoomId];
            
            return direction switch
            {
                TransitionDirection.North => connections.North,
                TransitionDirection.South => connections.South,
                TransitionDirection.East => connections.East,
                TransitionDirection.West => connections.West,
                _ => -1
            };
        }
        
        public RoomConnections GetRoomConnections(int roomId)
        {
            if (roomConnections.ContainsKey(roomId))
                return roomConnections[roomId];
            return null;
        }
        
        public void SetCurrentRoom(int roomId)
        {
            currentRoomId = roomId;
        }
        
        public void TransitionToRoom(int newRoomId, TransitionDirection direction)
        {
            if (newRoomId < 1 || newRoomId > 17)
                return;
            
            currentRoomId = newRoomId;
            
            // Call the existing GoToRoom methods in Game1
            switch (newRoomId)
            {
                case 1: game.GoToRoom1(); break;
                case 2: game.GoToRoom2(); break;
                case 3: game.GoToRoom3(); break;
                case 4: game.GoToRoom4(); break;
                case 5: game.GoToRoom5(); break;
                case 6: game.GoToRoom6(); break;
                case 7: game.GoToRoom7(); break;
                case 8: game.GoToRoom8(); break;
                case 9: game.GoToRoom9(); break;
                case 10: game.GoToRoom10(); break;
                case 11: game.GoToRoom11(); break;
                case 12: game.GoToRoom12(); break;
                case 13: game.GoToRoom13(); break;
                case 14: game.GoToRoom14(); break;
                case 15: game.GoToRoom15(); break;
                case 16: game.GoToRoom16(); break;
                case 17: game.GoToRoom17(); break;
            }
            
            // Reposition Link after the room loads
            RepositionLink(direction);
        }
        
        private void RepositionLink(TransitionDirection direction)
        {
            Vector2 newPosition = direction switch
            {
                TransitionDirection.North => new Vector2(384, 400),  // Coming from north, spawn at bottom
                TransitionDirection.South => new Vector2(384, 130),  // Coming from south, spawn at top
                TransitionDirection.East => new Vector2(130, 240),   // Coming from east, spawn at left
                TransitionDirection.West => new Vector2(600, 240),   // Coming from west, spawn at right
                _ => link.position
            };
            
            if (direction != TransitionDirection.None)
            {
                link.position = newPosition;
                link.velocity = Vector2.Zero;
            }
        }
    }
    
    public class RoomConnections
    {
        public int North { get; set; }
        public int South { get; set; }
        public int East { get; set; }
        public int West { get; set; }
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