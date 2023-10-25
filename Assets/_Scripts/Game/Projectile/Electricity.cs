using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Electricity : Projectile
{
    public float LifeTime;
    public float EffectRadius;
    public float EffectTime;
    

    // Start is called before the first frame update
    void Start()
    {
        if (Target != null)
        {
            transform.position = Target.transform.position;
        }
        else
        {
            Destroy(this.gameObject);
        }

        Invoke("EffectEnemy", EffectTime);

        Destroy(this.gameObject, LifeTime);
    }

    void EffectEnemy()
    {
        GameObject[] targets = GameObject.FindGameObjectsWithTag(TargetTag);
        
        foreach (GameObject t in targets)
        {
            if (Vector2.Distance(t.transform.position, transform.position) <= EffectRadius)
            {
                // TODO: Add a stun effect to the enemy
                
            }
        }
    }
}
