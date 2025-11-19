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
        
        private const int RoomWidth = 12;
        private const int RoomHeight = 8;
        private const int RoomSpacing = 2;

        // We still treat the logical layout as 6x6, but will only show
        // 3 visible rows at a time (top half or bottom half).
        private const int VisibleRows = 3;

        private static readonly int[] RoomLayout = new int[]
        {
           -1, 16, 17, -1, -1, -1,
           -1, -1, 13, -1, 14, 15,
            8,  9, 10, 11, 12, -1,
           -1,  5,  6,  7, -1, -1,
           -1, -1,  4, -1, -1, -1,
           -1,  1,  2,  3, -1, -1
        };

        public MinimapHud(
            Func<int> getCurrentRoom,
            Func<bool> hasMap,
            Func<int, RoomConnections> getRoomConnections,
            Vector2 position,
            GraphicsDevice graphicsDevice,
            int rows = 6, int cols = 6, int cellSize = 14)
        {
            this.getCurrentRoom = getCurrentRoom;
            this.hasMap = hasMap;
            this.getRoomConnections = getRoomConnections;
            this.pos = position;
            this.rows = rows;
            this.cols = cols;
            this.cellWidth = cellSize;
            this.cellHeight = cellSize;

            pixelTexture = new Texture2D(graphicsDevice, 1, 1);
            pixelTexture.SetData(new[] { Color.White });
        }

        public void Update(GameTime gameTime) { }

        /// <summary>
        /// Get the logical row (0-rows-1) where a room lives in the RoomLayout.
        /// Returns 0 if not found.
        /// </summary>
        private int GetRoomRow(int roomNum)
        {
            for (int i = 0; i < RoomLayout.Length; i++)
            {
                if (RoomLayout[i] == roomNum)
                {
                    return i / cols;
                }
            }
            return 0;
        }

        /// <summary>
        /// Decide which 3-row window (0-2 or 3-5) to show based on the
        /// current room's row.
        /// </summary>
        private void GetVisibleRowRange(out int startRow, out int endRow)
        {
            int currentRoom = getCurrentRoom();
            int currentRoomRow = GetRoomRow(currentRoom);

            // Top half if the current room is in rows 0-2; bottom half otherwise.
            if (currentRoomRow < rows / 2)
            {
                startRow = 0;
                endRow = VisibleRows - 1;          // 0,1,2
            }
            else
            {
                startRow = rows / 2;               // 3
                endRow = rows - 1;                 // 5
            }
        }

        public Rectangle? GetRoomBounds(int roomNum)
        {
            if (!hasMap()) return null;

            GetVisibleRowRange(out int visibleStartRow, out int visibleEndRow);

            // Find the logical row/col for this room
            int foundIndex = -1;
            for (int i = 0; i < RoomLayout.Length; i++)
            {
                if (RoomLayout[i] == roomNum)
                {
                    foundIndex = i;
                    break;
                }
            }

            if (foundIndex == -1) return null;

            int row = foundIndex / cols;
            int col = foundIndex % cols;

            // If this room is not in the currently visible half, return null
            if (row < visibleStartRow || row > visibleEndRow)
            {
                return null;
            }

            // Convert the logical row into the visible row index (0..VisibleRows-1)
            int visibleRowIndex = row - visibleStartRow;

            var cellPos = new Vector2(
                pos.X + col * cellWidth,
                pos.Y + visibleRowIndex * cellHeight
            );

            int rectWidth = Math.Max(1, RoomWidth - RoomSpacing);
            int rectHeight = Math.Max(1, RoomHeight - RoomSpacing);

            return new Rectangle(
                (int)cellPos.X + (cellWidth - RoomWidth) / 2,
                (int)cellPos.Y + (cellHeight - RoomHeight) / 2,
                rectWidth,
                rectHeight
            );
        }

        public int? GetRoomAtPoint(Point point)
        {
            if (!hasMap()) return null;

            GetVisibleRowRange(out int visibleStartRow, out int visibleEndRow);

            // Only check rooms in the currently visible half
            for (int row = visibleStartRow; row <= visibleEndRow; row++)
            {
                int visibleRowIndex = row - visibleStartRow;

                for (int col = 0; col < cols; col++)
                {
                    int idx = row * cols + col;
                    if (idx >= RoomLayout.Length) continue;

                    int roomNum = RoomLayout[idx];
                    if (roomNum == -1) continue;

                    // Bounds uses the same visible-row mapping
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
            if (!hasMap())
            {
                System.Console.WriteLine("[MinimapHud] Not drawing - hasMap() returned false");
                return;
            }

            int currentRoom = getCurrentRoom();
            System.Console.WriteLine($"[MinimapHud] Drawing minimap - Current Room: {currentRoom}");

            GetVisibleRowRange(out int visibleStartRow, out int visibleEndRow);

            int roomsDrawn = 0;
            for (int row = visibleStartRow; row <= visibleEndRow; row++)
            {
                int visibleRowIndex = row - visibleStartRow;

                for (int col = 0; col < cols; col++)
                {
                    int idx = row * cols + col;
                    if (idx >= RoomLayout.Length) continue;

                    int roomNum = RoomLayout[idx];
                    if (roomNum == -1) continue;

                    var cellPos = new Vector2(
                        pos.X + col * cellWidth,
                        pos.Y + visibleRowIndex * cellHeight
                    );

                    int rectWidth = Math.Max(1, RoomWidth - RoomSpacing);
                    int rectHeight = Math.Max(1, RoomHeight - RoomSpacing);

                    var roomRect = new Rectangle(
                        (int)cellPos.X + (cellWidth - RoomWidth) / 2,
                        (int)cellPos.Y + (cellHeight - RoomHeight) / 2,
                        rectWidth,
                        rectHeight
                    );

                    Color roomColor = (roomNum == currentRoom)
                        ? Color.Green
                        : new Color(0, 200, 255);

                    spriteBatch.Draw(pixelTexture, roomRect, roomColor);
                    roomsDrawn++;
                }
            }

            System.Console.WriteLine($"[MinimapHud] Drew {roomsDrawn} rooms (visible window rows {visibleStartRow}-{visibleEndRow})");
        }

        private void DrawLine(SpriteBatch spriteBatch, Vector2 start, Vector2 end, Color color)
        {
            Vector2 direction = end - start;
            float distance = direction.Length();
            if (distance < 0.1f) return;

            direction.Normalize();

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

