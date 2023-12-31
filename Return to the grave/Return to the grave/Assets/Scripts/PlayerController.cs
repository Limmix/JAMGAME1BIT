using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Animator playerAnimator;
    [SerializeField] private Rigidbody2D playerRigidbody2D;

    [SerializeField] private Transform groundCheckPoint;

    [SerializeField] private Vector2 groundCheckSize = new Vector2(1f, 0.1f);

    [SerializeField] private LayerMask groundLayerMask;

    [SerializeField] private float speed = 3f;

    [SerializeField] private float jumpForce = 400f;

    private float horizontalInput = 0;

    private void Update()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");

        if (horizontalInput > 0f && transform.right.x < 0f)
        {
            Flip();
        }

        if (horizontalInput < 0f && transform.right.x > 0f)
        {
            Flip();
        }

        if (Input.GetButtonDown("Jump") && IsOnGround())
        {
            Jump();
        }
    }
    private void FixedUpdate()
    {
        float horizontalSpeed = horizontalInput * speed;

        playerRigidbody2D.velocity =
            new Vector2(horizontalSpeed, playerRigidbody2D.velocity.y);
    }

    private void Jump()
    {
        playerRigidbody2D.velocity = Vector2.zero;
        playerRigidbody2D.AddForce(Vector2.up * jumpForce);
    }

    private bool IsOnGround()
    {
        if (Physics2D.OverlapBox(groundCheckPoint.position, groundCheckSize, 0f, groundLayerMask))
        {
            return true;
        }
        return false;
    }
    private void Flip()
    {
        transform.right = -transform.right;
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(groundCheckPoint.position, groundCheckSize);
    }
    private void Attack()
    {

    }
    private void Block()
    {

    }
}
