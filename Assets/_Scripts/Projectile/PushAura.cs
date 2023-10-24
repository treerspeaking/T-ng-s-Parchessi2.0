using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PushAura : MonoBehaviour
{ 
    public float LifeTime;
    public float SizeScale;
    void Start()
    {
        
        transform.DOScale(new Vector3(SizeScale, SizeScale, 1), LifeTime).SetEase(Ease.OutCubic).OnComplete(() =>
        {
            Destroy(gameObject);
        });
        
    }

    private void OnDestroy()
    {
        transform.DOKill();
    }

}
