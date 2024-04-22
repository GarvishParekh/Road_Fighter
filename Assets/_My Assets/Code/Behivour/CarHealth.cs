using UnityEngine;
using UnityEngine.UI;

public class CarHealth : MonoBehaviour
{
    [Header("<size=15>[SCRIPT]")]
    [SerializeField] private CarCollision carCollision;

    [Header ("<size=15>[SCRIPTABLE OBJECT]")]
    [SerializeField] private CarData carData;

    [Header("<size=15>[USER INTERFACE]")]
    [SerializeField] private Image healthBar;

    private void Start()
    {
        ResetHealth();
    }

    private void Update()
    {
        switch (carData.healthDepletion)
        {
            case HealthDepletion.ON:
                DepleteHealth(carData.healthDepletionRate);
                break;
            case HealthDepletion.OFF:
                break;
        }
    }

    private void ResetHealth()
    {
        carData.carHealth = 1;
        healthBar.fillAmount = carData.carHealth;
        carData.healthDepletion = HealthDepletion.OFF;
    }

    private void DepleteHealth(float depleationRate)
    {
        if (carData.carHealth <= 0)
        {
            carCollision.GameOverFunction();
            return;
        }

        carData.carHealth -= depleationRate * Time.deltaTime;
        healthBar.fillAmount = carData.carHealth;
    }
}
