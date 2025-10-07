using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using sprint0.Interfaces;
using sprint0.Commands;
using sprint0.Classes;

namespace sprint0.Controllers
{
    public sealed class KeyboardController : IController
    {

        private readonly Dictionary<Keys, ICommand> pressOnce = new();
        private readonly Dictionary<Keys, ICommand> hold = new();

        private KeyboardState prev, curr;

        public KeyboardController(Link player)
        {

            hold[Keys.W] = new MoveUpCommand(player);
            hold[Keys.A] = new MoveDownCommand(player);
            hold[Keys.S] = new MoveLeftCommand(player);
            hold[Keys.D] = new MoveRightCommand(player);
            hold[Keys.Up]    = hold[Keys.W];
            hold[Keys.Left]  = hold[Keys.A];
            hold[Keys.Down]  = hold[Keys.S];
            hold[Keys.Right] = hold[Keys.D];

            pressOnce[Keys.Z] = new AttackCommand(player);
            pressOnce[Keys.N] = new AttackCommand(player);
            pressOnce[Keys.E] = new DamageCommand(player);
            pressOnce[Keys.M] = new MagicCommand(player);

            pressOnce[Keys.D1] = new UseItem1Command(player);
            pressOnce[Keys.D2] = new UseItem2Command(player);
            pressOnce[Keys.D3] = new UseItem3Command(player);

        }

        public void Update()
        {
            curr = Keyboard.GetState();

            
            foreach (var kv in pressOnce)
                if (curr.IsKeyDown(kv.Key) && !prev.IsKeyDown(kv.Key))
                    kv.Value.Execute();

            foreach (var kv in hold)
                if (curr.IsKeyDown(kv.Key))
                    kv.Value.Execute();

            prev = curr;
        }
    }
}
