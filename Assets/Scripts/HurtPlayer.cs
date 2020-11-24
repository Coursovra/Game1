using UnityEngine;
public class HurtPlayer : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            HealthManager.instance.Hurt();
    }
}