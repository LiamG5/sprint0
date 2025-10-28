using Microsoft.Xna.Framework;
using sprint0;
using sprint0.Interfaces;

namespace sprint0.Commands
{
    public sealed class MoveCommand : ICommand
    {
        private readonly IPlayer player;
        private readonly Vector2 dir;
        public MoveCommand(IPlayer player, Vector2 direction) { this.player = player; dir = direction; }
        public void Execute() => player.Move(dir);
    }
}
