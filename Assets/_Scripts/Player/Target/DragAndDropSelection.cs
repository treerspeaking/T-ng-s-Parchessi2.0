using System;
using System.Collections;
using System.Collections.Generic;
using _Scripts.Player.Target;
using Shun_Card_System;
using UnityEditor;
using UnityEngine;
using UnityEngine.Device;

public abstract class DragAndDropSelection<TTargeter, TTargetee> : BaseDraggableObject where TTargeter : PlayerEntity, ITargeter<TTargeter> where TTargetee : PlayerEntity, ITargetee<TTargetee>
{
    [SerializeField] private ITargeter<TTargeter> _dragObject;
    [SerializeField] private TargetType _targetType;
    [SerializeField] private Vector2 _defaultPosition;
    [SerializeField] private bool _isUseOutsourceInteraction = false;
    
    bool _isDragging;
    // Start is called before the first frame update

    private void Awake()
    {
        _dragObject = GetComponent<ITargeter<TTargeter>>();
    }

    void Start()
    {
        
    }
    private void OnEnable()
    {
        _isDragging = false;
    }

    public void Drop()
    {
        Collider2D[] overlapCircleAll = Physics2D.OverlapCircleAll(transform.position, 0.2f);
        foreach (Collider2D hit in overlapCircleAll)
        {
            CheckHit(hit);
        }
        transform.position = _defaultPosition;
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
        if (_isUseOutsourceInteraction) return;

        if (Input.GetMouseButtonDown(0) && MouseSettingManager.Instance.IsOnUI == false)
        {
            _isDragging=true;
        }
    }

    private void OnMouseUp()
    {
        if (_isUseOutsourceInteraction) return;
        if (!_isDragging) return;
        
        _isDragging = false;
        Drop();
    }
    
    private void OnMouseDrag()
    {
        if (_isUseOutsourceInteraction) return;
        Vector2 mousepos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = mousepos;
    }

    private void OnMouseDown()
    {
    }
}
