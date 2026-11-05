using UnityEngine;

public class Zombie : Enemy
{
    [SerializeField] private GameObject[] _skins;

    public void ChooseSkin()
    {
        for (int i = 0; i < _skins.Length; i++)
        {
            _skins[i].SetActive(false);
        }

        _skins[Random.Range(0, _skins.Length)].SetActive(true);
    }
}
