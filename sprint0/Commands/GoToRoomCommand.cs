using sprint0.Interfaces;
using sprint0.Managers;

namespace sprint0.Commands
{
    public class GoToRoomCommand : ICommand
    {
        private RoomManager roomManager;
        private int targetRoomId;
        private Game1 game;
        
        public GoToRoomCommand(RoomManager manager, int roomId, Game1 game)
        {
            this.roomManager = manager;
            this.targetRoomId = roomId;
            this.game = game;
        }
        
        public void Execute()
        {
            roomManager.TransitionToRoom(targetRoomId, TransitionDirection.None, game.Content.RootDirectory);
        }
    }
}