using UnityEngine;

public class UserInterface : MonoBehaviour
{
    [field: SerializeField] public MainMenuPanel MainMenuPanel { get; private set; }
    [field: SerializeField] public InformationPanel InformationPanel { get; private set; }
    [field: SerializeField] public GameObject HUD { get; private set; }

    public void OnStartGame()
    {
        MainMenuPanel.gameObject.SetActive(false);
        InformationPanel.gameObject.SetActive(true);
        HUD.gameObject.SetActive(true);

        MainMenuPanel.HideVictoryText();
        MainMenuPanel.HideDefeatText();
    }

    public void OnFinishGame()
    {
        HUD.gameObject.SetActive(false);
        InformationPanel.gameObject.SetActive(false);
        MainMenuPanel.gameObject.SetActive(true);
    }

    public void OnVictory()
    {
        OnFinishGame();
        MainMenuPanel.ShowVictoryText();
    }

    public void OnDefeated()
    {
        OnFinishGame();
        MainMenuPanel.ShowDefeatText();
    }
}
