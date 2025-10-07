using sprint0;
using sprint0.Interfaces;
using sprint0.Classes;


namespace sprint0.Commands
{
    public sealed class UseItem2Command : ICommand
    {
        private readonly Link player;
        private readonly int slot;
        public UseItem2Command(Link player) { this.player = player; }
        public void Execute() => player.UseItem2();
    }
}
