using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class FloatingLight : MonoBehaviour
{
    public float moveSpeed = 2f;
    public float changeDirectionTime = 1.5f;

    private Rigidbody2D rb;
    private Vector2 moveDirection;
    private float timer;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        ChangeDirection();
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= changeDirectionTime)
        {
            ChangeDirection();
            timer = 0f;
        }

        rb.velocity = moveDirection * moveSpeed;
    }

    void ChangeDirection()
    {
        float angle = Random.Range(0f, 360f);
        moveDirection = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)).normalized;
    }
}
