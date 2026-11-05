using UnityEngine;
using UnityEngine.UI;

public class HealthBarDisplay : MonoBehaviour
{
    [SerializeField] private Slider _availableAmountHealthBar;

    public void UpdateHealthDisplay(float value)
    {
        _availableAmountHealthBar.value = value;
    }
}
