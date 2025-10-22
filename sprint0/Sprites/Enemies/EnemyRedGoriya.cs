using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using sprint0.Classes;
using sprint0.Interfaces;
using sprint0.Sprites.Enemies;
using System;

namespace sprint0.Sprites
{
    public class EnemyRedGoriya : ISprite {

        private Texture2D enemySS;
        // private static int enemyTypeX = 1;
        // private static int enemyTypeY = 2;
        private static Vector2 enemyPos = new Vector2(200, 100);
        private static Rectangle frame1 = new Rectangle(16 * 0, 16 * 2, 16, 16);
        private static Rectangle frame2 = new Rectangle(16 * 1, 16 * 2, 16, 16);
        private EnemyAnimationHelper animation = new EnemyAnimationHelper(frame1, frame2);
        public EnemyRedGoriya (Texture2D sheet)
        {
            enemySS = sheet;
        }
        public void Update(GameTime gameTime)
        {
            animation.Update(gameTime);
        }
        public void Draw(SpriteBatch spriteBatch, Vector2 position)
        {
            spriteBatch.Draw(enemySS, position, animation.GetFrame(), Color.White, 0f, Vector2.Zero, 3.0f, SpriteEffects.None, 0f);
        }
    }
}
