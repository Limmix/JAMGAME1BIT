using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Tilemaps;
using UnityEngine;
using UnityEngine.UIElements;

public class BaseEnemy : MonoBehaviour
{
    [SerializeField] private Rigidbody2D enemyRigidbody2D;
    [SerializeField] private Transform player;
    [SerializeField] private float patrolDistance = 5f;
    [SerializeField] private float patrolSpeed = 2f;
    [SerializeField] private float detectionRange = 5f;
    [SerializeField] private float followSpeed = 4f;

    private bool isPatrolling = true;
    private Vector2 initialPosition;
    private int patrolDirection = 1;
    private bool isFollowingPlayer = false;

    private void Start()
    {
        initialPosition = transform.position;
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
        if (Vector2.Distance(transform.position, player.position) <= detectionRange)
        {
            isPatrolling = false;
            isFollowingPlayer = true;
        }

        if (isFollowingPlayer && Vector2.Distance(transform.position, player.position) > detectionRange)
        {
            isFollowingPlayer = false;
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
            transform.localScale = new Vector3(-1, 2.5f, 1);
            transform.position += Vector3.left * followSpeed * Time.deltaTime;
        }
        if (transform.position.x < player.position.x)
        {
            transform.localScale = new Vector3(1, 2.5f, 1);
            transform.position += Vector3.right * followSpeed * Time.deltaTime;
        }
        if (Vector2.Distance(transform.position, player.position) > detectionRange)
        {
            if (transform.position.x > player.position.x)
            {
                transform.localScale = new Vector3(1, 2.5f, 1);
            }
            if (transform.position.x < player.position.x)
            {
                transform.localScale = new Vector3(-1, 2.5f, 1);
            }
            isPatrolling = true;
        }
    }

    private void Flip()
    {
        Vector3 scale = transform.localScale;
        scale.x *= -1; // Flip the scale horizontally
        transform.localScale = scale;
    }
}



