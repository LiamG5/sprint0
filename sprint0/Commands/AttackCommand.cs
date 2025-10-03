using sprint0;
using sprint0.Interfaces;

namespace sprint0.Commands
{
    public sealed class AttackCommand : ICommand
    {
        private readonly IPlayer player;
        public AttackCommand(IPlayer player) { this.player = player; }
        public void Execute() => player.Attack();
    }
}
