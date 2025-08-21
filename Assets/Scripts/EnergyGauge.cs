using UnityEngine;
using UnityEngine.UI; // Needed for UI Slider

public class EnergyGauge : MonoBehaviour
{
    public static EnergyGauge Instance; // Singleton reference

    [Header("Energy Settings")]
    public float maxEnergy = 100f;      
    public float drainDuration = 60f;   
    
    private float energyLevel;          

    [Header("UI")]
    public Slider energySlider;         

    void Awake()
    {
        // Make sure only one EnergyGauge exists
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    void Start()
    {
        energyLevel = maxEnergy;

        if (energySlider != null)
        {
            energySlider.minValue = 0;
            energySlider.maxValue = maxEnergy;
            energySlider.value = energyLevel;
        }
    }

    void Update()
    {
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

    public void RefillEnergy(float amount)
    {
        energyLevel = Mathf.Clamp(energyLevel + amount, 0, maxEnergy);
        UpdateGauge();
    }

    public float GetEnergyPercent()
    {
        return energyLevel / maxEnergy;
    }
}
