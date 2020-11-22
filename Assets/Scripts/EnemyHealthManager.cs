using UnityEngine;

public class EnemyHealthManager : MonoBehaviour

{
    #region fields
    private int _maxHealth = 2;
    private int _currentHealth;
    #endregion

    void Start()
    {
        _currentHealth = _maxHealth;
    }

    public void TakeDamage()
    {
        _currentHealth--;
        if(_currentHealth <= 0)
        {
            AudioManager.instance.PlaySFX(5);
            EnemyController.instance.IsAlive = false;
            StartCoroutine(EnemyController.instance.StartDeath());
        }
        else
        {
            AudioManager.instance.PlaySFX(6);
        }
    }

}