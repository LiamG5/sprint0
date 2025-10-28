using sprint0.Interfaces;
using sprint0.Classes;

namespace sprint0.Commands
{
    public sealed class MoveUpCommand : ICommand
    {
        private readonly Link link;
        public MoveUpCommand(Link link) { this.link = link; }
        public void Execute() => link.MoveUp();
    }
}
