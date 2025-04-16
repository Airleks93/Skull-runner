using UnityEngine;
using System.Collections;

public class EnemyManager : MonoBehaviour
{
    [Header("Rotation Settings")]
    public float rotationSpeed = 5f;

    [Header("Combat Settings")]
    public float attackCooldown = 1.5f;
    public float attackRange = 2f;

    [Header("Patrol Settings")]
    public Transform[] waypoints;
    public float speed = 2f;
    public float waitTime = 2f;

    private int currentWaypoint;
    private Animator animator;
    private bool isWaiting;
    private bool isAttacking;
    private Transform player;
    private GameManager _gameManager;

    void Start()
    {
        currentWaypoint = 0;
        animator = GetComponent<Animator>();
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        player = GameObject.FindGameObjectWithTag("whatIsPlayer_Tag")?.transform;

        if (_gameManager == null) Debug.LogError("GameManager not found!");
        if (player == null) Debug.LogError("Player not found!");

        InitializePatrol();
    }

    private void InitializePatrol()
    {
        if (waypoints.Length > 0)
        {
            animator.SetBool("isPatrolling", true);
            animator.SetBool("isIdle", false);
        }
    }

    void Update()
    {
        if (isAttacking)
        {
            RotateTowardsPlayer();
        }
        else
        {
            HandlePatrolMovement();
            CheckForPlayerInRange();
        }
    }

    private void HandlePatrolMovement()
    {
        if (waypoints.Length == 0 || isWaiting) return;

        Vector3 direction = waypoints[currentWaypoint].position - transform.position;
        direction.y = 0;

        if (direction != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }

        if (transform.position != waypoints[currentWaypoint].position)
        {
            transform.position = Vector3.MoveTowards(
                transform.position,
                waypoints[currentWaypoint].position,
                speed * Time.deltaTime
            );
        }
        else
        {
            StartCoroutine(WaitAtWaypoint());
        }
    }

    private void RotateTowardsPlayer()
    {
        if (player != null)
        {
            Vector3 directionToPlayer = player.position - transform.position;
            directionToPlayer.y = 0;

            if (directionToPlayer != Vector3.zero)
            {
                Quaternion targetRotation = Quaternion.LookRotation(directionToPlayer);
                transform.rotation = Quaternion.Slerp(
                    transform.rotation,
                    targetRotation,
                    rotationSpeed * Time.deltaTime
                );
            }
        }
    }

    private void CheckForPlayerInRange()
    {
        if (player != null && Vector3.Distance(transform.position, player.position) <= attackRange)
        {
            StartCoroutine(AttackSequence());
        }
    }

    private IEnumerator WaitAtWaypoint()
    {
        isWaiting = true;
        animator.SetBool("isPatrolling", false);
        animator.SetBool("isIdle", true);

        yield return new WaitForSeconds(waitTime);

        currentWaypoint = (currentWaypoint + 1) % waypoints.Length;
        isWaiting = false;
        animator.SetBool("isPatrolling", true);
        animator.SetBool("isIdle", false);
    }

    private IEnumerator AttackSequence()
    {
        isAttacking = true;
        animator.SetBool("isAttacking", true);
        animator.SetBool("isPatrolling", false);
        animator.SetBool("isIdle", false);

        yield return new WaitForSeconds(GetAttackAnimationLength() * 0.5f);

        OnAttackHitFrame();

        yield return new WaitForSeconds(GetAttackAnimationLength() * 0.5f);

        yield return new WaitForSeconds(attackCooldown);

        ResetAttackState();
    }

    public void OnAttackHitFrame()
    {
        ApplyDamage();
    }

    private void ApplyDamage()
    {
        if (_gameManager != null && player != null &&
            Vector3.Distance(transform.position, player.position) <= attackRange)
        {
            _gameManager.UpdateHealthLeft();
        }
    }

    private void ResetAttackState()
    {
        isAttacking = false;
        animator.SetBool("isAttacking", false);
        animator.SetBool("isPatrolling", waypoints.Length > 0);
        animator.SetBool("isIdle", waypoints.Length == 0);
    }

    private float GetAttackAnimationLength()
    {
        AnimationClip[] clips = animator.runtimeAnimatorController.animationClips;
        foreach (AnimationClip clip in clips)
        {
            if (clip.name.Contains("Attack")) 
            {
            return clip.length; 
            }
        }
        return 1f;
    }
}
