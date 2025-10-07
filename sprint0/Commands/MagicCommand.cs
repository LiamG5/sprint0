using sprint0.Interfaces;  
using sprint0.Classes;    


namespace sprint0
{
    public sealed class MagicCommand : ICommand
    {
        private readonly Link player;
        public MagicCommand(Link player) { this.player = player; }
        public void Execute() => player.UseMagic();
    }
}
