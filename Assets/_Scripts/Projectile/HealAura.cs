using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealAura : Projectile
{
    public float LifeTime;
    public float EffectTime;
    public float EffectRange;
    public float ScaleFactor;
    
    float _nextEffectTime;
    void Start()
    {
        transform.position = new Vector3(Random.Range(-7.5f, 7.5f), Random.Range(-4f, 4f));
        LifeTime += Time.time;
        
        Scale(ScaleFactor);
    }

    private void Scale(float scaleFactor)
    {
        transform.localScale = new Vector3(transform.localScale.x * scaleFactor, transform.localScale.y * scaleFactor, transform.localScale.z);
        EffectTime /= scaleFactor;
        EffectRange *= scaleFactor;
    }
    
    // Update is called once per frame
    void Update()
    {
        if (Time.time > _nextEffectTime)
        {
            List<GameObject> targetsInRange = new List<GameObject>();
            _nextEffectTime = Time.time + EffectTime;
            GameObject[] targets = GameObject.FindGameObjectsWithTag(TargetTag);

            foreach (GameObject t in targets)
            {
                if (t.activeSelf && Vector2.Distance(t.transform.position, transform.position) <= EffectRange)
                {
                    targetsInRange.Add(t);
                }
            }

            if (targetsInRange.Count > 0) { 
                int randomIndex = Random.Range(0, targetsInRange.Count);
                HealingTarget(targetsInRange[randomIndex]);

                
            }
        }

        if (Time.time > LifeTime)
        {
            Destroy(this.gameObject);
        }
    }

    private void HealingTarget(GameObject targetToHeal)
    {
        // TODO : Add a healing effect to the target
        
    }
}
