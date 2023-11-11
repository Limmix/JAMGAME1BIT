using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEnemy : MonoBehaviour
{
    [SerializeField] private Rigidbody2D enemyRigidbody;
    [SerializeField] private Transform player;

    private float patrolHeight = 5f;
    private float patrolWidth = 5f;
    private float patrolSpeed = 2f;
    private float descendSpeed = 2f;
    private float detectionRange = 5f;
    private float followSpeed = 4f;

    private bool isPatrolling = true;
    private Vector2 initialPosition;

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
            Descend();
            FollowPlayer();
        }
    }

    private void Patrol()
    {
        float horizontalStep = patrolSpeed * Time.deltaTime;
        float verticalStep = Mathf.Sin(Time.time * patrolSpeed) * patrolHeight * Time.deltaTime;

        transform.Translate(new Vector2(horizontalStep, verticalStep));

        if (Vector2.Distance(transform.position, player.position) < detectionRange)
        {
            isPatrolling = false;
        }

        if (Mathf.Abs(transform.position.x - initialPosition.x) >= patrolWidth)
        {
            // Change direction when reaching the patrol width limit
            patrolSpeed *= -1;
        }
    }

    private void Descend()
    {
        float step = descendSpeed * Time.deltaTime;
        transform.Translate(Vector2.down * step);
    }

   private void FollowPlayer()
    {
        float step = followSpeed * Time.deltaTime;
        transform.position = Vector2.MoveTowards(transform.position, player.position, step);

        if (Vector2.Distance(transform.position, player.position) > detectionRange)
        {
            isPatrolling = true;
        }
    }
}
