using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using sprint0.Sprites;
using sprint0.Managers;
using static sprint0.Sprites.ItemFactory;

namespace sprint0.HUD
{
    public class InventoryMenu : IHudElement
    {
        private readonly Func<List<ItemType>> getInventoryItems;
        private readonly Func<ItemType> getItemInSlotB;
        private readonly Action<ItemType> setItemInSlotB;
        private readonly Func<int> getCurrentSelection;
        private readonly Action<int> setCurrentSelection;
        private readonly Func<RoomManager> getRoomManager;
        private readonly Func<int> getHearts;
        private readonly Func<int> getMaxHearts;
        private readonly Func<int> getRupees;
        private readonly Func<int> getKeys;
        private readonly Func<int> getBombs;
        private readonly Texture2D slotBg;
        private readonly Texture2D pixelTexture;
        private readonly SpriteFont font;
        private readonly GraphicsDevice graphicsDevice;
        
        private const int MenuPadding = 20;
        private const int SlotSize = 32;
        private const int SlotSpacing = 4;
        private const int InventoryRows = 2;
        private const int InventoryCols = 4;
        private const int MapWidth = 200;
        private const int MapHeight = 150;
        
        public InventoryMenu(
            Func<List<ItemType>> getInventoryItems,
            Func<ItemType> getItemInSlotB,
            Action<ItemType> setItemInSlotB,
            Func<int> getCurrentSelection,
            Action<int> setCurrentSelection,
            Func<RoomManager> getRoomManager,
            Func<int> getHearts,
            Func<int> getMaxHearts,
            Func<int> getRupees,
            Func<int> getKeys,
            Func<int> getBombs,
            SpriteFont font,
            GraphicsDevice graphicsDevice)
        {
            this.getInventoryItems = getInventoryItems;
            this.getItemInSlotB = getItemInSlotB;
            this.setItemInSlotB = setItemInSlotB;
            this.getCurrentSelection = getCurrentSelection;
            this.setCurrentSelection = setCurrentSelection;
            this.getRoomManager = getRoomManager;
            this.getHearts = getHearts;
            this.getMaxHearts = getMaxHearts;
            this.getRupees = getRupees;
            this.getKeys = getKeys;
            this.getBombs = getBombs;
            this.font = font;
            this.graphicsDevice = graphicsDevice;
            
            slotBg = Texture2DStorage.GetTexture("hud_slot_bg");
            
            pixelTexture = new Texture2D(graphicsDevice, 1, 1);
            pixelTexture.SetData(new[] { Color.White });
        }
        
        public void Update(GameTime gameTime)
        {
            // Navigation is handled in Game1
        }
        
