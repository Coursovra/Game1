using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Checkpoint : MonoBehaviour
{
    public GameObject cpOn, cpOff;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (!cpOn.activeSelf)
            {
                AudioManager.instance.PlaySFX(3);
                GameManager.instance.SetSpawnPoint(transform.position);
                Checkpoint[] allCP = FindObjectsOfType<Checkpoint>();
                for (int i = 0; i < allCP.Length; i++)
                {
                    allCP[i].cpOff.SetActive(true);
                    allCP[i].cpOn.SetActive(false);
                }
                cpOff.SetActive(false);
                cpOn.SetActive(true);
            }
        }
    }
}