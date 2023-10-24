using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class PoisonSlash : Projectile
{
    public Ease Ease;
    public float LifeTime;
    public float Distance;
    public float EffectTime;

    Vector3 _targetPos;
    Vector3 _direction;

    
    // Start is called before the first frame update
    void Start()
    {
        
        if (Target != null)
        {
            _targetPos = Target.transform.position;
            _direction = _targetPos - transform.position + new Vector3(0f, 0.5f, 0f);
            RotateToDirection(_direction);
            transform.DOMove(transform.position + _direction.normalized * Distance, LifeTime).SetEase(Ease).OnComplete(() =>
            {
                GetComponent<BoxCollider2D>().enabled = false;
            });
            
        }
        else
        {
            Destroy(this.gameObject);
        }
        Destroy(this.gameObject, LifeTime + 5f);
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(TargetTag))
        {
            // TODO:
            
            
        }
    }

    
    private void OnDestroy()
    {
        transform.DOKill();
    }
}
