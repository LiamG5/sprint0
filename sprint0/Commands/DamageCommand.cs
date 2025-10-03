using sprint0;
using sprint0.Interfaces;

namespace sprint0.Commands
{
    public sealed class DamageCommand : ICommand
    {
        private readonly IPlayer player;
        public DamageCommand(IPlayer player) { this.player = player; }
        public void Execute() => player.TakeDamage();
    }
}
