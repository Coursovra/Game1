using UnityEngine;
public class Checkpoint : MonoBehaviour
{
    public GameObject CpOn, CpOff;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (!CpOn.activeSelf)
            {
                AudioManager.instance.PlaySFX(3);
                GameManager.instance.SetSpawnPoint(transform.position);
                Checkpoint[] allCP = FindObjectsOfType<Checkpoint>();
                for (int i = 0; i < allCP.Length; i++)
                {
                    allCP[i].CpOff.SetActive(true);
                    allCP[i].CpOn.SetActive(false);
                }
                CpOff.SetActive(false);
                CpOn.SetActive(true);
            }
        }
    }
}