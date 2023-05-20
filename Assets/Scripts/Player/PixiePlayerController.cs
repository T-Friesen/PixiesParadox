using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PixiePlayerController : MonoBehaviour
{
    [SerializeField]
    private float speed = 5f;                 // The movement speed of the player
    [SerializeField]
    private float slowdownRate = 2f;         // The rate at which the player slows down
    private Rigidbody2D rb;
    private Vector2 movement;
    private bool isMoving = false;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        movement = new Vector2(moveHorizontal, moveVertical).normalized;
        isMoving = movement.magnitude > 0;
    }

    void FixedUpdate()
    {
        if (isMoving)
        {
            rb.AddForce(movement * speed);
        }
        else
        {
            // Gradually slow down the player's velocity
            rb.velocity = Vector2.Lerp(rb.velocity, Vector2.zero, slowdownRate * Time.fixedDeltaTime);
        }
    }

}
