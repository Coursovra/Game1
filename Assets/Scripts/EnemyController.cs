using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public Transform[] PatrolPoints;
    public NavMeshAgent Agent;
    public Animator Animation;
    [SerializeField]
    private int _currentPatrolPonts;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Agent.SetDestination(PatrolPoints[_currentPatrolPonts].position);
        if (Agent.remainingDistance <= .2f)
        {
            _currentPatrolPonts++;
            if (_currentPatrolPonts >= PatrolPoints.Length)
            {
                _currentPatrolPonts = 0;
            }
            Agent.SetDestination(PatrolPoints[_currentPatrolPonts].position);
        }
        Animation.SetBool("IsMoving", true);
    }
}
 