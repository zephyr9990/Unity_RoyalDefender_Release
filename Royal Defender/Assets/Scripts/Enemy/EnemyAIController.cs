using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAIController : MonoBehaviour, IAIController
{
    public float stoppingDistanceFromTarget = 5f;
    public float rotationSpeed = 5f;
    public float attackDelay = 3f;
    public int damageAmount = 10;
    public GameObject attackCollisionObject;

    public AudioClip[] audioClips;

    private NavMeshAgent navMeshAgent;
    private Animator animator;
    private BoxCollider attackCollider;
    private AudioSource audioSource;

    private GameObject player;
    private GameObject cube;
    private bool movementStopped;
    private float timer;
    private int attackTriggered;

    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        attackCollider = attackCollisionObject.GetComponent<BoxCollider>();
        audioSource = GetComponent<AudioSource>();
        player = GameObject.FindGameObjectWithTag("Player");
        cube = GameObject.FindGameObjectWithTag("Cube");
        movementStopped = false;
        timer = 0f;
    }

    private void Update()
    {
        timer += Time.deltaTime;
        SetAnimatorValues();

        if (movementStopped)
            return; // do nothing;

        GameObject target = FindClosestTarget();
        GoToPosition(target);

        if (target)
        {
            Vector3 toTarget = GetVectorTo(target.transform);
            if (toTarget.magnitude <= stoppingDistanceFromTarget)
            {
                SetNavMeshAgentStopped(true);
                TurnToFace(target);
                Attack();
            }
        }

    }

    private GameObject FindClosestTarget()
    {
        Vector3 toPlayer = GetVectorTo(player.transform);
        Vector3 toCube = GetVectorTo(cube.transform);

        if (toPlayer.magnitude < toCube.magnitude)
            return player; // player is closer
        else
            return cube; // cube is closer
    }

    private Vector3 GetVectorTo(Transform targetTransform)
    {
        return targetTransform.position - transform.position;
    }

    private void GoToPosition(GameObject target)
    {
        SetNavMeshAgentStopped(false);
        navMeshAgent.SetDestination(target.transform.position);
    }

    private void TurnToFace(GameObject target)
    {
        Quaternion toTargetRotation = Quaternion.LookRotation(target.transform.position - transform.position);
        float rotationSpeedSmoothing = Mathf.Min(rotationSpeed * Time.deltaTime, 1);
        transform.rotation = Quaternion.Lerp(transform.rotation, toTargetRotation, rotationSpeedSmoothing);
    }

    private void SetAnimatorValues()
    {
        animator.SetFloat("Speed", navMeshAgent.velocity.magnitude);
    }

    private void SetNavMeshAgentStopped(bool value)
    {
        navMeshAgent.isStopped = value;
    }

    private void Attack()
    {
        if (timer <= attackDelay)
        {
            return; // Must delay till next attack
        }
       
        timer = 0f;   
        attackTriggered = Random.Range(0, 2);
        if (attackTriggered == 0)
        { 
            animator.SetTrigger("BiteAttack");
        }
        else 
        { 
            animator.SetTrigger("HornAttack"); 
        }
    }

    private void AttackEvent()
    {
        attackCollider.enabled = true;
        PlayAttackSound();
    }

    private void StopAttackEvent()
    {
        attackCollider.enabled = false;
    }

    public void StopMovement()
    {
        movementStopped = true;
        navMeshAgent.speed = 0f;
        navMeshAgent.enabled = false;
    }

    public void EnableMovement()
    {
        navMeshAgent.speed = 3.5f;
        navMeshAgent.enabled = true;
        movementStopped = false;
    }

    private void PlayAttackSound()
    {
        if (attackTriggered < audioClips.Length)
        {
            audioSource.clip = audioClips[attackTriggered];
            audioSource.Play();
        }
    }
}
