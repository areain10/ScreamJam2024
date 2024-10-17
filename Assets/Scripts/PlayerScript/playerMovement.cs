using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 2f;

    Rigidbody2D rb;

    public bool canMove;

    Vector2 movementDirection;
    // Start is called before the first frame update
    void Start()
    {
        canMove = true;
        rb = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        movementDirection = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
    }
    private void FixedUpdate()
    {
        if (canMove)
        {
            rb.velocity = movementDirection * moveSpeed;
        }
        else
        {
            rb.velocity = Vector2.zero;
        }
        
    }
}
