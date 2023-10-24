using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterSplash : Projectile
{
    private void Start()
    {
        GameObject[] targets = GameObject.FindGameObjectsWithTag(TargetTag);

        foreach (GameObject t in targets)
        {
            if (Vector2.Distance(t.transform.position, transform.position) <= 8f)
            {
                
                // TODO : Do something
            }
        }
        Destroy(gameObject, 1.1f);
    }
}
