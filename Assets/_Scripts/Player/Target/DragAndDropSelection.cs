using System;
using System.Collections;
using System.Collections.Generic;
using _Scripts.Player.Target;
using UnityEditor;
using UnityEngine;
using UnityEngine.Device;

public abstract class DragAndDropSelection<TTargeter, TTargetee> : MonoBehaviour where TTargeter : PlayerEntity, ITargeter<TTargeter> where TTargetee : PlayerEntity, ITargetee<TTargetee>
{
    [SerializeField] private ITargeter<TTargeter> _dragObject;
    [SerializeField] private TargetType _targetType;
    
    [SerializeField] private Vector2 _defaultPosition;
    Collider2D _collider;
    bool _isDragging;
    // Start is called before the first frame update

    private void Awake()
    {
        _dragObject = GetComponent<ITargeter<TTargeter>>();
    }

    void Start()
    {
        _collider = GetComponent<Collider2D>();
    }
    private void OnEnable()
    {
        _isDragging = false;
    }
    protected virtual void CheckHit(Collider2D hit)
    {
        DropTargetEntity<TTargetee> targetEntity = hit.gameObject.GetComponent<DropTargetEntity<TTargetee>>();
        
        if (targetEntity == null || !CheckValid(targetEntity)) return;
        
        targetEntity.ExecuteDrop(_dragObject.GetTarget());
        
    }
    
    protected virtual bool CheckValid(DropTargetEntity<TTargetee> dropTargetEntity)
    {
        return _targetType == dropTargetEntity.GetTargetType();
    }
    
    
    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0) && MouseSettingManager.Instance.IsOnUI == false)
        {
            _isDragging=true;
        }
    }

    private void OnMouseUp()
    {
        if (!_isDragging) return;
        
        _isDragging = false;
        Collider2D[] overlapCircleAll = Physics2D.OverlapCircleAll(transform.position, 0.2f);
        foreach (Collider2D hit in overlapCircleAll)
        {
            CheckHit(hit);
        }
        transform.position = _defaultPosition;
    }

    private void OnMouseDrag()
    {
        Vector2 mousepos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = mousepos;
    }

    private void OnMouseDown()
    {
    }
}
