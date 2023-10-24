using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoonRing : Projectile
{
    public float EffectSpeed;

    void Update()
    {
        // Fix Noah Moonring not Destroy
        if (Holder != null)
        {
            transform.position = Holder.transform.position;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(TargetTag))
        {
            // TODO
            
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag(TargetTag))
        {
            // TPDP
        }
    }
}
