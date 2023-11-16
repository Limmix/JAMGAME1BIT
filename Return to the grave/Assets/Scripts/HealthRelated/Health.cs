using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [Header("Health")]
    private float startingHealth = 3f;
    public float currentHealth { get; private set; }
    [SerializeField] private Animator animator;
    private bool dead;

    [Header("Invunerability")]
    [SerializeField] private float invunerability;



    private void Awake()
    {
        currentHealth = startingHealth;
    }
    public void TakeDamage(float damage)
    {
        currentHealth = Mathf.Clamp(currentHealth - damage, 0, startingHealth);

        if (currentHealth > 0)
        {
            animator.SetTrigger("Hurt");
            StartCoroutine(Invunerability());
        }
        else
        {
            if (!dead)
            {
                animator.SetTrigger("Dead");
                GetComponent<PlayerController>().enabled = false;
                dead = true;
            }

        }
    }
    public void Heal(float heal)
    {
        currentHealth = Mathf.Clamp(currentHealth + heal, 0, startingHealth);
    }
    public IEnumerator Invunerability()
    {
        Physics2D.IgnoreLayerCollision(9, 10, true);
        yield return new WaitForSeconds(1f);
        Physics2D.IgnoreLayerCollision(9, 10, false);
    }
}
