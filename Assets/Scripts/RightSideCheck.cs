using UnityEngine;

public class RightSideCheck : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            BossController.instance.RightSide = true;
            BossController.instance.LeftSide = false;
        }
    }
}
