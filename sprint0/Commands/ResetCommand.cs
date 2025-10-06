using sprint0;
using sprint0.Interfaces;

namespace sprint0.Commands
{
    public sealed class ResetCommand : ICommand
    {
        private readonly IGameState state;
        public ResetCommand(IGameState state) { this.state = state; }
        public void Execute() => state.Reset();
    }
}
