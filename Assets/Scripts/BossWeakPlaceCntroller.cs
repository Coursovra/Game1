using UnityEngine;

public class BossWeakPlaceCntroller : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && HealthManager.instance.InvincCounter <= 0)
        {
            StartCoroutine(BossController.instance.HurtBoss());
        }
    }
}
