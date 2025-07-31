using UnityEngine;

public class DotClick : MonoBehaviour
{
    private GameObject plantPrefab;
    private seedManager manager;
    private Vector2 myPosition;

    public void Init(GameObject plant, seedManager sm, Vector2 position)
    {
        plantPrefab = plant;
        manager = sm;
        myPosition = position;
    }

    void OnMouseDown()
    {
        Instantiate(plantPrefab, transform.position, Quaternion.identity);
        manager.OnPlantSpawned(myPosition);
        Destroy(gameObject);
    }
}
