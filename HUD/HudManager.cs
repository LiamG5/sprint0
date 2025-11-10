using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace sprint0.HUD
{
    public class HudManager
    {
        private readonly List<IHudElement> elements = new();

        public void Add(IHudElement e) => elements.Add(e);

        public void Update(GameTime gameTime)
        {
            for (int i = 0; i < elements.Count; i++)
                elements[i].Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < elements.Count; i++)
                elements[i].Draw(spriteBatch);
        }
    }
}
