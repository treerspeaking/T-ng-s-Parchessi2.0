using System;
using System.Collections;
using System.Collections.Generic;
using Shun_Card_System;
using UnityEditor;
using UnityEngine;
using UnityEngine.Device;

public abstract class DragAndTargeterObject : BaseDraggableObject
{
    [SerializeField] private ITargeter _targeterObject;
    [SerializeField] private TargetType[] _targetTypes;
    
    
    bool _isDragging;
    // Start is called before the first frame update

    private void Awake()
    {
        _targeterObject = GetComponent<ITargeter>();
        
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
        var targetEntity = hit.transform.gameObject.GetComponent<ITargetee>();
        
        if (targetEntity == null || !CheckValid(targetEntity)) return false;
        
        ActionManager.Instance.ExecuteTarget(_targeterObject, targetEntity);
        return true;
    }
    
    protected virtual bool CheckValid(ITargetee dropTargetEntity)
    {
        foreach (var targetType in _targetTypes)
        {
            if (targetType == dropTargetEntity.TargetType) return true;
        }
        return false;
    }
    
}
