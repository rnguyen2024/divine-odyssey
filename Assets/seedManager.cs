using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;

public class seedManager : MonoBehaviour
{
    public GameObject dotPrefab;
    public GameObject plantPrefab;

    public int maxDots = 1;
    public float spawnAreaWidth = 8f;
    public float spawnAreaHeight = 4f;
    public float minDotDistance = 1.5f;
    public float dotLifetime = 3f;
    public int plantGoal = 10;

    public Text messageText; // 👈 Assign your UI Text in the inspector

    private List<Vector2> activePositions = new();
    private int plantCount = 0;
    private bool gameEnded = false;

    void Start()
    {
        messageText.text = ""; // Start with no message
        SpawnDot();
    }

    void SpawnDot()
    {
        Vector2 newPos = GenerateNonOverlappingPosition();
        if (newPos == Vector2.zero) return;

        GameObject dot = Instantiate(dotPrefab, newPos, Quaternion.identity);
        dot.GetComponent<DotClick>().Init(plantPrefab, this, newPos);
        activePositions.Add(newPos);

        StartCoroutine(DestroyDotAfter(dot, newPos, dotLifetime));
    }

    Vector2 GenerateNonOverlappingPosition()
    {
        int maxAttempts = 20;
        for (int i = 0; i < maxAttempts; i++)
        {
            Vector2 randomPos = new Vector2(
                Random.Range(-5f, 5f),
                Random.Range(-4.5f, 0.5f)
            );

            bool tooClose = false;
            foreach (Vector2 pos in activePositions)
            {
                if (Vector2.Distance(pos, randomPos) < minDotDistance)
                {
                    tooClose = true;
                    break;
                }
            }

            if (!tooClose)
                return randomPos;
        }

        return Vector2.zero; // failed to find a valid spot
    }

    IEnumerator DestroyDotAfter(GameObject dot, Vector2 pos, float seconds)
    {
        yield return new WaitForSeconds(seconds);
        if (dot != null)
            Destroy(dot);

        activePositions.Remove(pos);

        if (activePositions.Count < maxDots && !gameEnded)
            SpawnDot();
    }

    public void OnPlantSpawned(Vector2 position)
    {
        plantCount++;
        activePositions.Remove(position);

        if (plantCount >= plantGoal && !gameEnded)
        {
            gameEnded = true;
            ShowEndMessage("🌱 The Earth is green and fruitful!");
        }
        else
        {
            if (activePositions.Count < maxDots)
                SpawnDot();
        }
    }

    void ShowEndMessage(string message)
    {
        if (messageText != null)
            messageText.text = message;
    }
}
