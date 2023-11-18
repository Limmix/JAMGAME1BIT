using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Disable Scripts")]
    [SerializeField]
    private PlayerBlock playerBlock;
    [SerializeField]
    private PlayerController playerController;
    [SerializeField]
    private Transform playerTransform;
    [SerializeField] private GameObject slashProjectilePrefab;
    public bool canSlash = true;
    public bool canJump = true;
    public bool canBlock = true;

    [Header("Change AnimatorController")]
    [SerializeField]
    private RuntimeAnimatorController newAnimatorController;
    [SerializeField]
    private Animator playerAnimator;

    private void Start()
    {
        slashProjectilePrefab.GetComponent<SlashProjectile>().enabled = true;
    }

    // Update is called once per frame
    private void Update()
    {
        if (playerTransform.transform.position.x > 236f)
        {
            LoseSlashAttack();
        }
        if (playerTransform.transform.position.x > 465f)
        {
            DisableBlock();
        }
        if (playerTransform.transform.position.x > 706f)
        {
            LoseJump();
        }
    }
    private void DisableBlock()
    {
        canBlock = false;
        playerBlock.enabled = false;
        if (newAnimatorController != null)
        {
            playerAnimator.runtimeAnimatorController = newAnimatorController;
        }
    }
    private void LoseSlashAttack()
    {
        
        canSlash = false;
        slashProjectilePrefab.GetComponent<SlashProjectile>().enabled = false;
    }
    private void LoseJump()
    {
        canJump = false;
        playerController.jumpForce = 0f;

    }



}