        public void Draw(SpriteBatch spriteBatch)
        {
            var viewport = graphicsDevice.Viewport;
            var screenWidth = viewport.Width;
            var screenHeight = viewport.Height;
            
            DrawRectangle(spriteBatch, new Rectangle(0, 0, screenWidth, screenHeight), Color.Black * 0.7f);
            
            var topLeft = new Vector2(MenuPadding, MenuPadding);
            
            var inventoryLabelPos = topLeft;
            spriteBatch.DrawString(font, "INVENTORY", inventoryLabelPos, Color.Orange, 0f, Vector2.Zero, 1.2f, SpriteEffects.None, 0f);

            var bSlotPos = topLeft + new Vector2(0, 40);
            var bSlotRect = new Rectangle((int)bSlotPos.X, (int)bSlotPos.Y, SlotSize, SlotSize);
            DrawSlot(spriteBatch, bSlotRect, getItemInSlotB());
            
            var bSlotTextPos = bSlotPos + new Vector2(SlotSize + 8, 0);
            spriteBatch.DrawString(font, "USE B BUTTON", bSlotTextPos, Color.White, 0f, Vector2.Zero, 0.8f, SpriteEffects.None, 0f);
            spriteBatch.DrawString(font, "FOR THIS", bSlotTextPos + new Vector2(0, 16), Color.White, 0f, Vector2.Zero, 0.8f, SpriteEffects.None, 0f);
            
            var inventoryStartPos = topLeft + new Vector2(screenWidth - MenuPadding - (InventoryCols * (SlotSize + SlotSpacing)), 40);
            var inventoryItems = getInventoryItems();
            int selectedIndex = getCurrentSelection();
            
            for (int row = 0; row < InventoryRows; row++)
            {
                for (int col = 0; col < InventoryCols; col++)
                {
                    int index = row * InventoryCols + col;
                    var slotPos = inventoryStartPos + new Vector2(col * (SlotSize + SlotSpacing), row * (SlotSize + SlotSpacing));
                    var slotRect = new Rectangle((int)slotPos.X, (int)slotPos.Y, SlotSize, SlotSize);
                    
                    ItemType? item = index < inventoryItems.Count ? inventoryItems[index] : (ItemType?)null;
                    DrawSlot(spriteBatch, slotRect, item);
                    
                    if (index == selectedIndex)
                    {
                        DrawRectangleOutline(spriteBatch, slotRect, Color.Yellow, 2);
                    }
                }
            }
            
            var mapPos = topLeft + new Vector2(screenWidth - MenuPadding - MapWidth, 120);
            DrawVisitedRoomsMap(spriteBatch, mapPos, MapWidth, MapHeight);
            
            var mapLabelPos = topLeft + new Vector2(0, 120);
            spriteBatch.DrawString(font, "MAP", mapLabelPos, Color.Orange, 0f, Vector2.Zero, 1.0f, SpriteEffects.None, 0f);
            
            var compassLabelPos = mapLabelPos + new Vector2(0, 40);
            spriteBatch.DrawString(font, "COMPASS", compassLabelPos, Color.Orange, 0f, Vector2.Zero, 1.0f, SpriteEffects.None, 0f);
            
            var bottomY = screenHeight - 80;
            var bottomLeft = new Vector2(MenuPadding, bottomY);
            
            spriteBatch.DrawString(font, "LEVEL-1", bottomLeft, Color.White, 0f, Vector2.Zero, 1.0f, SpriteEffects.None, 0f);
            
            var itemCountsPos = bottomLeft + new Vector2(120, 0);
            var rupeeIcon = Texture2DStorage.GetTexture("icon_rupee");
            var keyIcon = Texture2DStorage.GetTexture("icon_key");
            var bombIcon = Texture2DStorage.GetTexture("icon_bomb");
            
            if (rupeeIcon != null)
            {
                spriteBatch.Draw(rupeeIcon, new Rectangle((int)itemCountsPos.X, (int)itemCountsPos.Y, 16, 16), Color.White);
                spriteBatch.DrawString(font, "X" + getRupees(), itemCountsPos + new Vector2(20, 0), Color.White, 0f, Vector2.Zero, 1.0f, SpriteEffects.None, 0f);
            }
            
            if (keyIcon != null)
            {
                spriteBatch.Draw(keyIcon, new Rectangle((int)itemCountsPos.X, (int)(itemCountsPos.Y + 20), 16, 16), Color.White);
                spriteBatch.DrawString(font, "X" + getKeys(), itemCountsPos + new Vector2(20, 20), Color.White, 0f, Vector2.Zero, 1.0f, SpriteEffects.None, 0f);
            }
            
            if (bombIcon != null)
            {
                spriteBatch.Draw(bombIcon, new Rectangle((int)itemCountsPos.X, (int)(itemCountsPos.Y + 40), 16, 16), Color.White);
                spriteBatch.DrawString(font, "X" + getBombs(), itemCountsPos + new Vector2(20, 40), Color.White, 0f, Vector2.Zero, 1.0f, SpriteEffects.None, 0f);
            }
            
            var slotBPos = bottomLeft + new Vector2(280, 0);
            var slotAPos = slotBPos + new Vector2(SlotSize + 8, 0);
            var slotBRect = new Rectangle((int)slotBPos.X, (int)slotBPos.Y, SlotSize, SlotSize);
            var slotARect = new Rectangle((int)slotAPos.X, (int)slotAPos.Y, SlotSize, SlotSize);
            
            DrawSlot(spriteBatch, slotBRect, getItemInSlotB());
            DrawSlot(spriteBatch, slotARect, ItemType.Sword);
            
            spriteBatch.DrawString(font, "B", slotBPos + new Vector2(0, -16), Color.White, 0f, Vector2.Zero, 1.0f, SpriteEffects.None, 0f);
            spriteBatch.DrawString(font, "A", slotAPos + new Vector2(0, -16), Color.White, 0f, Vector2.Zero, 1.0f, SpriteEffects.None, 0f);
            
            var lifeLabelPos = bottomLeft + new Vector2(screenWidth - MenuPadding - 200, 0);
            spriteBatch.DrawString(font, "-LIFE-", lifeLabelPos, Color.White, 0f, Vector2.Zero, 1.0f, SpriteEffects.None, 0f);
            
            var heartsPos = lifeLabelPos + new Vector2(0, 20);
            var fullHeart = Texture2DStorage.GetTexture("hud_heart_full");
            var emptyHeart = Texture2DStorage.GetTexture("hud_heart_empty");
            int hearts = getHearts();
            int maxHearts = getMaxHearts();
            
            if (fullHeart != null && emptyHeart != null)
            {
                for (int i = 0; i < maxHearts; i++)
                {
                    var heartRect = new Rectangle((int)heartsPos.X + i * 20, (int)heartsPos.Y, 18, 18);
                    var heartTex = (i < hearts) ? fullHeart : emptyHeart;
                    var heartColor = (i < hearts) ? Color.Red : Color.DarkGray;
                    spriteBatch.Draw(heartTex, heartRect, heartColor);
                }
            }
            
            DrawRectangle(spriteBatch, new Rectangle(0, screenHeight - 4, screenWidth, 4), Color.Blue);
        }
        
