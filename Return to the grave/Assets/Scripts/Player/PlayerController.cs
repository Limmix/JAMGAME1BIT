using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Animator playerAnimator;
    [SerializeField] private Rigidbody2D playerRigidbody2D;
    [SerializeField] private Transform groundCheckPoint;

    [Header("Jump")]
    private Vector2 groundCheckSize = new Vector2(1.2f, 0.1f);
    [SerializeField] private LayerMask groundLayerMask;
    [SerializeField] private float speed = 3f;
    [SerializeField] public float jumpForce = 400f;

    private float horizontalInput = 0;

    [Header("Attack")]
    private bool canAttack = true;
    private float attackCooldown = 2f;
    [SerializeField] private Collider2D swordCollider;

    [Header("Block")]
    public bool canBlock = true;
    private float blockCooldown = 2f;
    [SerializeField] private Collider2D shieldCollider;

    [Header("SlashAttack")]
    public bool canSlashAttack = true;
    private float slashAttackCooldown = 10f;
    [SerializeField] private GameObject slashProjectilePrefab;

    [SerializeField] private GameManager gameManager;
    [SerializeField] private AudioManager audioManager;


    private void Start()
    {
        swordCollider.enabled = false;
        shieldCollider.enabled = false;
        
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
        if (Input.GetKeyDown(KeyCode.Mouse1) && canBlock)
        {
            StartCoroutine(Block());
        }
        if (Input.GetKeyDown(KeyCode.Mouse0) && canAttack)
        {
            StartCoroutine(Attack());
        }
        if (Input.GetKeyDown(KeyCode.E) && canSlashAttack)
        {
            StartCoroutine(SlashAttack());
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
        audioManager.JumpAudio();
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
        audioManager.SwordSwingAudio();
        speed = 0f;
        yield return new WaitForSeconds(attackCooldown);
        canAttack = true;
        swordCollider.enabled = false;
    }
    private IEnumerator Block()
    {
        if (gameManager.canBlock == true && FindAnyObjectByType<PlayerBlock>() != null)
        {
            canBlock = false;
            shieldCollider.enabled = true;
            playerAnimator.SetTrigger("Block");
            GetComponent<Health>().Invunerability();
            speed = 0f;
            yield return new WaitForSeconds(blockCooldown);
            canBlock = true;
            shieldCollider.enabled = false;
        }
        else
        {
            yield return null;
        }
        yield return null;
    }
    private IEnumerator SlashAttack()
    {
        if (gameManager.canSlash == true && slashProjectilePrefab.GetComponent<SlashProjectile>() != null)
        {
            canSlashAttack = false;
            playerAnimator.SetTrigger("SlashAttack");
            speed = 0f;
            yield return new WaitForSeconds(slashAttackCooldown);
            canSlashAttack = true;
        }
        else
        {
            yield return null;
        }
        yield return null;
    }
    private void CallProjectile()
    {
      Vector2 placement = new Vector2(transform.position.x, transform.position.y + 0.05f);
      Instantiate(slashProjectilePrefab, placement, Quaternion.identity);
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
