using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlashProjectile : MonoBehaviour
{
    [SerializeField] private Rigidbody2D projectileRigidbody;
    [SerializeField] private Collider2D Collider;

    private float horizontalInput = 1f;
    private float speed = 3f;

    private void FixedUpdate()
    {
        float horizontalSpeed = horizontalInput * speed;
        projectileRigidbody.velocity =
                new Vector2(horizontalSpeed, projectileRigidbody.velocity.y);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Destroy(collision.gameObject);
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
    }
    private void DestroyProjectile()
    {
        Destroy(this.gameObject);
    }
}
