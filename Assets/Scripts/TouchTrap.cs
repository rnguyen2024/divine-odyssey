using UnityEngine;

public class TouchTrap : MonoBehaviour
{
    [Header("Spike Settings")]
    public float damageAmount = 20f;     // Instant energy loss
    public float damageCooldown = 1.5f;  // Time between hits

    private bool canDamage = true;

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (!canDamage) return;

        if (collision.gameObject.CompareTag("Player"))
        {
            if (EnergyGauge.Instance != null)
            {
                EnergyGauge.Instance.RefillEnergy(-damageAmount);
                StartCoroutine(DamageCooldown());
            }
        }
    }

    private System.Collections.IEnumerator DamageCooldown()
    {
        canDamage = false;
        yield return new WaitForSeconds(damageCooldown);
        canDamage = true;
    }
}