        private void DrawSlot(SpriteBatch spriteBatch, Rectangle rect, ItemType? item)
        {
            if (slotBg != null)
            {
                spriteBatch.Draw(slotBg, rect, Color.Blue * 0.35f);
                DrawRectangleOutline(spriteBatch, rect, Color.White, 2);
            }
            
            if (item.HasValue)
            {
                var icon = GetItemIcon(item.Value);
                if (icon != null)
                {
                    var iconSize = 24;
                    var iconRect = new Rectangle(rect.X + (rect.Width - iconSize) / 2, rect.Y + (rect.Height - iconSize) / 2, iconSize, iconSize);
                    spriteBatch.Draw(icon, iconRect, Color.White);
                }
            }
        }
        
        private Texture2D GetItemIcon(ItemType itemType)
        {
            return itemType switch
            {
                ItemType.Boomerang => Texture2DStorage.GetTexture("icon_boomerang"),
                ItemType.Bomb => Texture2DStorage.GetTexture("icon_bomb"),
                ItemType.Bow => Texture2DStorage.GetTexture("icon_bow"),
                ItemType.Arrow => Texture2DStorage.GetTexture("icon_arrow"),
                ItemType.CandleRed => Texture2DStorage.GetTexture("icon_candle"),
                ItemType.CandleBlue => Texture2DStorage.GetTexture("icon_candle"),
                ItemType.Recorder => Texture2DStorage.GetTexture("icon_recorder"),
                ItemType.Food => Texture2DStorage.GetTexture("icon_food"),
                ItemType.PotionRed => Texture2DStorage.GetTexture("icon_potion"),
                ItemType.PotionBlue => Texture2DStorage.GetTexture("icon_potion"),
                ItemType.MagicalRod => Texture2DStorage.GetTexture("icon_rod"),
                _ => null
            };
        }
        
