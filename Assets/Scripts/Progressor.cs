using System;
using UnityEngine;

//Тут должен быть счётчик убитых врагов. при достижении 10 убитых врагов останавливается спавн зомби, выходит босс.при этом пишется уведомление, что вышел босс и потом уведомление что анчалась новая волна
public class Progressor : MonoBehaviour
{
    [SerializeField] private int[] _requiredNumberKills;

    private int _numberKills;
    private int _currentWave;

    public event Action KillTargetMet;

    public void ResetCounter()
    {
        _currentWave = 1;
        _numberKills = 0;

        Debug.Log($"ВОЛНА {_currentWave}");
    }

    public void OnKillingEnemy(Enemy _)
    {
        _numberKills++;        

        if (_numberKills >= _requiredNumberKills[_currentWave - 1])
        {
            KillTargetMet?.Invoke();
        }
    }

    private void SwitchWave()//Это будет после убийства босса а не зомби
    {
        _currentWave++;

        Debug.Log($"ВОЛНА {_currentWave}");
    }
}
