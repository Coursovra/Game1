using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
public class DarknessController : MonoBehaviour
{
    #region fields
    [SerializeField]
    private LayerMask _layerMask;
    private Light _light;
    public  bool PlayerInLight;
    #endregion
    void Start()
    {
        _light = GetComponent<Light>();
    }

    void Update()
    {
        List<Collider> hitColliders = new List<Collider>(Physics.OverlapSphere(transform.position, _light.range, _layerMask));Physics.OverlapSphere(transform.position, _light.range, _layerMask);
        //в коллайдере получить имя и понять на свету игрок или нет...
        //if (hitColliders.Contains("Player") || hitColliders[0].name == "GroundCheck")
            print("in light");
        //else
        {
            print("net");
        }
            //PlayerInLight = false;
        //PlayerInLight = Physics.CheckSphere(_lightTransofrm.position, _light.range, _layerMask);
        if (!PlayerInLight)
        {
            //print(1);
            //GameManager.instance.Respawn();
        }
    }

    public void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, _light.range);
    }
}
