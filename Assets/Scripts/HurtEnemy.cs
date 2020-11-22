using UnityEngine;

public class HurtEnemy : MonoBehaviour
{
    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            other.GetComponent<EnemyHealthManager>().TakeDamage();
        }

    }
}
