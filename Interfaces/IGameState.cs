namespace sprint0.Interfaces
{
    public interface IGameState
    {
        bool IsMenu { get; }
        bool IsGameplay { get; }
        void StartGameplay();
        void Reset();
        void Quit();
    }
}
