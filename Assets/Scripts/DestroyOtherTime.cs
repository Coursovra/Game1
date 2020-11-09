using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOtherTime : MonoBehaviour
{
    // Start is called before the first frame update
    public float lifetime;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Destroy(gameObject, lifetime);
    }
}
