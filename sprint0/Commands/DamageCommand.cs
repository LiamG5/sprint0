using sprint0;
using sprint0.Interfaces;
using sprint0.Classes;

namespace sprint0
{
    public sealed class DamageCommand : ICommand
    {
        private readonly Link player;
        public DamageCommand(Link player) { this.player = player; }
        public void Execute() => player.TakeDamage();
    }
}
