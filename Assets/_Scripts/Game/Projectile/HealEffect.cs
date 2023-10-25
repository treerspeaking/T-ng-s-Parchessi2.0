using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealEffect : Projectile
{
    private void Start()
    {
        transform.rotation = Quaternion.Euler(-60, 0, 0);
    }
    void Update()
    {
        if (Target != null) { 
            transform.position = Target.transform.position; 
        }   
    }
}
