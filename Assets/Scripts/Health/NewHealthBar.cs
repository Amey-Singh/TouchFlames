using UnityEngine;
using UnityEngine.UI;

public class NewHealthBar : MonoBehaviour
{
    [SerializeField] private Health playerHealth;
    public Slider slider;

    private void Start()
    {
        UpdateHealthBar();
    }

    private void Update()
    {
        UpdateHealthBar();
    }

    private void UpdateHealthBar()
    {
        if (playerHealth != null && slider != null)
        {
            // Use GetStartingHealth to retrieve the starting health value
            slider.maxValue = playerHealth.GetStartingHealth();
            slider.value = playerHealth.currentHealth;
        }
    }
}
