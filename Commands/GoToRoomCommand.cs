using sprint0.Interfaces;

namespace sprint0.Commands
{
    public sealed class GoToRoomCommand : ICommand
    {
        private readonly Game1 game;
        private readonly int roomId;

        public GoToRoomCommand(Game1 game, int roomId)
        {
            this.game = game;
            this.roomId = roomId;
        }

        public void Execute()
        {
            game.GoToRoom(roomId);
        }
    }
}

