using System;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuWindow : MonoBehaviour
{
    [SerializeField] private Button _playStartButton;

    public event Action PlayStartButtonPressed;

    private void Awake()
    {
        _playStartButton.onClick.AddListener(StartPlay);
    }

    private void StartPlay()
    {
        gameObject.SetActive(false);
        PlayStartButtonPressed?.Invoke();
    }

    public void DisplayStartMenu()
    {
        gameObject.SetActive(true);
    }
}
