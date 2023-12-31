using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemy : MonoBehaviour
{
    [Header("EnemyVariables")]
    [SerializeField] private float patrolDistance = 5f;
    [SerializeField] private float patrolSpeed = 2f;
    [SerializeField] private float followSpeed = 4f;
    [SerializeField] private float detectionRange = 5f;

    [SerializeField] private Animator animator;

    [Header("AttackRange")]
    [SerializeField] private Vector2 attackDetectionZone = new Vector2(2.5f, 3f);
    [SerializeField] private Collider2D attackCollider;
    [SerializeField] private LayerMask playerLayer;
    private float range = 2f;
    public bool canAttack = true;
    private float attackCooldown = 4f;

    [Header("Patrolling/Following")]
    private bool isFollowingPlayer = false;
    private bool isPatrolling = true;
    private Vector2 initialPosition;
    private int patrolDirection = 1;

    private Transform player;
    [SerializeField]
    private PlayerController playerController;
    private float cooldown = 2f;
    private bool canCollide = true;
    private void Start()
    {
        attackCollider.enabled = false;
        initialPosition = transform.position;
        player = GameObject.FindGameObjectWithTag("Player").transform;
        StartCoroutine(CheckForPlayerInAttackRange());
    }

    private void Update()
    {
        if (isPatrolling)
        {
            Patrol();
        }
        else
        {
            FollowPlayer();
        }



    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && canCollide && playerController.canBlock == true)
        {
            collision.GetComponent<Health>().TakeDamage(0);
            StartCoroutine(AttackCooldown());
        }
    }

    private void Patrol()
    {
        float step = patrolSpeed * patrolDirection * Time.deltaTime;

        // Move back and forth within the patrol distance
        transform.Translate(Vector2.right * step);

        if (Vector2.Distance(transform.position, player.position) < detectionRange)
        {
            isPatrolling = false;
            isFollowingPlayer = true;
        }
        if (!isFollowingPlayer && ((Vector2)transform.position - initialPosition).sqrMagnitude >= patrolDistance * patrolDistance)
        {
            Flip();
            // Change direction when reaching the limit
            patrolDirection *= -1;
            initialPosition = transform.position; // Update the initial position after changing direction
        }
    }

    private void FollowPlayer()
    {
        float step = followSpeed * Time.deltaTime;
        transform.position = Vector2.MoveTowards(transform.position, player.position, step);

        // Check if the player is now out of the detection range to resume patrolling
        if (transform.position.x > player.position.x)
        {
            transform.localScale = new Vector3(-1, 1, 1);
            transform.position += Vector3.left * followSpeed * Time.deltaTime;
        }
        if (transform.position.x < player.position.x)
        {
            transform.localScale = new Vector3(1, 1, 1);
            transform.position += Vector3.right * followSpeed * Time.deltaTime;
        }

    }
    private IEnumerator CheckForPlayerInAttackRange()
    {
        while (FindAnyObjectByType<PlayerController>() != null)
        {
            // Create a Raycast hit variable
            RaycastHit2D hit = Physics2D.Raycast(attackCollider.bounds.center, Vector2.right * range * transform.localScale.x, attackDetectionZone.x, playerLayer);
            // Check if the player is within the attack range
            if (hit.collider != null && hit.collider.CompareTag("Player"))
            {
                followSpeed = 0f;
                canAttack = false;
                attackCollider.enabled = true;
                animator.SetTrigger("Attack");
                yield return new WaitForSeconds(attackCooldown);
                canAttack = true;
                attackCollider.enabled = false;
            }
            yield return null;
        }
        yield return null;
    }

    private void Flip()
    {
        Vector3 scale = transform.localScale;
        scale.x *= -1; // Flip the scale horizontally
        transform.localScale = scale;
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(attackCollider.bounds.center, attackDetectionZone);
    }
    private IEnumerator KeepFollowing()
    {

        yield return new WaitForSeconds(0.5f);
        followSpeed = 1.5f;
    }
    private IEnumerator AttackCooldown()
    {
        canCollide = false;
        yield return new WaitForSeconds(cooldown);
        canCollide = true;
    }
}

