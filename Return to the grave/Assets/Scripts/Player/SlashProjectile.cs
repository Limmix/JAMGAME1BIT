using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlashProjectile : MonoBehaviour
{
    [SerializeField] private Rigidbody2D projectileRigidbody;
    [SerializeField] private Collider2D Collider;

    private float horizontalInput = 2f;
    private float speed = 7f;

    private PlayerController playerController;
    private Transform player;
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        playerController = GetComponent<PlayerController>();
        CheckPlayerSide();
    }

    public void CheckPlayerSide()
    {
        if (player.right.x > 0f)
        {
            transform.localScale = new Vector3(1, 1, 1);
            float horizontalSpeed = horizontalInput * speed;
            projectileRigidbody.velocity = new Vector2(horizontalSpeed, projectileRigidbody.velocity.y);
        }
        else if (player.right.x < 0f)
        {
            transform.localScale = new Vector3(-1, 1, 1);
            float horizontalSpeed = -horizontalInput * speed;
            projectileRigidbody.velocity = new Vector2(horizontalSpeed, projectileRigidbody.velocity.y);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Destroy(collision.gameObject);
            DestroyProjectile();

        }
        if (collision.gameObject.CompareTag("RangedEnemy"))
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
