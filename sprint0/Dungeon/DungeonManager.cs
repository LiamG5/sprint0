using System;
using System.Collections.Generic;
using System.IO;
using sprint0.Classes;
using sprint0.Sprites;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using sprint0.Dungeon;

namespace sprint0.Dungeon
{
    public class Room
    {
        public string Id { get; set; }
        public string Left { get; set; }
        public string Right { get; set; }
        public string Up { get; set; }
        public string Down { get; set; }
        public int Width { get; set; } = 256;
        public int Height { get; set; } = 240;
        public string BlocksCsv { get; set; }
        public string ItemsCsv { get; set; }
        public string EnemiesCsv { get; set; }
    }

    public class DungeonManager
    {
        public Dictionary<string, Room> Rooms { get; private set; } = new Dictionary<string, Room>();
        public Room CurrentRoom { get; private set; }
        private SpriteBatch spriteBatch;
        private BlockFactory blockFactory;
        private EnemySpriteFactory enemyFactory;
        private ItemFactory itemFactory;
        private const int PLAYER_MARGIN = 4;

        public DungeonManager(SpriteBatch sb, BlockFactory bf = null, EnemySpriteFactory ef = null, ItemFactory itf = null)
{
    spriteBatch = sb;
    blockFactory = bf ?? BlockFactory.Instance;
    enemyFactory = ef ?? EnemySpriteFactory.Instance;
    itemFactory = itf ?? ItemFactory.Instance;
}


        public void LoadFromCsv(string csvPath)
        {
            if (!File.Exists(csvPath)) throw new FileNotFoundException(csvPath);
            var lines = File.ReadAllLines(csvPath);
            foreach (var line in lines)
            {
                var t = line.Trim();
                if (string.IsNullOrEmpty(t) || t.StartsWith("#")) continue;
                var parts = line.Split(',');
                var room = new Room();
                room.Id = parts[0].Trim();
                room.Left = parts.Length > 1 ? parts[1].Trim() : null;
                room.Right = parts.Length > 2 ? parts[2].Trim() : null;
                room.Up = parts.Length > 3 ? parts[3].Trim() : null;
                room.Down = parts.Length > 4 ? parts[4].Trim() : null;
                if (parts.Length > 5 && int.TryParse(parts[5], out var w)) room.Width = w;
                if (parts.Length > 6 && int.TryParse(parts[6], out var h)) room.Height = h;
                var objIndex = 7;
                if (parts.Length > objIndex) room.BlocksCsv = parts[objIndex].Trim();
                if (parts.Length > objIndex + 1) room.ItemsCsv = parts[objIndex + 1].Trim();
                if (parts.Length > objIndex + 2) room.EnemiesCsv = parts[objIndex + 2].Trim();
                Rooms[room.Id] = room;
            }
            foreach (var kv in Rooms) { CurrentRoom = kv.Value; break; }
            if (CurrentRoom != null) SpawnRoomObjects(CurrentRoom);
        }

        public void Update(Link player)
        {
            if (player == null || CurrentRoom == null) return;

            if (player.position.X < 0 - PLAYER_MARGIN)
            {
                if (!string.IsNullOrEmpty(CurrentRoom.Left) && Rooms.ContainsKey(CurrentRoom.Left))
                    TransitionTo(CurrentRoom.Left, player, EntranceDirection.Left);
                else player.position = new Vector2(0, player.position.Y);
            }
            else if (player.position.X > CurrentRoom.Width + PLAYER_MARGIN)
            {
                if (!string.IsNullOrEmpty(CurrentRoom.Right) && Rooms.ContainsKey(CurrentRoom.Right))
                    TransitionTo(CurrentRoom.Right, player, EntranceDirection.Right);
                else player.position = new Vector2(CurrentRoom.Width, player.position.Y);
            }
            else if (player.position.Y < 0 - PLAYER_MARGIN)
            {
                if (!string.IsNullOrEmpty(CurrentRoom.Up) && Rooms.ContainsKey(CurrentRoom.Up))
                    TransitionTo(CurrentRoom.Up, player, EntranceDirection.Up);
                else player.position = new Vector2(player.position.X, 0);
            }
            else if (player.position.Y > CurrentRoom.Height + PLAYER_MARGIN)
            {
                if (!string.IsNullOrEmpty(CurrentRoom.Down) && Rooms.ContainsKey(CurrentRoom.Down))
                    TransitionTo(CurrentRoom.Down, player, EntranceDirection.Down);
                else player.position = new Vector2(player.position.X, CurrentRoom.Height);
            }
        }