        private void DrawVisitedRoomsMap(SpriteBatch spriteBatch, Vector2 pos, int width, int height)
        {
            var roomManager = getRoomManager();
            if (roomManager == null) return;
            
            var mapRect = new Rectangle((int)pos.X, (int)pos.Y, width, height);
            DrawRectangle(spriteBatch, mapRect, new Color(255, 165, 0)); // Orange
            
            for (int i = 0; i < width; i += 4)
            {
                DrawRectangle(spriteBatch, new Rectangle((int)pos.X + i, (int)pos.Y, 2, 2), new Color(200, 120, 0));
                DrawRectangle(spriteBatch, new Rectangle((int)pos.X + i + 2, (int)pos.Y + height - 2, 2, 2), new Color(200, 120, 0));
            }
            
            var visitedRooms = roomManager.GetVisitedRegularRooms();
            var connections = new Dictionary<int, RoomConnections>();
            
            for (int roomId = 1; roomId <= 17; roomId++)
            {
                var roomConn = roomManager.GetRoomConnections(roomId);
                if (roomConn != null)
                {
                    connections[roomId] = roomConn;
                }
            }
            
            int[] roomLayout = {
                1, 2, 3, 4, 5, 6,
                7, 8, 9, 10, 11, 12,
                13, 14, 15, 16, 17, -1
            };
            
            const int mapRows = 3;
            const int mapCols = 6;
            int cellWidth = width / mapCols;
            int cellHeight = height / mapRows;
            const int roomSize = 8;
            
            for (int row = 0; row < mapRows; row++)
            {
                for (int col = 0; col < mapCols; col++)
                {
                    int idx = row * mapCols + col;
                    if (idx >= roomLayout.Length) continue;
                    
                    int roomId = roomLayout[idx];
                    if (roomId == -1) continue;
                    
                    int roomX = (int)pos.X + col * cellWidth + (cellWidth - roomSize) / 2;
                    int roomY = (int)pos.Y + row * cellHeight + (cellHeight - roomSize) / 2;
                    
                    if (visitedRooms.Contains(roomId))
                    {
                        var roomRect = new Rectangle(roomX, roomY, roomSize, roomSize);
                        DrawRectangle(spriteBatch, roomRect, Color.Black);
                        
                        if (roomId == roomManager.CurrentRoomId)
                        {
                            int dotSize = 4;
                            var dotRect = new Rectangle(roomX + (roomSize - dotSize) / 2, roomY + (roomSize - dotSize) / 2, dotSize, dotSize);
                            DrawRectangle(spriteBatch, dotRect, Color.Green);
                        }
                        
                        if (connections.ContainsKey(roomId))
                        {
                            var conn = connections[roomId];
                            
                            if (conn.North != -1 && visitedRooms.Contains(conn.North))
                            {
                                var (nRow, nCol) = GetRoomPosition(conn.North, roomLayout, mapCols);
                                int nX = (int)pos.X + nCol * cellWidth + (cellWidth - roomSize) / 2 + roomSize / 2;
                                int nY = (int)pos.Y + nRow * cellHeight + (cellHeight - roomSize) / 2 + roomSize;
                                DrawLine(spriteBatch, new Vector2(roomX + roomSize / 2, roomY), new Vector2(nX, nY), Color.Black, 1);
                            }
                            
                            if (conn.South != -1 && visitedRooms.Contains(conn.South))
                            {
                                var (sRow, sCol) = GetRoomPosition(conn.South, roomLayout, mapCols);
                                int sX = (int)pos.X + sCol * cellWidth + (cellWidth - roomSize) / 2 + roomSize / 2;
                                int sY = (int)pos.Y + sRow * cellHeight + (cellHeight - roomSize) / 2;
                                DrawLine(spriteBatch, new Vector2(roomX + roomSize / 2, roomY + roomSize), new Vector2(sX, sY), Color.Black, 1);
                            }
                            
                            if (conn.East != -1 && visitedRooms.Contains(conn.East))
                            {
                                var (eRow, eCol) = GetRoomPosition(conn.East, roomLayout, mapCols);
                                int eX = (int)pos.X + eCol * cellWidth + (cellWidth - roomSize) / 2;
                                int eY = (int)pos.Y + eRow * cellHeight + (cellHeight - roomSize) / 2 + roomSize / 2;
                                DrawLine(spriteBatch, new Vector2(roomX + roomSize, roomY + roomSize / 2), new Vector2(eX, eY), Color.Black, 1);
                            }
                            
                            if (conn.West != -1 && visitedRooms.Contains(conn.West))
                            {
                                var (wRow, wCol) = GetRoomPosition(conn.West, roomLayout, mapCols);
                                int wX = (int)pos.X + wCol * cellWidth + (cellWidth - roomSize) / 2 + roomSize;
                                int wY = (int)pos.Y + wRow * cellHeight + (cellHeight - roomSize) / 2 + roomSize / 2;
                                DrawLine(spriteBatch, new Vector2(roomX, roomY + roomSize / 2), new Vector2(wX, wY), Color.Black, 1);
                            }
                        }
                    }
                }
            }
        }
        
        private (int row, int col) GetRoomPosition(int roomId, int[] roomLayout, int cols)
        {
            for (int i = 0; i < roomLayout.Length; i++)
            {
                if (roomLayout[i] == roomId)
                {
                    return (i / cols, i % cols);
                }
            }
            return (-1, -1);
        }
        
        private void DrawRectangle(SpriteBatch spriteBatch, Rectangle rect, Color color)
        {
            spriteBatch.Draw(pixelTexture, rect, color);
        }
        
        private void DrawRectangleOutline(SpriteBatch spriteBatch, Rectangle rect, Color color, int thickness)
        {
            DrawRectangle(spriteBatch, new Rectangle(rect.X, rect.Y, rect.Width, thickness), color);
            DrawRectangle(spriteBatch, new Rectangle(rect.X, rect.Bottom - thickness, rect.Width, thickness), color);
            DrawRectangle(spriteBatch, new Rectangle(rect.X, rect.Y, thickness, rect.Height), color);
            DrawRectangle(spriteBatch, new Rectangle(rect.Right - thickness, rect.Y, thickness, rect.Height), color);
        }
        
        private void DrawLine(SpriteBatch spriteBatch, Vector2 start, Vector2 end, Color color, int thickness)
        {
            Vector2 direction = end - start;
            float length = direction.Length();
            direction.Normalize();
            
            for (int i = 0; i < (int)length; i++)
            {
                Vector2 pos = start + direction * i;
                DrawRectangle(spriteBatch, new Rectangle((int)pos.X, (int)pos.Y, thickness, thickness), color);
            }
        }
    }
}

