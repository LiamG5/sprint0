using sprint0;
using sprint0.Interfaces;
using sprint0.Classes;

namespace sprint0
{
    public sealed class AttackCommand : ICommand
    {
        private readonly Link player;
        public AttackCommand(Link player) { this.player = player; }
        public void Execute() => player.Attack();
    }
}
