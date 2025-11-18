using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using sprint0.Sprites;
using sprint0.Managers;
using sprint0.Interfaces;
using static sprint0.Sprites.ItemFactory;

namespace sprint0.HUD
{
    public class InventoryMenu : IHudElement
    {
        private readonly Func<List<ItemType>> getInventoryItems;
        private readonly Func<ItemType?> getItemInSlotB;
        private readonly Action<ItemType?> setItemInSlotB;
        private readonly Func<int> getCurrentSelection;
        private readonly Action<int> setCurrentSelection;
        private readonly Func<RoomManager> getRoomManager;
        private readonly Func<int> getHearts;
        private readonly Func<int> getMaxHearts;
        private readonly Func<int> getRupees;
        private readonly Func<int> getKeys;
        private readonly Func<int> getBombs;
        private readonly Func<bool> hasMap;
        private readonly Func<bool> hasCompass;
        private readonly Texture2D slotBg;
        private readonly Texture2D pixelTexture;
        private readonly SpriteFont font;
        private readonly GraphicsDevice graphicsDevice;
        private readonly Texture2D itemSpriteSheet;
        private readonly Texture2D hudSpriteSheet;
        private readonly Texture2D linkSpriteSheet;
        private readonly IItem fullHeartItem;
        private readonly IItem emptyHeartItem;
        
        private const int MenuPadding = 20;
        private const int SlotSize = 32;
        private const int SlotSpacing = 4;
        private const int InventoryRows = 2;
        private const int InventoryCols = 4;
        private const int MapWidth = 200;
        private const int MapHeight = 150;
        private const int InventoryPadding = 12;
        private const int InventoryBorderThickness = 4;
        
