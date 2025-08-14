using UnityEngine;

public class EnergyDrainOrb : MonoBehaviour
{
    public float fallSpeed = 3f;        // How fast it falls
    public float damageAmount = 10f;    // How much energy it removes

    private void Update()
    {
        // Move downward
        transform.Translate(Vector2.down * fallSpeed * Time.deltaTime);

        // Destroy if far below the scene
        if (transform.position.y < -10f)
            Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if we hit the player
        if (collision.CompareTag("Player"))
        {
            // Try to get EnergyGauge component from the player
            EnergyGauge energy = collision.GetComponent<EnergyGauge>();
            if (energy != null)
            {
                energy.RefillEnergy(-damageAmount); // subtract energy
            }

            Destroy(gameObject); // remove orb
        }
    }
}
