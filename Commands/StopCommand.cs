using sprint0;
using sprint0.Interfaces;

namespace sprint0.Commands
{
    public sealed class StopCommand : ICommand
    {
        private readonly IPlayer player;
        public StopCommand(IPlayer player) { this.player = player; }
        public void Execute() => player.Stop();
    }
}
