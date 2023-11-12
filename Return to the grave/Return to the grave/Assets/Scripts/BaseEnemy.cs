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
        if (Vector2.Distance(transform.position, player.position) < detectionRange)
        {
            isPatrolling = false;
        }

        if (((Vector2)transform.position - initialPosition).sqrMagnitude >= patrolDistance * patrolDistance)
        {
            // Change direction when reaching the limit
            patrolDirection *= -1;
            initialPosition = transform.position; // Update the initial position after changing direction
        }
    }
    private void FollowPlayer()
    {
        float step = followSpeed * Time.deltaTime;
        transform.position = Vector2.MoveTowards(transform.position, player.position, step);
        Flip();
        // Check if the player is now out of the detection range to resume patrolling
        if (Vector2.Distance(transform.position, player.position) > detectionRange)
        {
            isPatrolling = true;
        }
    }
    private void Flip()
    {
        transform.right = -transform.right;
    }
}
