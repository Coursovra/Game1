using UnityEngine;

public class LeftSideCheck : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            BossController.instance.RightSide = false;
            BossController.instance.LeftSide = true;
        }
    }
}
