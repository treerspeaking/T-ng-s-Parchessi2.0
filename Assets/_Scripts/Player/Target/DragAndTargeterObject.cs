using System;
using System.Collections;
using System.Collections.Generic;
using Shun_Card_System;
using UnityEditor;
using UnityEngine;
using UnityEngine.Device;

public abstract class DragAndTargeterObject<TTargeter> : BaseDraggableObject where TTargeter : PlayerEntity, ITargeter<TTargeter> 
{
    [SerializeField] private ITargeter<TTargeter> _targeterObject;
    [SerializeField] private TargetType[] _targetTypes;
    
    
    bool _isDragging;
    // Start is called before the first frame update

    private void Awake()
    {
        _targeterObject = GetComponent<ITargeter<TTargeter>>();

        _targeterObject.GetTarget().OnDestroy += () => { IsDestroyed = true; };

    }


    public override void EndDrag()
    {
        if (TryDrop()) return;
        base.EndDrag();
    }

    private bool TryDrop()
    {
        var mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D[] overlapCircleAll = Physics2D.RaycastAll(mousePosition, Vector2.zero);
        foreach (RaycastHit2D hit in overlapCircleAll)
        {
            if (hit.transform.gameObject == gameObject) continue;
            if (CheckHit(hit)) return true;
        }

        return false;
    }

    protected virtual bool CheckHit(RaycastHit2D hit)
    {
        var targetEntity = hit.transform.gameObject.GetComponent<TargetEntity>();
        
        if (targetEntity == null || !CheckValid(targetEntity)) return false;
        
        ActionManager.Instance.ExecuteTarget(_targeterObject.GetTarget(), targetEntity );
        return true;
    }
    
    protected virtual bool CheckValid(TargetEntity dropTargetEntity)
    {
        foreach (var targetType in _targetTypes)
        {
            if (targetType == dropTargetEntity.GetTargetType()) return true;
        }
        return false;
    }
    
    /*
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
    
    */
}
