using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spear : MonoBehaviour
{
    [SerializeField] private Rigidbody2D projectileRigidbody;

    private PlayerController playerController;

    private float speed = 6f;
    private float horizontalInput = -2f;

    private void Start()
    {
        playerController = GetComponent<PlayerController>();
    }
    private void Update()
    {
        CheckPlayerPosition();
    }
    private void CheckPlayerPosition()
    {
        float horizontalSpeed =  horizontalInput* speed ;
        projectileRigidbody.velocity = new Vector2(horizontalSpeed, projectileRigidbody.velocity.y);

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && playerController.canBlock == true)
        {
            collision.GetComponent<Health>().TakeDamage(1);
            DestroyProjectile();
        }
        if (collision.gameObject.CompareTag("Wall"))
        {
            DestroyProjectile();
        }
        if (collision.gameObject.CompareTag("Platform"))
        {
            DestroyProjectile();
        }
        if (collision.gameObject.CompareTag("DestructibleWall"))
        {
            Destroy(collision.gameObject);
            DestroyProjectile();
        }
    }
    private void DestroyProjectile()
    {
        Destroy(this.gameObject);
    }
}
