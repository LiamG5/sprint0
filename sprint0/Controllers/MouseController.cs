using Microsoft.Xna.Framework.Input;
using sprint0.Interfaces;
using sprint0.Managers;
using System;

namespace sprint0.Controllers
{
    public class MouseController : IController
    {
        private MouseState previousMouseState;
        private RoomManager roomManager;
        
        public MouseController(RoomManager roomManager)
        {
            this.roomManager = roomManager;
            this.previousMouseState = Mouse.GetState();
        }
        
        public void Update()
        {
            MouseState currentMouseState = Mouse.GetState();
            
            if (currentMouseState.LeftButton == ButtonState.Pressed && 
                previousMouseState.LeftButton == ButtonState.Released)
            {
                int prevRoom = Math.Max(1, roomManager.CurrentRoomId - 1);
                if (prevRoom != roomManager.CurrentRoomId)
                {
                    roomManager.TransitionToRoom(prevRoom, TransitionDirection.West, "Content");
                }
            }
            
            if (currentMouseState.RightButton == ButtonState.Pressed && 
                previousMouseState.RightButton == ButtonState.Released)
            {
                int nextRoom = Math.Min(17, roomManager.CurrentRoomId + 1);
                if (nextRoom != roomManager.CurrentRoomId)
                {
                    roomManager.TransitionToRoom(nextRoom, TransitionDirection.East, "Content");
                }
            }
            
            previousMouseState = currentMouseState;
        }
    }
}
