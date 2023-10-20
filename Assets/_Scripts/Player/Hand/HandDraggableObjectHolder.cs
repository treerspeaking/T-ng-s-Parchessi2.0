using DG.Tweening;
using Shun_Card_System;
using UnityEngine;


public class HandDraggableObjectHolder : BaseDraggableObjectHolder
{
    [Header("Tween")] 
    [SerializeField] private float _addDuration = 0.25f;
    [SerializeField] private Ease _addEase = Ease.InCubic;
    
    private Tween _moveTween;
    private Tween _scaleTween;

    protected override void AttachCardVisual()
    {
        Transform cardTransform = DraggableObject.transform;

        _moveTween = cardTransform.DOLocalMove(Vector3.zero, _addDuration).SetEase(_addEase)
            .OnComplete(DraggableObject.EnableDrag);

        _scaleTween = cardTransform.DOScale(Vector3.one, _addDuration).SetEase(_addEase);

        DraggableObject.OnDestroy += KillTween;
    }

    private void KillTween(BaseDraggableObject baseDraggableObject)
    {
        if (_moveTween != null && _moveTween.IsActive()) _moveTween.Kill();
        if (_scaleTween != null && _scaleTween.IsActive()) _scaleTween.Kill();
    }

    protected override void DetachCardVisual()
    {
        // Check if the tweens are active and kill them if necessary
        if (_moveTween != null && _moveTween.IsActive()) _moveTween.Kill();
        if (_scaleTween != null && _scaleTween.IsActive()) _scaleTween.Kill();

        DraggableObject.OnDestroy -= KillTween;
        
        // Additional code to move the card back to its original position, for example:
        // Transform cardTransform = DraggableObject.transform;
        // cardTransform.DOLocalMove(originalPosition, _addDuration).SetEase(_addEase);
    }
}
