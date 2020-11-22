using UnityEngine;

public class DestroyOtherTime : MonoBehaviour
{
    public float _lifetime;

    void Update()
    {
        Destroy(gameObject, _lifetime);
    }
}
