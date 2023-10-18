using DG.Tweening;
using Shun_Card_System;
using UnityEngine;


public class HandCardHolder : BaseDraggableObjectHolder
{
    [Header("Tween")] 
    [SerializeField] private float _addDuration = 0.25f;
    [SerializeField] private Ease _addEase = Ease.InCubic;
    
    protected override void AttachCardVisual()
    {
        Transform cardTransform = DraggableObject.transform;
        cardTransform.DOLocalMove(Vector3.zero, _addDuration).SetEase(_addEase)
            .OnComplete(DraggableObject.EnableDrag);

        
        cardTransform.DOScale(Vector3.one , _addDuration).SetEase(_addEase);
    }
    
    
}
