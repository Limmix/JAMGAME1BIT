using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header ("Disable Scripts")]
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

    [Header("Change CameraLimiterPoints")]
    [SerializeField]
    private PolygonCollider2D polygonCollider;
    private Vector2[] initialPoints;

    [Header("Positions to trigger events")]
    public float threshold1 = 80f;
    public float threshold2 = 100f;
    private Dictionary<float, bool> thresholdStatus = new Dictionary<float, bool>();

    private void Start()
    {
        slashProjectilePrefab.GetComponent<SlashProjectile>().enabled = true;
        initialPoints = polygonCollider.points;

        thresholdStatus.Add(threshold1, false);
        thresholdStatus.Add(threshold2, false);
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            DisableBlock();
        }
        foreach (var threshold in thresholdStatus.Keys)
        {
            if (transform.position.x > threshold && !thresholdStatus[threshold])
            {
                ChangeColliderPoints(threshold);
                thresholdStatus[threshold] = true;
            }
        }
    }
    private void DisableSlashAttack()
    {
        canSlash = false;
        slashProjectilePrefab.GetComponent<SlashProjectile>().enabled = false;
    }
    private void DisableBlock()
    {
        canBlock = false;
        playerBlock.enabled = false;
        if (newAnimatorController != null)
        {
            // Change the animator controller
            playerAnimator.runtimeAnimatorController = newAnimatorController;
        }
    }
    private void LoseSlashAttackScene()
    {
            // Set points using direct assignment
            polygonCollider.points = new Vector2[]
            {
            new Vector2(0f, 0f),
            new Vector2(1f, 0f),
            new Vector2(1f, 1f),
            new Vector2(0f, 1f)
            };
    }

    private void LoseBlockScene()
    {
        polygonCollider.points = new Vector2[]
        {
            new Vector2(-1f, -1f),
            new Vector2(0f, -1f),
            new Vector2(0f, 0f),
            new Vector2(-1f, 0f)
        };
    }
    private void LoseJumpScene()
    {
        
        polygonCollider.points = new Vector2[]
        {
            new Vector2(0f, 0f),
            new Vector2(1f, 0f),
            new Vector2(1f, 1f),
            new Vector2(0f, 1f)
        };
    }
    private void FinalScene()
    {
        polygonCollider.points = new Vector2[]
        {
            new Vector2(0f, 0f),
            new Vector2(1f, 0f),
            new Vector2(1f, 1f),
            new Vector2(0f, 1f)
        };
    }

    private void StartingCameraLimiter()
    {
        polygonCollider.points = initialPoints;
    }
    private void ChangeColliderPoints(float threshold)
    {
        // Implement your logic to change collider points based on the threshold
        if (threshold == threshold1)
        {
           DisableSlashAttack();
        }
        else if (threshold == threshold2)
        {
           StartingCameraLimiter();
        }
    }
}
