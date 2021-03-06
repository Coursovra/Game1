﻿using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    #region fields
    public Transform[] PatrolPoints;
    public NavMeshAgent Agent;
    public Animator Animation;
    public GameObject Coin;
    public bool IsAlive = true;
    [SerializeField]
    private Collider[] SkeletonColliderArray;
    private int _lastPoint = -1;
    private int _currentPatrolPoint;
    private float _groundDistance = 0.1f;
    private float _gravity = -9.81f;
    private float _waitAtPoint = 4f;
    private float _waitCounter;
    private float _chaseRange = 5f;
    private float _attackRange = .7f;
    private float _attackSpeed = .7f;
    private float _attackCounter;
    public enum AIState
    {
        isIdle,
        isPatrolling,
        isChasing,
        isAttacking
    }
    AIState currentState;
    #endregion
    void Start()
    {
        _currentPatrolPoint = Random.Range(0 , PatrolPoints.Length);
        _lastPoint = _currentPatrolPoint;
        _chaseRange = Random.Range(5f, 8f);
        _waitAtPoint = Random.Range(3, 9);
        SkeletonColliderArray = GetComponentsInChildren<Collider>();
        _waitCounter = _waitAtPoint;
        IsAlive = true;
    }

    void Update()
    {
        SkeletonMovement();
    }

    private void SkeletonMovement()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, PlayerController.instance.transform.position);

        if (IsAlive)
        {
            switch (currentState)
            {
                case AIState.isIdle:
                    Animation.SetBool("IsMoving", false);

                    if (_waitCounter > 0)
                    {
                        _waitCounter -= Time.deltaTime;
                    }
                    else
                    {
                        currentState = AIState.isPatrolling;
                        Agent.SetDestination(PatrolPoints[_currentPatrolPoint].position);

                    }

                    if (distanceToPlayer <= _chaseRange)
                    {
                        currentState = AIState.isChasing;
                        Animation.SetBool("IsMoving", true);
                    }

                    break;

                case AIState.isPatrolling:

                    if (Agent.remainingDistance <= .2f)
                    {
                        while(_lastPoint == _currentPatrolPoint)
                            _currentPatrolPoint = Random.Range(0 , PatrolPoints.Length);
                        _lastPoint = _currentPatrolPoint;


                        currentState = AIState.isIdle;
                        _waitCounter = _waitAtPoint;
                    }

                    if (distanceToPlayer <= _chaseRange)
                    {
                        currentState = AIState.isChasing;
                    }

                    Animation.SetBool("IsMoving", true);

                    break;

                case AIState.isChasing:

                    Agent.SetDestination(PlayerController.instance.transform.position);

                    if (distanceToPlayer <= _attackRange)
                    {
                        currentState = AIState.isAttacking;
                        Animation.SetTrigger("Attack");
                        Animation.SetBool("IsMoving", false);

                        Agent.velocity = Vector3.zero;
                        Agent.isStopped = true;

                        _attackCounter = _attackSpeed;
                    }

                    if (distanceToPlayer > _chaseRange)
                    {
                        currentState = AIState.isIdle;
                        _waitCounter = _waitAtPoint;

                        Agent.velocity = Vector3.zero;
                        Agent.SetDestination(transform.position);
                    }

                    break;

                case AIState.isAttacking:

                    transform.LookAt(PlayerController.instance.transform, Vector3.up);
                    transform.rotation = Quaternion.Euler(0f, transform.rotation.eulerAngles.y, 0f);

                    _attackCounter -= Time.deltaTime;
                    if (_attackCounter <= 0)
                    {
                        if (distanceToPlayer < _attackRange)
                        {
                            Animation.SetTrigger("Attack");
                            _attackCounter = _attackSpeed;
                        }
                        else
                        {
                            currentState = AIState.isIdle;
                            _waitCounter = _waitAtPoint;

                            Agent.isStopped = false;
                        }
                    }

                    break;
            }
        }
    }

    public IEnumerator StartDeath()
    {
        foreach (var collider in SkeletonColliderArray)
        {
            if(collider.enabled)
                collider.enabled = false;
        }
        Animation.SetTrigger("Death");
        Destroy(gameObject, 3.1f);
        yield return new WaitForSeconds(3);

        Instantiate(Coin, new Vector3(transform.position.x, transform.position.y + 1, transform.position.z), Quaternion.identity);
    }
}
 