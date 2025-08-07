using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public Transform player;        // Assign in Inspector
    public float followSpeed = 3f;  // Speed of movement
    public Vector3 offset = new Vector3(1f, 1f, 0f); // Optional offset

    void Update()
    {
        if (player != null)
        {
            Vector3 targetPosition = player.position + offset;
            transform.position = Vector3.Lerp(transform.position, targetPosition, followSpeed * Time.deltaTime);
        }
    }
}
