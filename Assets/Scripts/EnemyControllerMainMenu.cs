using UnityEngine;
using UnityEngine.AI;

public class EnemyControllerMainMenu : MonoBehaviour
{
    #region field
    public Transform[] PatrolPoints;
    public NavMeshAgent Agent;
    public Animator Animation;
    public int TakenPoint = 0;
    public int CurrentPatrolPoint;
    private float _groundDistance = 0.1f;
    private float _waitAtPoint = 1;
    private float _waitCounter;
    public enum AIState
    {
        isIdle,
        isPatrolling
    }
    AIState currentState;
    #endregion



    void Start()
    {
        CurrentPatrolPoint = Random.Range(0, 11);
        TakenPoint = CurrentPatrolPoint;
        _waitCounter = _waitAtPoint;
    }


    void Update()
    {
        SkeletonMovement();
    }

    private void SkeletonMovement()
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
                    Agent.SetDestination(PatrolPoints[CurrentPatrolPoint].position);
                }

                break;

            case AIState.isPatrolling:

                if (Agent.remainingDistance <= .2f)
                {
                    CurrentPatrolPoint = GetRandromPoint(CurrentPatrolPoint);
                    if (CurrentPatrolPoint >= PatrolPoints.Length)
                    {
                        CurrentPatrolPoint = GetRandromPoint(CurrentPatrolPoint);
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
        _waitAtPoint = Random.Range(4, 11);
        int point = 0;
        if (currentPoint == 0)
            return 1;
        if (currentPoint == 1)
        {
            point = Random.Range(1, 10);
            while (point == 5 || point == 4 || point == TakenPoint)
                point = Random.Range(2, 10);
            TakenPoint = point;
            return point;
        }

        if (currentPoint == 2 || currentPoint == 3)
        {
            point = Random.Range(0, 10);
            while (point == 0 || point == 10 || point == TakenPoint)
                point = Random.Range(0, 10);
            TakenPoint = point;
            return point;
        }

        while (currentPoint == 4 || currentPoint == 5  ||point == TakenPoint)
        {
            point = Random.Range(2, 5);
            while (point == 0 || point == 10 || point == TakenPoint)
                point = Random.Range(0, 10);
            TakenPoint = point;
            return point;
        }


        for (int i = 6; i < 10; i++)
        {
            if (currentPoint == i)
            {
                point = Random.Range(0, 10);
                while (point == 0 || point == 2 || point == 3  || point == 4 || point == 5  || point == TakenPoint)
                    point = Random.Range(0, 10);
                TakenPoint = point;
                return point;
            }
        }




        return point;
    }
}

