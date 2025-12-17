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
        private Dictionary<int, int> roomLevels;
        private HashSet<int> visitedRooms;
        private int currentRoomId;
        private Link link;
        private Game1 game;
        private RoomTransitionManager transitionManager;
        private int pendingRoomId = -1;
        private TransitionDirection pendingDirection = TransitionDirection.None;
        
        public int CurrentRoomId => currentRoomId;
        public RoomTransitionManager TransitionManager => transitionManager;
        
        public RoomManager(Link link, Game1 game, RoomTransitionManager transitionManager)
        {
            this.link = link;
            this.game = game;
            this.transitionManager = transitionManager;
            this.currentRoomId = 2;
            this.visitedRooms = new HashSet<int>();
            this.roomTypes = new Dictionary<int, RoomType>();
            this.roomLevels = new Dictionary<int, int>();
            SetupRoomConnections();
            SetupRoomTypes();
            SetupRoomLevels();
            MarkRoomVisited(2);
        }
        
        public RoomManager(Link link, Game1 game)
        {
            this.link = link;
            this.game = game;
            this.transitionManager = null;
            this.currentRoomId = 2;
            this.visitedRooms = new HashSet<int>();
            this.roomTypes = new Dictionary<int, RoomType>();
            this.roomLevels = new Dictionary<int, int>();
            SetupRoomConnections();
            SetupRoomTypes();
            SetupRoomLevels();
            MarkRoomVisited(2);
        }

        private void SetupRoomConnections()
        {
            roomConnections = new Dictionary<int, RoomConnections>();

            roomConnections[16] = new RoomConnections { North = -1, South = -1, East = 17, West = -1 };
            roomConnections[17] = new RoomConnections { North = -1, South = 13, East = -1, West = 16 };

            roomConnections[13] = new RoomConnections { North = 17, South = 10, East = -1, West = -1 };
            roomConnections[14] = new RoomConnections { North = -1, South = 12, East = 15, West = -1 };
            roomConnections[15] = new RoomConnections { North = -1, South = -1, East = -1, West = 14 };

            roomConnections[8] = new RoomConnections { North = -1, South = -1, East = 9, West = -1 };
            roomConnections[9] = new RoomConnections { North = -1, South = 5, East = 10, West = 8 };
            roomConnections[10] = new RoomConnections { North = 13, South = 6, East = 11, West = 9 };
            roomConnections[11] = new RoomConnections { North = -1, South = 7, East = 12, West = 10 };
            roomConnections[12] = new RoomConnections { North = 14, South = -1, East = -1, West = 11 };

            roomConnections[5] = new RoomConnections { North = 9, South = -1, East = 6, West = -1 };
            roomConnections[6] = new RoomConnections { North = 10, South = 4, East = 7, West = 5 };
            roomConnections[7] = new RoomConnections { North = 11, South = -1, East = -1, West = 6 };

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

        private void SetupRoomLevels()
        {
            for (int i = 1; i <= 9; i++)
            {
                roomLevels[i] = 1;
            }
            for (int i = 10; i <= 17; i++)
            {
                roomLevels[i] = 2;
            }
        }

        public int GetRoomLevel(int roomId)
        {
            return roomLevels.ContainsKey(roomId) ? roomLevels[roomId] : 1;
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
        
        public void TransitionToRoom(TransitionDirection direction)
        {
            int newRoomId = GetConnectedRoom(currentRoomId, direction);
            
            if (newRoomId == -1)
            {
                System.Console.WriteLine($"[RoomManager] No connection from Room {currentRoomId} in direction {direction}");
                return;
            }
            
            if (newRoomId < 1 || newRoomId > 18)
            {
                System.Console.WriteLine($"[RoomManager] Invalid room ID: {newRoomId}");
                return;
            }
            
            if (transitionManager != null)
            {
                if (transitionManager.IsTransitioning)
                    return;
                
                pendingRoomId = newRoomId;
                pendingDirection = direction;
                
                transitionManager.StartTransition(PerformRoomChange, direction);
                
                System.Console.WriteLine($"[RoomManager] Starting {direction} slide transition from Room {currentRoomId} to Room {newRoomId}");
            }
            else
            {
                pendingRoomId = newRoomId;
                pendingDirection = direction;
                PerformRoomChange();
                System.Console.WriteLine($"[RoomManager] Instant transition from Room {currentRoomId} to Room {newRoomId} via {direction}");
            }
        }
        
        private void PerformRoomChange()
        {
            if (pendingRoomId == -1) return;
            
            int oldRoomId = currentRoomId;
            currentRoomId = pendingRoomId;
            
            game.GoToRoom(currentRoomId);
            
            RepositionLink(pendingDirection);
            
            System.Console.WriteLine($"[RoomManager] Room changed from {oldRoomId} to {currentRoomId}");
            
            pendingRoomId = -1;
            pendingDirection = TransitionDirection.None;
        }
        
        private void RepositionLink(TransitionDirection direction)
        {
            Vector2 newPosition = direction switch
            {
                TransitionDirection.North => new Vector2(360, 406),
                TransitionDirection.South => new Vector2(360, 96),
                TransitionDirection.East => new Vector2(96, 240),
                TransitionDirection.West => new Vector2(620, 240),
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