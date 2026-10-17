using System;
using UnityEngine;

public class Game : MonoBehaviour
{
    [SerializeField] private MainMenuWindow _mainMenuWindow;
    [SerializeField] private Scene _scene;

    public bool IsPlaying { get; private set; } = false;

    private void Start()
    {
        _mainMenuWindow.PlayStartButtonPressed += StartGame;
    }

    private void OnDisable()
    {
        _mainMenuWindow.PlayStartButtonPressed -= StartGame;
    }

    private void StartGame()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        IsPlaying = true;
        _scene.InitializeScene();
        _scene.GameOver += EndGame;
    }

    public void EndGame()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        IsPlaying = false;
        _mainMenuWindow.DisplayStartMenu();
        _scene.GameOver -= EndGame;
    }
}
