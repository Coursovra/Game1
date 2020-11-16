using UnityEngine;
using UnityEngine.AI;


public class EnemyController : MonoBehaviour
{
    public Transform[] PatrolPoints;
    public Transform _groundCheck;
    public LayerMask _groundMask;
    private float _groundDistance = 0.1f;
    public NavMeshAgent Agent;
    public Animator Animation;
    [SerializeField]
    private int _currentPatrolPoint;
    private bool _isGrounded;
    private float _gravity = -9.81f;
    private float _waitAtPoint = 2f;
    private float _waitCounter;
    private float _chaseRange = 7;
    private float _attackRange = .6f;
    private float _attackSpeed = 2f;
    private float attackCounter;
    public enum AIState
    {
        isIdle,
        isPatrolling,
        isChasing,
        isAttacking
    }

    AIState currentState;
    // Start is called before the first frame update
    void Start()
    {
        _waitCounter = _waitAtPoint;
    }

    // Update is called once per frame
    void Update()
    {

        _isGrounded = Physics.CheckSphere(_groundCheck.position, _groundDistance, _groundMask);
        var transformPosition = transform.position;
        if (!_isGrounded)
        {
            //print(1);
            //transformPosition.y -= _gravity * Time.deltaTime;
        }
        float distanceToPlayer = Vector3.Distance(transformPosition, PlayerController.instance.transform.position);

        switch (currentState)
        {
            case AIState.isIdle:
                Animation.SetBool("IsMoving", false);

                if (_waitCounter > 0)
                {
                    _waitCounter -= Time.deltaTime;
                } else
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
                    _currentPatrolPoint++;
                    if (_currentPatrolPoint >= PatrolPoints.Length)
                    {
                        _currentPatrolPoint = 0;
                    }

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

                if(distanceToPlayer <= _attackRange)
                {
                    currentState = AIState.isAttacking;
                    Animation.SetTrigger("Attack");
                    Animation.SetBool("IsMoving", false);

                    Agent.velocity = Vector3.zero;
                    Agent.isStopped = true;

                    attackCounter = _attackSpeed;
                }

                if(distanceToPlayer > _chaseRange)
                {
                    currentState = AIState.isIdle;
                    _waitCounter = _waitAtPoint;

                    Agent.velocity = Vector3.zero;
                    Agent.SetDestination(transformPosition);
                }

                break;

            case AIState.isAttacking:

                transform.LookAt(PlayerController.instance.transform, Vector3.up);
                transform.rotation = Quaternion.Euler(0f, transform.rotation.eulerAngles.y, 0f);

                attackCounter -= Time.deltaTime;
                if(attackCounter <= 0)
                {
                    if(distanceToPlayer < _attackRange)
                    {
                        Animation.SetTrigger("Attack");
                        attackCounter = _attackSpeed;
                    } else
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
 