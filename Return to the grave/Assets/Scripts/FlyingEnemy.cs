using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class FlyingEnemy : MonoBehaviour
{
    [SerializeField] private Rigidbody2D enemyRigidbody;
    [SerializeField] private Transform player;
    [SerializeField] private float patrolDistance = 5f;
    [SerializeField] private float patrolSpeed = 2f;
    [SerializeField] private float detectionRange = 5f;
    [SerializeField] private float followSpeed = 4f;

    private int patrolDirection = 1;
    private bool isPatrolling = true;
    private Vector2 initialPosition;
    private bool flip;

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

    private void Patrol()
    {
        float step = patrolSpeed * patrolDirection * Time.deltaTime;

        // Move back and forth within the patrol distance
        transform.Translate(Vector2.right * step);

        // Check if the player is within the detection range
        if (Vector2.Distance(transform.position, player.position) < detectionRange)
        {
            isPatrolling = false;
            Flip();
        }

        if (((Vector2)transform.position - initialPosition).sqrMagnitude >= patrolDistance * patrolDistance)
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
        enemyRigidbody.position = Vector2.MoveTowards(enemyRigidbody.position, player.position, step);

        if (Vector2.Distance(transform.position, player.position) > detectionRange)
        {
            isPatrolling = true;
        }
    }
    private void Flip()
    {
        if (!isPatrolling)
        {
            Vector2 facePlayer = transform.localScale;
            // Face the player horizontally
            if (player.transform.position.x > transform.position.x)
            {
                facePlayer.x = Mathf.Abs(facePlayer.x) * (flip ? -1 : 1);
            }
            else
            {
                facePlayer.x = Mathf.Abs(facePlayer.x)* -1 * (flip ? -1 : 1);
            }
            transform.localScale = facePlayer;
        }
        else
        {
            Vector2 changeDirection = transform.localScale;
            changeDirection.x *= -1;
            transform.localScale = changeDirection;
        }
    }
}
