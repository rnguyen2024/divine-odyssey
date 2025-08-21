using UnityEngine;

public class GoldenOrb : MonoBehaviour
{
    [Header("Golden Orb Settings")]
    public float restoreAmount = 25f;   // How much energy it restores
    public float floatSpeed = 1f;       // Small floating effect
    public float floatHeight = 0.25f;   // Range of float up/down

    private Vector3 startPos;

    void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
        // Floating up and down effect
        float newY = startPos.y + Mathf.Sin(Time.time * floatSpeed) * floatHeight;
        transform.position = new Vector3(startPos.x, newY, startPos.z);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && EnergyGauge.Instance != null)
        {
            // Restore player energy
            EnergyGauge.Instance.RefillEnergy(restoreAmount);

            // Destroy the collectible
            Destroy(gameObject);
        }
    }
}
