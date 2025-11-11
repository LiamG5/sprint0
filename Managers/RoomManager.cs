using Microsoft.Xna.Framework;
using sprint0.Classes;
using sprint0.Sprites;
using System.Collections.Generic;

namespace sprint0.Managers
{
    public class RoomManager
    {
        private Dictionary<int, RoomConnections> roomConnections;
        private Dictionary<int, RoomType> roomTypes;
        private HashSet<int> visitedRooms;
        private int currentRoomId;
        private Link link;
        private Game1 game;
        
        public int CurrentRoomId => currentRoomId;
        
        public RoomManager(Link link, Game1 game)
        {
            this.link = link;
            this.game = game;
            this.currentRoomId = 1;
            this.visitedRooms = new HashSet<int>();
            this.roomTypes = new Dictionary<int, RoomType>();
            SetupRoomConnections();
            SetupRoomTypes();
            MarkRoomVisited(1);
        }

        private void SetupRoomConnections()
        {
            roomConnections = new Dictionary<int, RoomConnections>();

            // Row 1
            roomConnections[16] = new RoomConnections { North = -1, South = -1, East = 17, West = -1 };
            roomConnections[17] = new RoomConnections { North = -1, South = 13, East = -1, West = 16 };

            // Row 2
            roomConnections[13] = new RoomConnections { North = 17, South = 10, East = -1, West = -1 };
            roomConnections[14] = new RoomConnections { North = -1, South = 12, East = 15, West = -1 };
            roomConnections[15] = new RoomConnections { North = -1, South = -1, East = -1, West = 14 };

            // Row 3
            roomConnections[8] = new RoomConnections { North = -1, South = -1, East = 9, West = -1 };
            roomConnections[9] = new RoomConnections { North = -1, South = 5, East = 10, West = 8 };
            roomConnections[10] = new RoomConnections { North = 13, South = 4, East = 11, West = 9 };
            roomConnections[11] = new RoomConnections { North = -1, South = 7, East = 12, West = 10 };
            roomConnections[12] = new RoomConnections { North = 14, South = -1, East = -1, West = 11 };

            // Row 4
            roomConnections[5] = new RoomConnections { North = 9, South = -1, East = 6, West = -1 };
            roomConnections[6] = new RoomConnections { North = 10, South = 4, East = 7, West = 5 };
            roomConnections[7] = new RoomConnections { North = 11, South = -1, East = -1, West = 6 };

            // Row 5
            roomConnections[4] = new RoomConnections { North = 6, South = 2, East = -1, West = -1 };

            roomConnections[1] = new RoomConnections { North = -1, South = -1, East = 2, West = -1 };
            roomConnections[2] = new RoomConnections { North = 4, South = -1, East = 3, West = 1 };
            roomConnections[3] = new RoomConnections { North = -1, South = -1, East = -1, West = 2 };
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
        
        public bool HasConnection(int fromRoomId, TransitionDirection direction)
        {
            return GetConnectedRoom(fromRoomId, direction) != -1;
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
            MarkRoomVisited(roomId);
        }
        
        private void SetupRoomTypes()
        {
            for (int i = 1; i <= 17; i++)
            {
                roomTypes[i] = RoomType.Regular;
            }
        }
        
        public void MarkRoomVisited(int roomId)
        {
            if (roomId >= 1 && roomId <= 17)
            {
                visitedRooms.Add(roomId);
            }
        }
        
        public bool IsRoomVisited(int roomId)
        {
            return visitedRooms.Contains(roomId);
        }
        
        public RoomType GetRoomType(int roomId)
        {
            return roomTypes.ContainsKey(roomId) ? roomTypes[roomId] : RoomType.Regular;
        }
        
        public HashSet<int> GetVisitedRegularRooms()
        {
            var result = new HashSet<int>();
            foreach (var roomId in visitedRooms)
            {
                if (GetRoomType(roomId) == RoomType.Regular)
                {
                    result.Add(roomId);
                }
            }
            return result;
        }
        
        public HashSet<int> GetVisitedRoomsForMinimap()
        {
            var result = new HashSet<int>();
            foreach (var roomId in visitedRooms)
            {
                var type = GetRoomType(roomId);
                if (type == RoomType.Regular || type == RoomType.Secret)
                {
                    result.Add(roomId);
                }
            }
            return result;
        }
        
        public void TransitionToRoom(int newRoomId, TransitionDirection direction)
        {
            // Validate the transition is allowed
            if (newRoomId < 1 || newRoomId > 17)
                return;
            
            // Check if this transition is valid from the current room
            int expectedRoomId = GetConnectedRoom(currentRoomId, direction);
            if (expectedRoomId != newRoomId)
            {
                System.Console.WriteLine($"[RoomManager] Invalid transition: Room {currentRoomId} -> {newRoomId} via {direction}. Expected: {expectedRoomId}");
                return;
            }
            
            currentRoomId = newRoomId;
            
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
            
            RepositionLink(direction);
        }
        
        private void RepositionLink(TransitionDirection direction)
        {
            Vector2 newPosition = direction switch
            {
                TransitionDirection.North => new Vector2(384, 400),  // Enter from bottom
                TransitionDirection.South => new Vector2(384, 130),  // Enter from top
                TransitionDirection.East => new Vector2(130, 240),   // Enter from left
                TransitionDirection.West => new Vector2(600, 240),   // Enter from right
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
    
    public enum RoomType
    {
        Regular,
        Secret,
        SuperSecret
    }
}