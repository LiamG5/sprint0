using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using sprint0.Sprites;
using sprint0.Managers;

namespace sprint0.HUD
{
    public class MinimapHud : IHudElement
    {
        private readonly Func<int> getCurrentRoom;
        private readonly Func<bool> hasMap;
        private readonly Func<int, RoomConnections> getRoomConnections;
        private readonly Vector2 pos;
        private readonly int rows;
        private readonly int cols;
        private readonly int cellWidth;
        private readonly int cellHeight;
        private readonly Texture2D pixelTexture;
        
        // Room rectangles are wider than tall (like in the picture)
        private const int RoomWidth = 16;  // Wider rectangles
        private const int RoomHeight = 10; // Shorter height
        private const int RoomSpacing = 4; // Gap between rooms (increased for more separation)
        
        // Room layout: 3 rows x 6 cols
        // Row 1: Rooms 1-6
        // Row 2: Rooms 7-12
        // Row 3: Rooms 13-17 (only 5 rooms)
        private static readonly int[] RoomLayout = new int[]
        {
            1, 2, 3, 4, 5, 6,    // Row 1
            7, 8, 9, 10, 11, 12, // Row 2
            13, 14, 15, 16, 17, -1 // Row 3 (last cell empty)
        };
        
        // Helper to get room position from room number
        private static (int row, int col) GetRoomPosition(int roomNum)
        {
            for (int i = 0; i < RoomLayout.Length; i++)
            {
                if (RoomLayout[i] == roomNum)
                {
                    int row = i / 6;
                    int col = i % 6;
                    return (row, col);
                }
            }
            return (-1, -1);
        }

        public MinimapHud(Func<int> getCurrentRoom, Func<bool> hasMap, Func<int, RoomConnections> getRoomConnections, Vector2 position, GraphicsDevice graphicsDevice, int rows = 3, int cols = 6, int cellSize = 8)
        {
            this.getCurrentRoom = getCurrentRoom;
            this.hasMap = hasMap;
            this.getRoomConnections = getRoomConnections;
            this.pos = position;
            this.rows = rows;
            this.cols = cols;
            this.cellWidth = cellSize;
            this.cellHeight = cellSize;
            
            // Create a simple pixel texture for drawing
            pixelTexture = new Texture2D(graphicsDevice, 1, 1);
            pixelTexture.SetData(new[] { Color.White });
        }

        public void Update(GameTime gameTime) { }
        
        // Get the clickable rectangle for a specific room
        public Rectangle? GetRoomBounds(int roomNum)
        {
            if (!hasMap()) return null;
            
            for (int i = 0; i < RoomLayout.Length; i++)
            {
                if (RoomLayout[i] == roomNum)
                {
                    int row = i / 6;
                    int col = i % 6;
                    
                    var cellPos = new Vector2(
                        pos.X + col * cellWidth,
                        pos.Y + row * cellHeight
                    );
                    
                    return new Rectangle(
                        (int)cellPos.X + (cellWidth - RoomWidth) / 2 + RoomSpacing / 2,
                        (int)cellPos.Y + (cellHeight - RoomHeight) / 2 + RoomSpacing / 2,
                        RoomWidth - RoomSpacing,
                        RoomHeight - RoomSpacing
                    );
                }
            }
            return null;
        }
        
        // Check if a point is on a room and return the room number
        public int? GetRoomAtPoint(Point point)
        {
            if (!hasMap()) return null;
            
            for (int row = 0; row < rows; row++)
            {
                for (int col = 0; col < cols; col++)
                {
                    int idx = row * cols + col;
                    if (idx >= RoomLayout.Length) continue;
                    
                    int roomNum = RoomLayout[idx];
                    if (roomNum == -1) continue;
                    
                    var bounds = GetRoomBounds(roomNum);
                    if (bounds.HasValue && bounds.Value.Contains(point))
                    {
                        return roomNum;
                    }
                }
            }
            return null;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            // Only draw minimap if player has the map item
            if (!hasMap()) return;
            
            int currentRoom = getCurrentRoom();
            
            // First pass: Draw all rooms as blue rectangles
            for (int row = 0; row < rows; row++)
            {
                for (int col = 0; col < cols; col++)
                {
                    int idx = row * cols + col;
                    if (idx >= RoomLayout.Length) continue;
                    
                    int roomNum = RoomLayout[idx];
                    if (roomNum == -1) continue; // Empty cell
                    
                    var cellPos = new Vector2(
                        pos.X + col * cellWidth,
                        pos.Y + row * cellHeight
                    );
                    
                    // Draw room as a small blue rectangle (wider than tall) with spacing
                    var roomRect = new Rectangle(
                        (int)cellPos.X + (cellWidth - RoomWidth) / 2 + RoomSpacing / 2, // Add spacing
                        (int)cellPos.Y + (cellHeight - RoomHeight) / 2 + RoomSpacing / 2,
                        RoomWidth - RoomSpacing, // Reduce size to create gap
                        RoomHeight - RoomSpacing
                    );
                    
                    // Draw blue rectangle for room
                    Color roomColor = (roomNum == currentRoom) ? Color.Green : new Color(0, 200, 255);
                    spriteBatch.Draw(pixelTexture, roomRect, roomColor);
                }
            }
            
            // Connections removed - rooms are now separate with gaps
        }
        
        private void DrawLine(SpriteBatch spriteBatch, Vector2 start, Vector2 end, Color color)
        {
            Vector2 direction = end - start;
            float distance = direction.Length();
            if (distance < 0.1f) return; // Skip if too short
            
            direction.Normalize();
            
            // Draw line by drawing small rectangles along the path
            int steps = (int)distance;
            for (int i = 0; i <= steps; i++)
            {
                Vector2 pos = start + direction * i;
                Rectangle pixelRect = new Rectangle((int)pos.X, (int)pos.Y, 1, 1);
                spriteBatch.Draw(pixelTexture, pixelRect, color);
            }
        }
    }
}

