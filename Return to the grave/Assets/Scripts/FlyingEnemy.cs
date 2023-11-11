using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEnemy : MonoBehaviour
{
    [SerializeField] private Rigidbody2D enemyRigidbody;
    [SerializeField] private Transform player;
    public float patrolHeight = 5f;
    public float patrolSpeed = 2f;
    public float detectionRange = 5f;
    public float followSpeed = 4f;

    private bool isPatrolling = true;
    private Vector2 initialPosition;

     private void Start()
    {
        initialPosition = transform.position;
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        if (Vector2.Distance(transform.position, player.position) < detectionRange)
        {
            isPatrolling = false;
            FollowPlayer();
        }
        else
        {
            isPatrolling = true;
            Patrol();
        }
    }
    private void Patrol()
    {
        float verticalMovement = Mathf.Sin(Time.time * patrolSpeed) * patrolHeight * Time.deltaTime;
        enemyRigidbody.velocity = new Vector2(0f, verticalMovement);
    }

    private void FollowPlayer()
    {
        float step = followSpeed * Time.deltaTime;
        enemyRigidbody.position = Vector2.MoveTowards(enemyRigidbody.position, player.position, step);
    }



}
