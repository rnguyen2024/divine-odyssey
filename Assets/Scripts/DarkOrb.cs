using UnityEngine;

public class DarkOrb : MonoBehaviour
{
    [Header("Leech Settings")]
    public float leechRange = 2.5f;      // Distance at which it starts draining
    public float leechRate = 5f;         // Energy drained per second

    private Transform player;

    void Start()
    {
        // Just grab player transform for distance checking
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
    }

    void Update()
    {
        if (player == null || EnergyGauge.Instance == null) return;

        // Check distance
        float dist = Vector2.Distance(transform.position, player.position);
        if (dist <= leechRange)
        {
            // Drain energy over time
            EnergyGauge.Instance.RefillEnergy(-leechRate * Time.deltaTime);
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, leechRange);
    }
}
