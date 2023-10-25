using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiderProjectile : Projectile
{
    public float ExplodeRange;
    public GameObject ExplodeHit;
    public float LifeTime;


    private void Start()
    {
        LifeTime += Time.time;
        
    }

    private void Update()
    {
        if (Time.time >= LifeTime)
        {
            TakeDamageEnemyNearby();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            TakeDamageEnemyNearby();
        }
    }

    void TakeDamageEnemyNearby()
    {
        GameObject[] targets = GameObject.FindGameObjectsWithTag(TargetTag);
        foreach (GameObject t in targets)
        {
            if (Vector2.Distance(transform.position, t.transform.position) <= ExplodeRange)
            {
                // TODO : Do something
            }
        }
        Instantiate(ExplodeHit, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
