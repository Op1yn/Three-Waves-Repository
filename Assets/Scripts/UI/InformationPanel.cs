using System.Collections;
using TMPro;
using UnityEngine;

public class InformationPanel : MonoBehaviour
{
    [SerializeField] private TMP_Text _waveCountText;
    [SerializeField] private float _waitingTime;

    private WaitForSeconds waiting;

    private void Awake()
    {
        waiting = new WaitForSeconds(_waitingTime);
    }

    public void OnWaveStarted(int waveNumber)
    {
        waveNumber++;
        _waveCountText.text = waveNumber.ToString();
        gameObject.SetActive(true);

        StartCoroutine(HideAfterDelay());
    }

    private IEnumerator HideAfterDelay()
    {
        yield return waiting;
        gameObject.SetActive(false);
    }
}
