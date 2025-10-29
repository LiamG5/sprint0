using sprint0.Interfaces;
using sprint0.Classes;

namespace sprint0.Commands
{
    public sealed class MoveRightCommand : ICommand
    {
        private readonly Link link;
        public MoveRightCommand(Link link) { this.link = link; }
        public void Execute() => link.MoveRight();
    }
}
