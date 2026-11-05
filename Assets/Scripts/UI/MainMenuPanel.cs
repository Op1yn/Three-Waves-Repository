using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MainMenuPanel : MonoBehaviour
{
    [SerializeField] private Button _playStartButton;
    [SerializeField] private TMP_Text _textVictory;
    [SerializeField] private TMP_Text _textDefeat;

    public event Action PlayStartButtonPressed;

    private void Awake()
    {
        _playStartButton.onClick.AddListener(OnGameStartButtonPressed);
    }

    public void ShowStartMenu()
    {
        gameObject.SetActive(true);
    }

    public void ShowVictoryText()
    {
        _textVictory.gameObject.SetActive(true);
    }

    public void HideVictoryText()
    {
        _textVictory.gameObject.SetActive(false);
    }

    public void ShowDefeatText()
    {
        _textDefeat.gameObject.SetActive(true);
    }

    public void HideDefeatText()
    {
        _textDefeat.gameObject.SetActive(false);
    }

    private void OnGameStartButtonPressed()
    {
        PlayStartButtonPressed?.Invoke();
    }
}
