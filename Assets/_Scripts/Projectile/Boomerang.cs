using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Boomerang : Projectile
{
    public float CurrentSpeed;
    public float EffectTime;
    public float RotationTime;

    private float _originalSpeed;
    Vector3 _targetPos;
    Vector3 _direction;

    

    void Start()
    {
        EffectTime += Time.time;
        _originalSpeed = CurrentSpeed;
        transform.DOLocalRotate(new Vector3(0, 0, 360), RotationTime, RotateMode.LocalAxisAdd).SetLoops(-1, LoopType.Incremental);

        if (Target != null)
        {
            _targetPos = Target.transform.position;
            _direction = _targetPos - transform.position;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    void Update()
    {
        transform.Translate(_direction.normalized * CurrentSpeed * Time.deltaTime, Space.World);
       
        if (!Holder.activeSelf)
        {
            Destroy(this.gameObject);
        }
        else if (Time.time >= EffectTime) 
        {
            _direction = Holder.transform.position - transform.position;
            CurrentSpeed = Mathf.Lerp(CurrentSpeed, _originalSpeed, 0.005f);
        }
        else
        {
            CurrentSpeed = Mathf.Lerp(CurrentSpeed, 0, 0.005f);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision) 
    {
        if (collision.CompareTag(TargetTag))
        {
            
            if (Time.time < EffectTime)
            {
                CurrentSpeed -= 1f;
            }
        }
        if (Time.time >= EffectTime && collision.gameObject == Holder)
        {
            Destroy(this.gameObject);
        }    
    }

    private void OnDestroy()
    {
        transform.DOKill();
    }
}
