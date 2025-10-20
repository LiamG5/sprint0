using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using sprint0.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sprint0.Sprites
{
    public class DungeonLoader : ISprite
    {
        private BlockFactory blocks;

        public DungeonLoader(BlockFactory blocks)
        {
            this.blocks = blocks;
        }

        public void Update(GameTime gameTime)
        {

        }

        public void Draw(SpriteBatch sprite, Vector2 pos)
        {

        }
    }
}
