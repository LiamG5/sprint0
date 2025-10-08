using System.Collections.Generic;
using Microsoft.Xna.Framework.Input;
using sprint0.Interfaces;

namespace sprint0.Classes
{
    public sealed class GameplayKeyboardController : IController
    {
        private KeyboardState prev, curr;
        private readonly InputBindings bindings;
        private readonly ICommand stop;
        private readonly HashSet<Keys> moveKeys = new()
        { Keys.W, Keys.A, Keys.S, Keys.D, Keys.Up, Keys.Left, Keys.Down, Keys.Right };

        public GameplayKeyboardController(InputBindings bindings, IPlayer player)
        {
            this.bindings = bindings;
            stop = new sprint0.Commands.StopCommand(player);
        }

        public void Update()
        {
            curr = Keyboard.GetState();

            foreach (var kv in bindings.PressOnce)
                if (curr.IsKeyDown(kv.Key) && !prev.IsKeyDown(kv.Key))
                    kv.Value.Execute();

            bool anyMove = false;
            foreach (var kv in bindings.Hold)
            {
                if (curr.IsKeyDown(kv.Key))
                {
                    kv.Value.Execute();
                    if (moveKeys.Contains(kv.Key)) anyMove = true;
                }
            }

            if (!anyMove) stop.Execute();

            prev = curr;
        }
    }
}
