﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class HurtPlayer : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")){
            AudioManager.instance.PlaySFX(5);
            HealthManager.instance.Hurt();
        }
    }
}