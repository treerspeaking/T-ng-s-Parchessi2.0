using System;
using System.Collections;
using System.Collections.Generic;
using Shun_Card_System;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;
using UnityEngine.Device;

public abstract class DragAndTargeterObject : BaseDraggableObject
{
    [ShowInInspector] protected ITargeter TargeterObject;
    
    
    bool _isDragging;
    // Start is called before the first frame update

    private void Awake()
    {
        TargeterObject = GetComponent<ITargeter>();
        
    }

    public override void StartDrag()
    {
        base.StartDrag();
        ActionManager.Instance.StartHighlightTargetee(TargeterObject, TargeterObject.CheckTargeteeValid);
        
    }

    public override void EndDrag()
    {
        ActionManager.Instance.EndHighlightTargetee();
        
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
        
        if (targetEntity == null || !TargeterObject.CheckTargeteeValid(targetEntity)) return false;
        
        ActionManager.Instance.ExecuteTarget(TargeterObject, targetEntity);
        return true;
    }
    
    
}
