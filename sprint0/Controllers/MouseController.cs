using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using sprint0.Interfaces;

namespace sprint0.Classes
{
    public sealed class MouseController : IController
    {
        private readonly Rectangle mapRect;
        private readonly int rows, cols;
        private readonly Dictionary<int, ICommand> cellCommands;
        private MouseState previousState;

        public MouseController(Rectangle mapRect, int rows, int cols, Dictionary<int, ICommand> cellCommands)
        {
            this.mapRect = mapRect;
            this.rows = rows;
            this.cols = cols;
            this.cellCommands = cellCommands;
            previousState = Mouse.GetState();
        }

        public void Update()
        {
            var current = Mouse.GetState();
            var pos = new Point(current.X, current.Y);

            // Left click edge
            if (current.LeftButton == ButtonState.Pressed && previousState.LeftButton == ButtonState.Released)
            {
                if (mapRect.Contains(pos))
                {
                    int cellW = mapRect.Width / cols;
                    int cellH = mapRect.Height / rows;
                    int col = (pos.X - mapRect.X) / cellW;
                    int row = (pos.Y - mapRect.Y) / cellH;
                    int index = row * cols + col;

                    if (cellCommands.TryGetValue(index, out var cmd))
                        cmd.Execute();
                }
            }
            previousState = current;
        }
    }
}
