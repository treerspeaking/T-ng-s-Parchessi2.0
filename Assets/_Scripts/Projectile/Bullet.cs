using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class Bullet: Projectile
{
    public float Speed;
    public float LifeTime;
    public ParticleSystem LeafEffect;

    Vector3 _targetPos;
    Vector3 _direction;

    

    void Start()
    {
        if (Target != null) 
        {
            _targetPos = Target.transform.position;
            _direction = _targetPos - transform.position;
            RotateToDirection(_direction);
            if (LeafEffect != null)
            {
                ParticleSystem i_leaf = Instantiate(LeafEffect, transform.position, transform.rotation);
                Destroy(i_leaf.gameObject, 0.35f);
            }
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
    }


    //Run 1 times at the time of collision between 2 objects.
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(TargetTag))
        {
            // Deals target damage
            
            Destroy(gameObject);
        }
    }
}
