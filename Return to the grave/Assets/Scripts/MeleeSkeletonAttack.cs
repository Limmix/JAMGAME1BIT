using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeSkeletonAttack : MonoBehaviour
{
    [SerializeField] private Collider2D swordCollider;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.GetComponent<Health>().TakeDamage(1);
        }
    }
}
