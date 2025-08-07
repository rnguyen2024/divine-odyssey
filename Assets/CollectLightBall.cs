using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollectLightBall : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player touched the light ball!");
            // Add your logic here, for example:
            Destroy(gameObject); // remove the light ball
            SceneManager.LoadScene("Day 1");
        }
    }
}
