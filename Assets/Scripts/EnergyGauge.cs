using UnityEngine;
using UnityEngine.UI; // Needed for UI Slider

public class EnergyGauge : MonoBehaviour
{
    [Header("Energy Settings")]
    public float maxEnergy = 100f;      // Maximum possible energy
    public float drainDuration = 60f;   // How long until it hits 0 (in seconds)
    
    private float energyLevel;          // Current energy value

    [Header("UI")]
    public Slider energySlider;         // UI Slider for gauge display

    void Start()
    {
        // Start with full energy
        energyLevel = maxEnergy;

        // Set up UI slider
        if (energySlider != null)
        {
            energySlider.minValue = 0;
            energySlider.maxValue = maxEnergy;
            energySlider.value = energyLevel;
        }
    }

    void Update()
    {
        // Drain energy over time
        if (energyLevel > 0)
        {
            energyLevel -= (maxEnergy / drainDuration) * Time.deltaTime;
            energyLevel = Mathf.Clamp(energyLevel, 0, maxEnergy);

            UpdateGauge();
        }
    }

    void UpdateGauge()
    {
        if (energySlider != null)
            energySlider.value = energyLevel;
    }

    // Call this method to refill energy (optional)
    public void RefillEnergy(float amount)
    {
        energyLevel = Mathf.Clamp(energyLevel + amount, 0, maxEnergy);
        UpdateGauge();
    }

    // Get the percentage of energy remaining
    public float GetEnergyPercent()
    {
        return energyLevel / maxEnergy;
    }
}
