using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public Transform player;        // Assign in Inspector
    public float followSpeed = 3f;  // Speed of movement
    public Vector3 offset = new Vector3(1f, 1f, 0f); // Optional offset
    public float moveDistance = 1f;

    void Update()
    {
        if (player != null)
        {
            Vector3 targetPosition = player.position + offset;
            transform.position = Vector3.Lerp(transform.position, targetPosition, followSpeed * Time.deltaTime);
        }
        if (Input.GetMouseButtonDown(0)) // Left click
        {
            offset = new Vector3(-moveDistance, 1.5f, 0f); // Move left
            UpdatePosition();
        }
        else if (Input.GetMouseButtonDown(1)) // Right click
        {
            offset = new Vector3(moveDistance, 1.5f, 0f); // Move right
            UpdatePosition();
        }
        void UpdatePosition()
    {
        if (player != null)
        {
            transform.position = player.position + offset;
        }
    }
    }
    /*public float moveDistance = 1f;   // How far the light moves each click
    void Start()
    {
        // Start with light right above player
        offset = new Vector3(moveDistance, 0f, 0f);
        UpdatePosition();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Left click
        {
            offset = new Vector3(-moveDistance, 0f, 0f); // Move left
            UpdatePosition();
        }
        else if (Input.GetMouseButtonDown(1)) // Right click
        {
            offset = new Vector3(moveDistance, 0f, 0f); // Move right
            UpdatePosition();
        }
    }

    void UpdatePosition()
    {
        if (player != null)
        {
            transform.position = player.position + offset;
        }
    }*/
}
