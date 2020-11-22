using UnityEngine;

public class CoinPuckup : MonoBehaviour
{
    #region fields
    public int _value;
    public GameObject CoinParticles;
    private float _lifeTime = 2;
    #endregion
    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Destroy(gameObject);
            GameManager.instance.AddCoins(_value);
            Destroy(Instantiate(CoinParticles, transform.position, Quaternion.identity), _lifeTime);
            AudioManager.instance.PlaySFX(4);
        }
    }
}
