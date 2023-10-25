using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orb : Projectile
{
    public float Speed;
    public float LifeTime;
    public float EffectTime;
    public float EffectRadius;

    Vector3 _targetPos;
    Vector3 _direction;

    float _nextEffectTime;

    void Start()
    {
        
        if (Target != null)
        {
            _targetPos = Target.transform.position;
            _direction = _targetPos - transform.position;
        }
        else
        {
            Destroy(this.gameObject);
        }
        Destroy(this.gameObject, LifeTime);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(_direction.normalized * Speed * Time.deltaTime, Space.World);

        GameObject[] targets = GameObject.FindGameObjectsWithTag(TargetTag);

        if (Time.time >= _nextEffectTime)
        {
            _nextEffectTime = Time.time + EffectTime;

            foreach (GameObject t in targets)
            {
                if (Vector2.Distance(t.transform.position, transform.position) <= EffectRadius)
                {
                    // TODO 
                }
            }
        }
    }
}
