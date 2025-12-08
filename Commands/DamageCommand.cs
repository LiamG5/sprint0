using sprint0.Interfaces;
using sprint0.Classes;

namespace sprint0.Commands
{
    public sealed class DamageCommand : ICommand
    {
        private readonly Link link;
        private readonly int damage;
        public DamageCommand(Link link, int damage) { this.link = link; this.damage = damage; }
        public void Execute() => link.TakeDamage(damage);
    }
}