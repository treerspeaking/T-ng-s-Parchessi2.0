using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ExploderBullet : MonoBehaviour
{
    
    public float Speed;
    public float MoveTime;
    
    private void Start()
    {
        transform.DOScaleX(0.25f, 0.1f).SetEase(Ease.Linear).SetLoops(4, LoopType.Yoyo);
        MoveTime += Time.time;
        Destroy(gameObject, 4f);
        
    }

    private void Update()
    {
        if (Time.time > MoveTime)
        {
            transform.Translate(Vector3.right * Speed * Time.deltaTime, Space.Self);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Champion"))
        {
            // TODO : Do something
            
            
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        transform.DOKill();
    }
}
