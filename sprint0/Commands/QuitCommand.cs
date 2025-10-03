using sprint0;
using sprint0.Interfaces;

namespace sprint0.Commands
{
    public sealed class QuitCommand : ICommand
    {
        private readonly IGameState state;
        public QuitCommand(IGameState state) { this.state = state; }
        public void Execute() => state.Quit();
    }
}
