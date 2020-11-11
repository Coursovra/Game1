using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPuckup : MonoBehaviour
{
    public int value;
    public GameObject CoinParticles;
    private float _lifeTime = 2;
    public int SoundToPlay;

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Destroy(gameObject);
            GameManager.instance.AddCoins(value);
            Destroy(Instantiate(CoinParticles, transform.position, Quaternion.identity), _lifeTime);
            AudioManager.instance.PlaySFX(4);
        }
    }
}
