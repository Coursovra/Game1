using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    #region fields
    public GameObject HealthParticles;
    public int HealAmount;
    private float _lifeTime = 2;
    public bool IsFullHealhPoints;
    #endregion

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Destroy(gameObject);

            if (IsFullHealhPoints)
            {
                HealthManager.instance.ResetHealth();
            }
            else
            {
                HealthManager.instance.AddHealth(HealAmount);
            }
            Destroy(Instantiate(HealthParticles, transform.position, Quaternion.identity), _lifeTime);
            AudioManager.instance.PlaySFX(8);
        }
    }
}


