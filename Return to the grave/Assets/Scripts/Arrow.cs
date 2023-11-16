using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    [SerializeField] private Rigidbody2D projectileRigidbody;
    [SerializeField] private Collider2D swordCollider;


    private float horizontalInput = 1f;
    private float speed = 3f;

    private Transform player;



    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Direction()
    {
        if (transform.position.x > player.position.x)
        {
            transform.localScale = new Vector3(-1, 1, 1);
            horizontalInput = -1f;
        }
        if (transform.position.x < player.position.x)
        {
            transform.localScale = new Vector3(1, 1, 1);
            horizontalInput = 1f;
        }
    }
    private void FixedUpdate()
    {

        float horizontalSpeed = horizontalInput * speed;
        projectileRigidbody.velocity =
                new Vector2(horizontalSpeed, projectileRigidbody.velocity.y);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.GetComponent<Health>().TakeDamage(1);

        }
        if (collision.gameObject.CompareTag("Wall"))
        {
            DestroyProjectile();
        }
        if (collision.gameObject.CompareTag("Platform"))
        {
            DestroyProjectile();
        }
    }
    private void DestroyProjectile()
    {
        Destroy(this.gameObject);
    }
}
