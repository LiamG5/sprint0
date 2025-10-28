using sprint0;
using sprint0.Interfaces;

namespace sprint0.Commands
{
    public sealed class StartGameplayCommand : ICommand
    {
        private readonly IGameState state;
        public StartGameplayCommand(IGameState state) { this.state = state; }
        public void Execute() => state.StartGameplay();
    }
}