        public InventoryMenu(
            Func<List<ItemType>> getInventoryItems,
            Func<ItemType?> getItemInSlotB,
            Action<ItemType?> setItemInSlotB,
            Func<int> getCurrentSelection,
            Action<int> setCurrentSelection,
            Func<RoomManager> getRoomManager,
            Func<int> getHearts,
            Func<int> getMaxHearts,
            Func<int> getRupees,
            Func<int> getKeys,
            Func<int> getBombs,
            Func<bool> hasMap,
            Func<bool> hasCompass,
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
            this.hasMap = hasMap ?? (() => false);
            this.hasCompass = hasCompass ?? (() => false);
            this.font = font;
            this.graphicsDevice = graphicsDevice;
            
            slotBg = Texture2DStorage.GetTexture("hud_slot_bg");
            
            pixelTexture = new Texture2D(graphicsDevice, 1, 1);
            pixelTexture.SetData(new[] { Color.White });
            
            itemSpriteSheet = Texture2DStorage.GetItemSpriteSheet();
            hudSpriteSheet = Texture2DStorage.GetHudSpriteSheet();
            linkSpriteSheet = Texture2DStorage.GetLinkSpriteSheet();
            if (itemSpriteSheet != null)
            {
                fullHeartItem = new ItemRecoveryHeart(itemSpriteSheet);
                emptyHeartItem = new ItemEmptyHeart(itemSpriteSheet);
            }
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
            
            var inventoryItems = getInventoryItems();
            int selectedIndex = getCurrentSelection();
            
            int inventoryContentWidth = InventoryCols * SlotSize + (InventoryCols - 1) * SlotSpacing;
            int inventoryContentHeight = InventoryRows * SlotSize + (InventoryRows - 1) * SlotSpacing;
            int inventoryWidth = inventoryContentWidth + InventoryPadding * 2;
            int inventoryHeight = inventoryContentHeight + InventoryPadding * 2;
            var inventoryStartPos = topLeft + new Vector2(screenWidth - MenuPadding - inventoryWidth, 40);
            var inventoryRect = new Rectangle((int)inventoryStartPos.X, (int)inventoryStartPos.Y, inventoryWidth, inventoryHeight);
            DrawRectangleOutline(spriteBatch, inventoryRect, new Color(0, 200, 255), InventoryBorderThickness);
            
            var itemStartPos = inventoryStartPos + new Vector2(InventoryPadding, InventoryPadding);
            for (int row = 0; row < InventoryRows; row++)
            {
                for (int col = 0; col < InventoryCols; col++)
                {
                    int index = row * InventoryCols + col;
                    var slotPos = itemStartPos + new Vector2(col * (SlotSize + SlotSpacing), row * (SlotSize + SlotSpacing));
                    var slotRect = new Rectangle((int)slotPos.X, (int)slotPos.Y, SlotSize, SlotSize);
                    
                    ItemType? item = index < inventoryItems.Count ? inventoryItems[index] : (ItemType?)null;
                    if (item.HasValue && itemSpriteSheet != null)
                    {
                        var sourceRect = GetItemSourceRect(item.Value);
                        if (sourceRect.HasValue)
                        {
                            float scale = 2.0f;
                            var itemPos = new Vector2(slotRect.X + (slotRect.Width - sourceRect.Value.Width * scale) / 2, slotRect.Y + (slotRect.Height - sourceRect.Value.Height * scale) / 2);
                            spriteBatch.Draw(itemSpriteSheet, itemPos, sourceRect.Value, Color.White, 0f, Vector2.Zero, scale, SpriteEffects.None, 0f);
                        }
                    }
                    
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
            
            if (hasMap() && hudSpriteSheet != null)
            {
                var mapIconRect = GetHudSpriteRect("map_icon");
                if (mapIconRect.HasValue)
                {
                    var mapIconPos = mapLabelPos + new Vector2(0, 20);
                    spriteBatch.Draw(hudSpriteSheet, mapIconPos, mapIconRect.Value, Color.White, 0f, Vector2.Zero, 2.0f, SpriteEffects.None, 0f);
                }
            }
            
            var compassLabelPos = mapLabelPos + new Vector2(0, 60);
            spriteBatch.DrawString(font, "COMPASS", compassLabelPos, Color.Orange, 0f, Vector2.Zero, 1.0f, SpriteEffects.None, 0f);
            
            if (hasCompass() && hudSpriteSheet != null)
            {
                var compassIconRect = GetHudSpriteRect("compass_icon");
                if (compassIconRect.HasValue)
                {
                    var compassIconPos = compassLabelPos + new Vector2(0, 20);
                    spriteBatch.Draw(hudSpriteSheet, compassIconPos, compassIconRect.Value, Color.White, 0f, Vector2.Zero, 2.0f, SpriteEffects.None, 0f);
                }
            }
            
            var bottomY = screenHeight - 80;
            var bottomLeft = new Vector2(MenuPadding, bottomY);
            
            spriteBatch.DrawString(font, "LEVEL-1", bottomLeft, Color.White, 0f, Vector2.Zero, 1.0f, SpriteEffects.None, 0f);
            
            if (linkSpriteSheet != null)
            {
                var linkSpritePos = bottomLeft + new Vector2(0, 20);
                var linkSourceRect = new Rectangle(0, 0, 16, 16);
                spriteBatch.Draw(linkSpriteSheet, linkSpritePos, linkSourceRect, Color.White, 0f, Vector2.Zero, 2.0f, SpriteEffects.None, 0f);
            }
            
            var itemCountsPos = bottomLeft + new Vector2(120, -8);
            
            if (itemSpriteSheet != null)
            {
                var rupeeRect = new Rectangle(40 * 4, 40 * 3, 15, 16);
                spriteBatch.Draw(itemSpriteSheet, itemCountsPos, rupeeRect, Color.White, 0f, Vector2.Zero, 1.5f, SpriteEffects.None, 0f);
                spriteBatch.DrawString(font, "x" + getRupees(), itemCountsPos + new Vector2(20, -2), Color.White, 0f, Vector2.Zero, 1.2f, SpriteEffects.None, 0f);
                
                var keyRect = new Rectangle(40 * 7, 40 * 1, 15, 16);
                spriteBatch.Draw(itemSpriteSheet, itemCountsPos + new Vector2(0, 28), keyRect, Color.White, 0f, Vector2.Zero, 1.5f, SpriteEffects.None, 0f);
                spriteBatch.DrawString(font, "x" + getKeys(), itemCountsPos + new Vector2(20, 26), Color.White, 0f, Vector2.Zero, 1.2f, SpriteEffects.None, 0f);
                
                var bombRect = new Rectangle(40 * 5, 40 * 0, 15, 16);
                spriteBatch.Draw(itemSpriteSheet, itemCountsPos + new Vector2(0, 56), bombRect, Color.White, 0f, Vector2.Zero, 1.5f, SpriteEffects.None, 0f);
                spriteBatch.DrawString(font, "x" + getBombs(), itemCountsPos + new Vector2(20, 54), Color.White, 0f, Vector2.Zero, 1.2f, SpriteEffects.None, 0f);
            }
            
            var slotBPos = bottomLeft + new Vector2(280, 0);
            var slotAPos = slotBPos + new Vector2(SlotSize + 8, 0);
            var slotBRect = new Rectangle((int)slotBPos.X, (int)slotBPos.Y, SlotSize, SlotSize);
            var slotARect = new Rectangle((int)slotAPos.X, (int)slotAPos.Y, SlotSize, SlotSize);
            
            DrawSlot(spriteBatch, slotBRect, getItemInSlotB());
            DrawSlot(spriteBatch, slotARect, ItemType.Sword);
            
            spriteBatch.DrawString(font, "B", slotBPos + new Vector2(0, -24), Color.White, 0f, Vector2.Zero, 1.0f, SpriteEffects.None, 0f);
            spriteBatch.DrawString(font, "A", slotAPos + new Vector2(0, -24), Color.White, 0f, Vector2.Zero, 1.0f, SpriteEffects.None, 0f);
            
            var lifeLabelPos = bottomLeft + new Vector2(screenWidth - MenuPadding - 200, 0);
            spriteBatch.DrawString(font, "-LIFE-", lifeLabelPos, Color.White, 0f, Vector2.Zero, 1.0f, SpriteEffects.None, 0f);
            
            var heartsPos = lifeLabelPos + new Vector2(0, 20);
            int hearts = getHearts();
            int maxHearts = getMaxHearts();
            
            if (fullHeartItem != null && emptyHeartItem != null)
            {
                var heartCursor = heartsPos;
                for (int i = 0; i < maxHearts; i++)
                {
                    var heartItem = (i < hearts) ? fullHeartItem : emptyHeartItem;
                    heartItem.Draw(spriteBatch, heartCursor);
                    heartCursor.X += 34;
                }
            }
            
            DrawRectangle(spriteBatch, new Rectangle(0, screenHeight - 4, screenWidth, 4), new Color(173, 216, 230));
        }
        
        private void DrawSlot(SpriteBatch spriteBatch, Rectangle rect, ItemType? item)
        {
            DrawRectangleOutline(spriteBatch, rect, new Color(0, 200, 255), 2);
            
            if (item.HasValue && itemSpriteSheet != null)
            {
                var sourceRect = GetItemSourceRect(item.Value);
                if (sourceRect.HasValue)
                {
                    float scale = 2.0f;
                    var itemPos = new Vector2(rect.X + (rect.Width - sourceRect.Value.Width * scale) / 2, rect.Y + (rect.Height - sourceRect.Value.Height * scale) / 2);
                    spriteBatch.Draw(itemSpriteSheet, itemPos, sourceRect.Value, Color.White, 0f, Vector2.Zero, scale, SpriteEffects.None, 0f);
                }
            }
        }
        
        private Rectangle? GetItemSourceRect(ItemType itemType)
        {
            return itemType switch
            {
                ItemType.RupeeRed => new Rectangle(40 * 4, 40 * 3, 15, 16),
                ItemType.RupeeBlue => new Rectangle(40 * 5, 40 * 3, 15, 16),
                ItemType.SmallKey => new Rectangle(40 * 7, 40 * 1, 15, 16),
                ItemType.Bomb => new Rectangle(40 * 5, 40 * 0, 15, 16),
                ItemType.Boomerang => new Rectangle(40 * 7, 40 * 0, 15, 16),
                ItemType.Bow => new Rectangle(40 * 0, 40 * 1, 15, 16),
                ItemType.Arrow => new Rectangle(40 * 1, 40 * 1, 15, 16),
                ItemType.CandleRed => new Rectangle(40 * 2, 40 * 1, 15, 16),
                ItemType.CandleBlue => new Rectangle(40 * 3, 40 * 1, 15, 16),
                ItemType.Recorder => new Rectangle(360, 40 * 2, 13, 16),
                ItemType.Food => new Rectangle(40 * 5, 40 * 1, 15, 16),
                ItemType.PotionRed => new Rectangle(40 * 6, 40 * 1, 15, 16),
                ItemType.PotionBlue => new Rectangle(40 * 7, 40 * 2, 15, 16),
                ItemType.MagicalRod => new Rectangle(40 * 0, 40 * 2, 15, 16),
                ItemType.Sword => new Rectangle(40 * 7, 40 * 3, 15, 16),
                ItemType.WhiteSword => new Rectangle(40 * 0, 40 * 3, 15, 16),
                ItemType.MagicalBoomerang => new Rectangle(40 * 1, 40 * 3, 15, 16),
                ItemType.SilverArrow => new Rectangle(40 * 2, 40 * 3, 15, 16),
                ItemType.Letter => new Rectangle(40 * 6, 40 * 2, 15, 16),
                ItemType.Raft => new Rectangle(40 * 3, 40 * 2, 15, 16),
                ItemType.BookOfMagic => new Rectangle(40 * 4, 40 * 2, 15, 16),
                ItemType.BlueRing => new Rectangle(40 * 5, 40 * 2, 15, 16),
                ItemType.RedRing => new Rectangle(40 * 6, 40 * 2, 15, 16),
                ItemType.Stepladder => new Rectangle(40 * 1, 40 * 2, 15, 16),
                ItemType.MagicalKey => new Rectangle(40 * 2, 40 * 2, 15, 16),
                ItemType.PowerBracelet => new Rectangle(40 * 4, 40 * 1, 15, 16),
                ItemType.Compass => new Rectangle(40 * 0, 40 * 0, 15, 16),
                ItemType.DungeonMap => new Rectangle(40 * 1, 40 * 0, 15, 16),
                ItemType.TriforceFragment => new Rectangle(40 * 2, 40 * 0, 15, 16),
                ItemType.RecoveryHeart => new Rectangle(40 * 6, 40 * 1, 15, 16),
                ItemType.HeartContainer => new Rectangle(40 * 7, 40 * 1, 15, 16),
                ItemType.Clock => new Rectangle(40 * 3, 40 * 0, 15, 16),
                ItemType.Fairy => new Rectangle(40 * 3, 40 * 1, 15, 16),
                _ => null
            };
        }
        
        private IItem CreateItem(ItemType itemType)
        {
            if (itemSpriteSheet == null) return null;
            
            return itemType switch
            {
                ItemType.Boomerang => new ItemBoomerang(itemSpriteSheet),
                ItemType.Bomb => new ItemBomb(itemSpriteSheet),
                ItemType.Bow => new ItemBow(itemSpriteSheet),
                ItemType.Arrow => new ItemArrow(itemSpriteSheet),
                ItemType.CandleRed => new ItemCandleRed(itemSpriteSheet),
                ItemType.CandleBlue => new ItemCandleBlue(itemSpriteSheet),
                ItemType.Recorder => new ItemRecorder(itemSpriteSheet),
                ItemType.Food => new ItemFood(itemSpriteSheet),
                ItemType.PotionRed => new ItemPotionRed(itemSpriteSheet),
                ItemType.PotionBlue => new ItemPotionBlue(itemSpriteSheet),
                ItemType.MagicalRod => new ItemMagicalRod(itemSpriteSheet),
                ItemType.Sword => new ItemSword(itemSpriteSheet),
                ItemType.WhiteSword => new ItemWhiteSword(itemSpriteSheet),
                ItemType.MagicalBoomerang => new ItemMagicalBoomerang(itemSpriteSheet),
                ItemType.SilverArrow => new ItemSilverArrow(itemSpriteSheet),
                ItemType.Letter => new ItemLetter(itemSpriteSheet),
                ItemType.Raft => new ItemRaft(itemSpriteSheet),
                ItemType.BookOfMagic => new ItemBookOfMagic(itemSpriteSheet),
                ItemType.BlueRing => new ItemBlueRing(itemSpriteSheet),
                ItemType.RedRing => new ItemRedRing(itemSpriteSheet),
                ItemType.Stepladder => new ItemStepladder(itemSpriteSheet),
                ItemType.MagicalKey => new ItemMagicalKey(itemSpriteSheet),
                ItemType.PowerBracelet => new ItemPowerBracelet(itemSpriteSheet),
                ItemType.Compass => new ItemCompass(itemSpriteSheet),
                ItemType.DungeonMap => new ItemDungeonMap(itemSpriteSheet),
                ItemType.SmallKey => new ItemSmallKey(itemSpriteSheet),
                ItemType.TriforceFragment => new ItemTriforceFragment(itemSpriteSheet),
                ItemType.RecoveryHeart => new ItemRecoveryHeart(itemSpriteSheet),
                ItemType.HeartContainer => new ItemHeartContainer(itemSpriteSheet),
                ItemType.Clock => new ItemClock(itemSpriteSheet),
                ItemType.RupeeRed => new ItemRupeeRed(itemSpriteSheet),
                ItemType.RupeeBlue => new ItemRupeeBlue(itemSpriteSheet),
                ItemType.Fairy => new ItemFairy(itemSpriteSheet),
                _ => null
            };
        }
        
        private void DrawVisitedRoomsMap(SpriteBatch spriteBatch, Vector2 pos, int width, int height)
        {
            var roomManager = getRoomManager();
            if (roomManager == null) return;
            
            var mapRect = new Rectangle((int)pos.X, (int)pos.Y, width, height);
            DrawRectangle(spriteBatch, mapRect, new Color(255, 165, 0));
            
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
               -1, 16, 17, -1, -1, -1,
               -1, -1, 13, -1, 14, 15,
                8,  9, 10, 11, 12, -1,
               -1,  5,  6,  7, -1, -1,
               -1, -1,  4, -1, -1, -1,
               -1,  1,  2,  3, -1, -1
            };
            
            const int mapRows = 6;
            const int mapCols = 6;
            int cellWidth = width / mapCols;
            int cellHeight = height / mapRows;
            const int roomSize = 8;
            const int roomSpacing = 2;
            
            Dictionary<int, Vector2> roomPositions = new Dictionary<int, Vector2>();
            
            for (int row = 0; row < mapRows; row++)
            {
                for (int col = 0; col < mapCols; col++)
                {
                    int idx = row * mapCols + col;
                    if (idx >= roomLayout.Length) continue;
                    
                    int roomId = roomLayout[idx];
                    if (roomId == -1) continue;
                    
                    if (!visitedRooms.Contains(roomId)) continue;
                    
                    int roomX = (int)pos.X + col * cellWidth + (cellWidth - roomSize) / 2 + roomSpacing / 2;
                    int roomY = (int)pos.Y + row * cellHeight + (cellHeight - roomSize) / 2 + roomSpacing / 2;
                    int roomCenterX = roomX + (roomSize - roomSpacing) / 2;
                    int roomCenterY = roomY + (roomSize - roomSpacing) / 2;
                    
                    roomPositions[roomId] = new Vector2(roomCenterX, roomCenterY);
                    
                    var roomRect = new Rectangle(roomX, roomY, roomSize - roomSpacing, roomSize - roomSpacing);
                    DrawRectangle(spriteBatch, roomRect, Color.Black);
                    
                    if (roomId == roomManager.CurrentRoomId)
                    {
                        int dotSize = 6;
                        var dotRect = new Rectangle(roomX + (roomSize - roomSpacing - dotSize) / 2, roomY + (roomSize - roomSpacing - dotSize) / 2, dotSize, dotSize);
                        DrawRectangle(spriteBatch, dotRect, Color.Green);
                    }
                }
            }
            
            foreach (var kvp in roomPositions)
            {
                int roomId = kvp.Key;
                Vector2 roomCenter = kvp.Value;
                
                if (!connections.ContainsKey(roomId)) continue;
                var conn = connections[roomId];
                
                if (conn.North != -1 && visitedRooms.Contains(conn.North) && roomPositions.ContainsKey(conn.North))
                {
                    DrawLine(spriteBatch, roomCenter, roomPositions[conn.North], Color.Black, 1);
                }
                if (conn.South != -1 && visitedRooms.Contains(conn.South) && roomPositions.ContainsKey(conn.South))
                {
                    DrawLine(spriteBatch, roomCenter, roomPositions[conn.South], Color.Black, 1);
                }
                if (conn.East != -1 && visitedRooms.Contains(conn.East) && roomPositions.ContainsKey(conn.East))
                {
                    DrawLine(spriteBatch, roomCenter, roomPositions[conn.East], Color.Black, 1);
                }
                if (conn.West != -1 && visitedRooms.Contains(conn.West) && roomPositions.ContainsKey(conn.West))
                {
                    DrawLine(spriteBatch, roomCenter, roomPositions[conn.West], Color.Black, 1);
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
        
        private Rectangle? GetHudSpriteRect(string spriteName)
        {
            if (hudSpriteSheet == null) return null;
            
            return spriteName switch
            {
                "map_icon" => new Rectangle(0, 0, 16, 16),
                "compass_icon" => new Rectangle(0, 0, 16, 16),
                _ => null
            };
        }
    }
}

