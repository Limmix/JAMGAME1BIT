using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Animator playerAnimator;
    [SerializeField] private Rigidbody2D playerRigidbody2D;
    [SerializeField] private Transform groundCheckPoint;

    private Vector2 groundCheckSize = new Vector2(1.2f, 0.1f);

    [SerializeField] private LayerMask groundLayerMask;
    [SerializeField] private float speed = 3f;
    [SerializeField] private float jumpForce = 400f;

    private float horizontalInput = 0;

    private bool canAttack = true;
    private float attackCooldown = 2f;
    [SerializeField] private Collider2D swordCollider;
    private void Start()
    {
        swordCollider.enabled = false;
    }
    private void Update()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");

        bool hasHorizontalInput = horizontalInput != 0;

        playerAnimator.SetBool("isRunning", hasHorizontalInput);

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
        // Trigger appropriate animations based on player's state

        if (Input.GetButtonDown("Fire1") && canAttack)
        {
            StartCoroutine(Attack());
        }

        PlayerHeightCheck();
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
            playerAnimator.SetBool("Grounded", true);
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
    private IEnumerator Attack()
    {
        canAttack = false;
        swordCollider.enabled = true;
        playerAnimator.SetTrigger("Attack");
        speed = 0f;
        yield return new WaitForSeconds(attackCooldown);
        canAttack = true;
        swordCollider.enabled = false;
    }

    private void PlayerHeightCheck()
    {
        if (IsOnGround() == false)
        {
            bool isJumping = playerRigidbody2D.velocity.y > 0;
            bool isFalling = playerRigidbody2D.velocity.y < 0;

            if (isJumping)
            {
                playerAnimator.SetTrigger("Jump");
            }
            else if (isFalling)
            {
                playerAnimator.SetTrigger("Fall");
            }
        }
    }
    private void Move()
    {
        speed = 4.5f;
    }
}
