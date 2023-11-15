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

    [Header("Knockback")]
    private float knockbackForce;
    private float knockbackCounter;
    private float knockbackTime;
    private bool knockbackRight;

    private bool canAttack = true;



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
        if (Input.GetButtonDown("Fire1") && canAttack)
        {
            StartCoroutine(Attack());
        }

    }
    private void FixedUpdate()
    {
        float horizontalSpeed = horizontalInput * speed;
        playerRigidbody2D.velocity =
                new Vector2(horizontalSpeed, playerRigidbody2D.velocity.y);
        //if (knockbackCounter <= 0)
        //{

        //}
        //else
        //{
        //    if(knockbackRight == true)
        //    {
        //        playerRigidbody2D.velocity = new Vector2(knockbackForce)
        //    }
        //}

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
    private IEnumerator Attack()
    {
        playerAnimator.SetTrigger("Attack");
        yield return new WaitForSeconds(2f);
    }
    private void Block()
    {

    }
}
