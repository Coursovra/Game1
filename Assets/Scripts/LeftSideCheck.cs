using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftSideCheck : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            BossController.instance.RightSide = false;
            BossController.instance.LeftSide = true;
        }
    }
}
