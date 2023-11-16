using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeSkeletonAttack : MonoBehaviour
{
    [SerializeField] private Collider2D swordCollider;
    [SerializeField]
    private PlayerController player;
    private void Start()
    {
        player = GetComponent<PlayerController>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && player.canBlock == true)
        {
            collision.GetComponent<Health>().TakeDamage(1);
        }
    }
}
