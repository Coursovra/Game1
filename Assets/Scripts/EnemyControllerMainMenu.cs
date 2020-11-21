using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;


public class EnemyControllerMainMenu : MonoBehaviour
{
    public static EnemyControllerMainMenu instance;
    public Transform[] PatrolPoints;
    public NavMeshAgent Agent;
    public Animator Animation;
    [SerializeField]
    private int _currentPatrolPoint;
    private float _groundDistance = 0.1f;
    private float _waitAtPoint = 1;
    private float _waitCounter;

    public enum AIState
    {
        isIdle,
        isPatrolling
    }

    AIState currentState;
    void Start()
    {
        _waitCounter = _waitAtPoint;
    }

    // Update is called once per frame
    void Update()
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

                break;

            case AIState.isPatrolling:

                if (Agent.remainingDistance <= .1f)
                {
                    _currentPatrolPoint = GetRandromPoint(_currentPatrolPoint);
                    if (_currentPatrolPoint >= PatrolPoints.Length)
                    {
                        _currentPatrolPoint = GetRandromPoint(_currentPatrolPoint);
                    }

                    currentState = AIState.isIdle;
                    _waitCounter = _waitAtPoint;
                }

                Animation.SetBool("IsMoving", true);

                break;
        }

    }

    private int GetRandromPoint(int currentPoint)
    {
        if (currentPoint == 0)
            return 1;
        int point = Random.Range(0, 21);
        if(currentPoint == 0 && point == 2 || currentPoint == 2 && point == 0 || (PatrolPoints[currentPoint].position - PatrolPoints[point].position).magnitude > 35 || currentPoint == point )
        {
            point = Random.Range(0, 21);
            //print("q");
            //print(point);
            //print(currentPoint);
        }
        else
        {
            return GetRandromPoint(currentPoint);
        }
        return GetRandromPoint(currentPoint);

        //_waitAtPoint = Random.Range(5, 16);

        // while((PatrolPoints[currentPoint].position - PatrolPoints[point].position).magnitude > 35)
        // {
        //     print((PatrolPoints[currentPoint].position - PatrolPoints[point].position).magnitude + " vector3");
        //     point = Random.Range(0, 21);
        //     print("r");
        //     print(point);
        //     print(currentPoint);
        // }

        // if (currentPoint != point)
        //     return point;

    }

    // private float GetRange(int pointOne, int pointTwo)
    // {
    //     Vector3 first = PatrolPoints[pointOne].position;
    //     Vector3 second = PatrolPoints[pointTwo].position;
    //     return (Mathf.Abs(first - second));
    // }


}
