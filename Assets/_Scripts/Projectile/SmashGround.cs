using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmashGround : Projectile
{
    public float EffectRadius;
    public float BonusDamagePerEnemy;
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
    }
    
    void EffectEnemy()
    {
        int enemyInRange = 0;
        GameObject[] targets = GameObject.FindGameObjectsWithTag(TargetTag);

        foreach (GameObject t in targets)
        {
            if (Vector2.Distance(t.transform.position, transform.position) <= EffectRadius)
            {
                enemyInRange++;
            }
        }


        foreach (GameObject t in targets)
        {
            if (Vector2.Distance(t.transform.position, transform.position) <= EffectRadius)
            {
                // TODO : Do something
            }
        }
    }
}
