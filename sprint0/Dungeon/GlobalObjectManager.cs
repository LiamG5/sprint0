using System.Collections.Generic;
using sprint0.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace sprint0.Dungeon
{
    public class SpawnedObject
    {
        public ISprite Sprite { get; set; }
        public Vector2 Position { get; set; }
        public SpawnedObject(ISprite s, Vector2 p) { Sprite = s; Position = p; }
    }

    public static class GlobalObjectManager
    {
        public static List<SpawnedObject> Blocks = new List<SpawnedObject>();
        public static List<SpawnedObject> Enemies = new List<SpawnedObject>();
        public static List<SpawnedObject> Items = new List<SpawnedObject>();

        public static void ClearAll()
        {
            Blocks.Clear();
            Enemies.Clear();
            Items.Clear();
        }

        public static void UpdateAll(GameTime gameTime)
        {
            foreach (var b in Blocks) b.Sprite.Update(gameTime);
            foreach (var e in Enemies) e.Sprite.Update(gameTime);
            foreach (var i in Items) i.Sprite.Update(gameTime);
        }

        public static void DrawAll(SpriteBatch spriteBatch)
        {
            foreach (var b in Blocks) b.Sprite.Draw(spriteBatch, b.Position);
            foreach (var e in Enemies) e.Sprite.Draw(spriteBatch, e.Position);
            foreach (var i in Items) i.Sprite.Draw(spriteBatch, i.Position);
        }
    }
}
