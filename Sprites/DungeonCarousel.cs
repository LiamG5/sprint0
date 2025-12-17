using Microsoft.Xna.Framework.Graphics;
using sprint0.Factories;
using sprint0.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace sprint0.Sprites
{
    public class DungeonCarousel : ICarousel
    {
        private BlockFactory blocks;
        private SpriteBatch sprite;
        private int idx = 0;
        private Dungeon[] list =
        {
            Dungeon.Start,
            Dungeon.Debug
        };
        public enum Dungeon
        {
            Start, Debug
        }
        public DungeonCarousel(BlockFactory blocks, SpriteBatch sprite)
        {
            this.blocks = blocks;
            this.sprite = sprite;
        }

        public void Next()
        {
            idx = (idx + 1) % list.Length;
            UpdateCurrentRoom();
        }
        
        public void Prev()
        {
            idx = (idx - 1 + list.Length) % list.Length;
            UpdateCurrentRoom();
        }

        private void UpdateCurrentRoom()
        {
            switch (list[idx])
            {
                case Dungeon.Start:
                    break;
                case Dungeon.Debug:
                    break;
            }
        }

    }
}
