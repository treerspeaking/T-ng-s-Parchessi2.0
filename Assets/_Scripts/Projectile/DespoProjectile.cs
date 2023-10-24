using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DespoProjectile : Projectile
{
    public float LifeTime;
    Vector3 _targetPos;
    Vector3 _direction;
    readonly Vector3 _offset = new Vector3(0, 0.5f, 0);
    void Start()
    {
        Projectile[] bullets = GetComponentsInChildren<Projectile>();
        
        if (Target != null)
        {
            for (int i = 1; i < bullets.Length; i++)
            {
                bullets[i].Target = Target;
                bullets[i].Holder = Holder;
            }
            _targetPos = Target.transform.position + _offset;
            _direction = _targetPos - transform.position;
            RotateToDirection(_direction);
        }
        else
        {
            Destroy(this.gameObject);
        }
        Destroy(this.gameObject, LifeTime);
    }
}
