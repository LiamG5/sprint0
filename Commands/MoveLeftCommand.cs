using sprint0.Interfaces;
using sprint0.Classes;

namespace sprint0.Commands
{
    public sealed class MoveLeftCommand : ICommand
    {
        private readonly Link link;
        public MoveLeftCommand(Link link) { this.link = link; }
        public void Execute() => link.MoveLeft();
    }
}
