using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class Sword : Projectile
{
    public float Angle;
    public float RotateDuration;
    public Ease Ease;
    public float LifeTime;

    Vector3 _targetPos;
    Vector3 _direction;
    
    void Start()
    {
        if (Target != null)
        {
            _targetPos = Target.transform.position;
            if (_targetPos.x > transform.position.x)
            {
                Angle = -Angle;
            }
            _direction = _targetPos - transform.position;
            RotateToDirection(_direction);
            transform.Rotate(new Vector3(0, 0, -Angle), Space.Self);
            transform.DOLocalRotate(new Vector3(0, 0, Angle * 2), RotateDuration, RotateMode.LocalAxisAdd).SetEase(Ease);
            
        }
        Destroy(this.gameObject, LifeTime);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Holder.transform.position;
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
