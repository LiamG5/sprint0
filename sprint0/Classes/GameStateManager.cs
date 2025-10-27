using System;

namespace sprint0.Classes
{
    public enum GameStateType
    {
        Start,
        Playing,
        RoomTransition,
        Paused,
        Inventory,
        GameOver
    }

    public class GameStateManager
    {
        public GameStateType CurrentState { get; private set; } = GameStateType.Playing;

        public void ChangeState(GameStateType newState)
        {
            // could add exit/enter hooks here if needed
            CurrentState = newState;
        }
    }
}