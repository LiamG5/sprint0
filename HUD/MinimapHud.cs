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
        private readonly Func<bool> hasCompass;
        private readonly Func<int> getTriforceRoom;
        private readonly Func<int, RoomConnections> getRoomConnections;
        private readonly Vector2 pos;
        private readonly int rows;
        private readonly int cols;
        private readonly int cellWidth;
        private readonly int cellHeight;
        private readonly int? manualStartRow;
        private readonly int? manualEndRow;
        private readonly Texture2D pixelTexture;
        private double flashTimer = 0;
        private const double FlashInterval = 0.5;
        
        private const int RoomWidth = 12;
        private const int RoomHeight = 8;
        private const int RoomSpacing = 2;

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
            int rows = 6, int cols = 6, int cellSize = 14,
            Func<bool> hasCompass = null,
            Func<int> getTriforceRoom = null,
            int? manualStartRow = null,
            int? manualEndRow = null)
        {
            this.getCurrentRoom = getCurrentRoom;
            this.hasMap = hasMap;
            this.hasCompass = hasCompass ?? (() => false);
            this.getTriforceRoom = getTriforceRoom ?? (() => 15);
            this.getRoomConnections = getRoomConnections;
            this.pos = position;
            this.rows = rows;
            this.cols = cols;
            this.cellWidth = cellSize;
            this.cellHeight = cellSize;
            this.manualStartRow = manualStartRow;
            this.manualEndRow = manualEndRow;

            pixelTexture = new Texture2D(graphicsDevice, 1, 1);
            pixelTexture.SetData(new[] { Color.White });
        }

        public void Update(GameTime gameTime)
        {
            if (hasCompass())
            {
                flashTimer += gameTime.ElapsedGameTime.TotalSeconds;
                if (flashTimer >= FlashInterval * 2)
                {
                    flashTimer = 0;
                }
            }
        }

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

        private void GetVisibleRowRange(out int startRow, out int endRow)
        {
            if (manualStartRow.HasValue && manualEndRow.HasValue)
            {
                startRow = manualStartRow.Value;
                endRow = manualEndRow.Value;
                return;
            }

            int currentRoom = getCurrentRoom();
            int currentRoomRow = GetRoomRow(currentRoom);

            if (currentRoomRow < rows / 2)
            {
                startRow = 0;
                endRow = VisibleRows - 1;
            }
            else
            {
                startRow = rows / 2;
                endRow = rows - 1;
            }
        }

        public Rectangle? GetRoomBounds(int roomNum)
        {
            if (!hasMap()) return null;

            GetVisibleRowRange(out int visibleStartRow, out int visibleEndRow);

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

            if (row < visibleStartRow || row > visibleEndRow)
            {
                return null;
            }

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

            for (int row = visibleStartRow; row <= visibleEndRow; row++)
            {
                int visibleRowIndex = row - visibleStartRow;

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

                    Color roomColor;
                    int triforceRoom = getTriforceRoom();
                    
                    if (roomNum == currentRoom)
                    {
                        roomColor = Color.Green;
                    }
                    else if (hasCompass() && roomNum == triforceRoom)
                    {
                        bool isFlashing = (flashTimer < FlashInterval);
                        roomColor = isFlashing ? Color.Red : new Color(0, 200, 255);
                    }
                    else
                    {
                        roomColor = new Color(0, 200, 255);
                    }

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

