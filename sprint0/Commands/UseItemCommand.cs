using sprint0;
using sprint0.Interfaces;

namespace sprint0.Commands
{
    public sealed class UseItemCommand : ICommand
    {
        private readonly IPlayer player;
        private readonly int slot;
        public UseItemCommand(IPlayer player, int slot) { this.player = player; this.slot = slot; }
        public void Execute() => player.UseItem(slot);
    }
}
