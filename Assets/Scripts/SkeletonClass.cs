using UnityEngine;
using UnityEngine.AI;

namespace DefaultNamespace
{
    public class SkeletonClass
    {
        public int Id;
        public int HealthPoints;
        public NavMeshAgent Agent;
        public Animator Animation;
        public bool IsAlive = true;
    }
}