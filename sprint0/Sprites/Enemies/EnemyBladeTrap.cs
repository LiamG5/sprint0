using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using sprint0.Interfaces;
using sprint0.Classes;
using System;
using Microsoft.Xna.Framework.Content;

namespace sprint0.Sprites
{
    public class EnemyBladeTrap : ISprite {

        private Texture2D enemySS;
        private static int enemyTypeX = 2;
        private static int enemyTypeY = 21;
        private static Vector2 enemyPos = new Vector2(200, 100);
        private static Rectangle enemy = new Rectangle(16 * enemyTypeX, 16 * enemyTypeY, 16, 16);
        public EnemyBladeTrap (Texture2D sheet)
        {
            enemySS = sheet;
        }
        public void Update(GameTime gameTime)
        {
            
        }
        public void Draw(SpriteBatch spriteBatch, Vector2 position)
        {
            float scale = 3.0f; // Make sprites 3x bigger
            spriteBatch.Draw(enemySS, position, enemy, Color.White, 0f, Vector2.Zero, scale, SpriteEffects.None, 0f);
        }
    }
}
