using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
public class DarknessController : MonoBehaviour
{
    #region fields
    [SerializeField]
    public LayerMask LayerMask;
    private Light _light;
    private List<Collider> _hitColliders;
    private bool _inLight;
    public int id;
    static bool[] InLight = new bool[9];
    private int _counter = 0;
    #endregion
    void Start()
    {
        _light = GetComponent<Light>();
    }

    private void Awake()
    {
        InLight[id] = false;
        InLight[0] = true;
    }

    void Update()
    {
        PlayerState();
    }

    private void PlayerState()
    {
        _hitColliders = new List<Collider>(Physics.OverlapSphere(transform.position, _light.range, LayerMask));
        if (_hitColliders.Count > 0)
        {
            _inLight = true;
        }
        else
        {
            _inLight = false;
        }
        InLight[id] = _inLight;
        _counter = 0;
        foreach (var VARIABLE in InLight)
        {
            if (VARIABLE)
            {
                PlayerController.instance.InDarkness = false;
                break;
            }
            if(_counter == InLight.Length-1)
            {
                PlayerController.instance.InDarkness = true;
            }
            _counter++;
        }
    }

    public void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, _light.range);
    }
}
//print("0" + ": " + InLight[0] + ",  " + "1" + ": " + InLight[1] + ",  " + "2" + ": " + InLight[2] + ",  " +
//"3" + ": " + InLight[3] + ",  " + "4" + ": " + InLight[4] + ",  " + "5" + ": " + InLight[5] + ",  " +
//"6" + ": " + InLight[6] + ",  " + "7" + ": " + InLight[7] + ",  " + "8" + ": " + InLight[8]);