        private enum EntranceDirection { Left, Right, Up, Down }

        private void TransitionTo(string roomId, Link player, EntranceDirection dir)
        {
            if (!Rooms.ContainsKey(roomId)) return;
            var newRoom = Rooms[roomId];
            CurrentRoom = newRoom;
            SpawnRoomObjects(newRoom);
            switch (dir)
            {
                case EntranceDirection.Left:
                    player.position = new Vector2(newRoom.Width - 16, ClampY(player.position.Y, newRoom.Height));
                    break;
                case EntranceDirection.Right:
                    player.position = new Vector2(16, ClampY(player.position.Y, newRoom.Height));
                    break;
                case EntranceDirection.Up:
                    player.position = new Vector2(ClampX(player.position.X, newRoom.Width), newRoom.Height - 16);
                    break;
                case EntranceDirection.Down:
                    player.position = new Vector2(ClampX(player.position.X, newRoom.Width), 16);
                    break;
            }
            player.velocity = new Vector2(0, 0);
        }

        private void SpawnRoomObjects(Room room)
        {
            GlobalObjectManager.ClearAll();

            if (!string.IsNullOrEmpty(room.BlocksCsv))
            {
                var blocks = room.BlocksCsv.Split(';');
                foreach (var b in blocks)
                {
                    var t = b.Trim();
                    if (t == "") continue;
                    var parts = t.Split('@');
                    var type = parts[0].Trim();
                    var coords = parts.Length > 1 ? parts[1] : "0:0";
                    var xy = coords.Split(':');
                    int x = 0, y = 0;
                    int.TryParse(xy[0], out x); int.TryParse(xy[1], out y);
                    var mi = blockFactory.GetType().GetMethod("Build" + type + "Block");
                    if (mi != null)
                    {
                        var sprite = (Interfaces.ISprite)mi.Invoke(blockFactory, new object[] { spriteBatch });
                        GlobalObjectManager.Blocks.Add(new SpawnedObject(sprite, new Vector2(x, y)));
                    }
                }
            }

            if (!string.IsNullOrEmpty(room.ItemsCsv))
            {
                var items = room.ItemsCsv.Split(';');
                foreach (var it in items)
                {
                    var t = it.Trim();
                    if (t == "") continue;
                    var parts = t.Split('@');
                    var type = parts[0].Trim();
                    var coords = parts.Length > 1 ? parts[1] : "0:0";
                    var xy = coords.Split(':');
                    int x = 0, y = 0;
                    int.TryParse(xy[0], out x); int.TryParse(xy[1], out y);
                    var mi = itemFactory.GetType().GetMethod("Build" + type);
                    if (mi != null)
                    {
                        var sprite = (Interfaces.ISprite)mi.Invoke(itemFactory, new object[] { spriteBatch });
                        GlobalObjectManager.Items.Add(new SpawnedObject(sprite, new Vector2(x, y)));
                    }
                }
            }

            if (!string.IsNullOrEmpty(room.EnemiesCsv))
            {
                var enemies = room.EnemiesCsv.Split(';');
                foreach (var en in enemies)
                {
                    var t = en.Trim();
                    if (t == "") continue;
                    var parts = t.Split('@');
                    var type = parts[0].Trim();
                    var coords = parts.Length > 1 ? parts[1] : "0:0";
                    var xy = coords.Split(':');
                    int x = 0, y = 0;
                    int.TryParse(xy[0], out x); int.TryParse(xy[1], out y);
                    var mi = enemyFactory.GetType().GetMethod("Spawn" + type);
                    if (mi != null)
                    {
                        var sprite = (Interfaces.ISprite)mi.Invoke(enemyFactory, new object[] { spriteBatch });
                        GlobalObjectManager.Enemies.Add(new SpawnedObject(sprite, new Vector2(x, y)));
                    }
                }
            }
        }

        private float ClampX(float x, int width) => Math.Max(0, Math.Min(x, width));
        private float ClampY(float y, int height) => Math.Max(0, Math.Min(y, height));
    }
}
