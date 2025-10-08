using sprint0.Interfaces;
using sprint0.Classes;

namespace sprint0.Commands
{
    public sealed class IdleCommand : ICommand
    {
        private readonly Link link;
        public IdleCommand(Link link) { this.link = link; }
        public void Execute() => link.Idle();
    }
}
