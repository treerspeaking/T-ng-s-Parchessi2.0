using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DespoBullet : Projectile
{
    public float Speed;

    private void Update()
    {
        transform.Translate(Vector3.right * Speed * Time.deltaTime, Space.Self);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(TargetTag))
        {
            // TODO : Do something
        }
    }
}
