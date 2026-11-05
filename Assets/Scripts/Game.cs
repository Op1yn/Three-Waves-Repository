using UnityEngine;

public class Game : MonoBehaviour
{
    [SerializeField] private UserInterface _userInterface;
    [SerializeField] private Scene _scene;

    private void Start()
    {
        _scene.Initialize();
        SubscribeToEventsEndGame();
        _userInterface.MainMenuPanel.PlayStartButtonPressed += StartGame;
        _scene.Progressor.WaveStarted += _userInterface.InformationPanel.OnWaveStarted;
    }

    private void OnDestroy()
    {
        _scene.Progressor.WaveStarted -= _userInterface.InformationPanel.OnWaveStarted;
        _userInterface.MainMenuPanel.PlayStartButtonPressed -= StartGame;
        UnsubscribeFromEventsEndGame();
    }

    private void StartGame()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        _userInterface.OnStartGame();
        _scene.StartScene();
    }

    public void FinishGame()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    private void SubscribeToEventsEndGame()
    {
        _scene.VictoryWon += _userInterface.OnVictory;
        _scene.Defeated += _userInterface.OnDefeated;
        _scene.VictoryWon += FinishGame;
        _scene.Defeated += FinishGame;
    }

    private void UnsubscribeFromEventsEndGame()
    {
        _scene.Defeated -= FinishGame;
        _scene.VictoryWon -= FinishGame;
        _scene.Defeated -= _userInterface.OnDefeated;
        _scene.VictoryWon -= _userInterface.OnVictory;
    }
}
