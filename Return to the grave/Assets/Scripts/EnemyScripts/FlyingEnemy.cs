using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class FlyingEnemy : MonoBehaviour
{
    [Header("EnemyVariables")]
    [SerializeField] private float patrolDistance = 5f;
    [SerializeField] private float patrolSpeed = 2f;
    [SerializeField] private float detectionRange = 5f;
    [SerializeField] private float followSpeed = 4f;
    [SerializeField] private Animator animator;

    private int patrolDirection = 1;
    private bool isPatrolling = true;
    private bool isFollowingPlayer = false;
    private Vector2 initialPosition;
    private Transform player;

    private void Start()
    {
        initialPosition = transform.position;
        player = GameObject.FindGameObjectWithTag("Player").transform;
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
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.GetComponent<Health>().TakeDamage(1);
        }

    }
    private void Patrol()
    {
        float step = patrolSpeed * patrolDirection * Time.deltaTime;

        // Move back and forth within the patrol distance
        transform.Translate(Vector2.right * step);

        // Check if the player is within the detection range
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
        animator.SetTrigger("Following");
        float step = followSpeed * Time.deltaTime;
        Vector2 targetPosition = Vector2.MoveTowards(transform.position, player.position, step);
        transform.position = new Vector3(targetPosition.x, targetPosition.y, transform.position.z);

        if (transform.position.x > player.position.x)
        {
            transform.localScale = new Vector3(1, 1, 1);
            transform.position += Vector3.left * followSpeed * Time.deltaTime;
        }
        if (transform.position.x < player.position.x)
        {
            transform.localScale = new Vector3(-1, 1, 1);
            transform.position += Vector3.right * followSpeed * Time.deltaTime;
        }
    }
    private void Flip()
    {
        Vector3 scale = transform.localScale;
        scale.x *= -1; // Flip the scale horizontally
        transform.localScale = scale;
    }
}
