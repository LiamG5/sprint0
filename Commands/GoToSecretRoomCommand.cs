using sprint0.Interfaces;

namespace sprint0.Commands
{
    public sealed class GoToSecretRoomCommand : ICommand
    {
        private readonly Game1 game;
        public GoToSecretRoomCommand(Game1 game) { this.game = game; }
        public void Execute() => game.GoToSecretRoom();
    }
}