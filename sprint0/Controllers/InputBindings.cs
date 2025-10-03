using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using sprint0.Commands;
using sprint0.Interfaces;

namespace sprint0.Classes
{
    public sealed class InputBindings
    {
        public readonly Dictionary<Keys, ICommand> PressOnce = new();
        public readonly Dictionary<Keys, ICommand> Hold = new();

        public static InputBindings CreateDefault(IPlayer player, IGameState state,
                                                  ICarousel blocks, ICarousel items, ICarousel enemies)
        {
            var b = new InputBindings();

            b.Hold[Keys.W] = new MoveCommand(player, new Vector2(0, -1));
            b.Hold[Keys.A] = new MoveCommand(player, new Vector2(-1, 0));
            b.Hold[Keys.S] = new MoveCommand(player, new Vector2(0,  1));
            b.Hold[Keys.D] = new MoveCommand(player, new Vector2( 1, 0));
            b.Hold[Keys.Up]    = b.Hold[Keys.W];
            b.Hold[Keys.Left]  = b.Hold[Keys.A];
            b.Hold[Keys.Down]  = b.Hold[Keys.S];
            b.Hold[Keys.Right] = b.Hold[Keys.D];

            b.PressOnce[Keys.Z] = new AttackCommand(player);
            b.PressOnce[Keys.N] = new AttackCommand(player);
            b.PressOnce[Keys.E] = new DamageCommand(player);

            b.PressOnce[Keys.D1] = new UseItemCommand(player, 1);
            b.PressOnce[Keys.D2] = new UseItemCommand(player, 2);
            b.PressOnce[Keys.D3] = new UseItemCommand(player, 3);
            b.PressOnce[Keys.D4] = new UseItemCommand(player, 4);
            b.PressOnce[Keys.D5] = new UseItemCommand(player, 5);

            b.PressOnce[Keys.T] = new PrevCommand(blocks);
            b.PressOnce[Keys.Y] = new NextCommand(blocks);
            b.PressOnce[Keys.U] = new PrevCommand(items);
            b.PressOnce[Keys.I] = new NextCommand(items);
            b.PressOnce[Keys.O] = new PrevCommand(enemies);
            b.PressOnce[Keys.P] = new NextCommand(enemies);

            b.PressOnce[Keys.Q] = new QuitCommand(state);
            b.PressOnce[Keys.R] = new ResetCommand(state);

            return b;
        }
    }
}
