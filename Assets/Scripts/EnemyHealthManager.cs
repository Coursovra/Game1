using UnityEngine;

public class EnemyHealthManager : MonoBehaviour

{
    #region fields
    private int _maxHealth = 2;
    private int _currentHealth;
    private EnemyController _skeleton;
    #endregion

    void Start()
    {
        _skeleton = GetComponent<EnemyController>();
        _currentHealth = _maxHealth;
    }

    public void TakeDamage()
    {
        _currentHealth--;
        if(_currentHealth <= 0)
        {
            AudioManager.instance.PlaySFX(5);
            _skeleton.IsAlive = false;
            StartCoroutine(_skeleton.StartDeath());
        }
        else
            AudioManager.instance.PlaySFX(6);
    }

}