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

    public override bool StartDrag()
    {
        ActionManager.Instance.StartHighlightTargetee(TargeterObject, TargeterObject.CheckTargeteeValid);
        return base.StartDrag();
    }

    public override bool EndDrag()
    {
        ActionManager.Instance.EndHighlightTargetee();

        if( TryDrop() )
            MouseInput.ForceEndDragAndDetachTemporary();
        

        return base.EndDrag();

    }

    private bool TryDrop()
    {
        var mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D[] overlapCircleAll = Physics2D.RaycastAll(mousePosition, Vector2.zero);
        foreach (RaycastHit2D hit in overlapCircleAll)
        {
            if (hit.transform.gameObject == gameObject) continue;
            var targetee = CheckHit(hit);
            if (targetee != null)
            {
                ActionManager.Instance.ExecuteTarget(TargeterObject, targetee);
                return true;
            }
        }

        return false;
    }

    protected virtual ITargetee CheckHit(RaycastHit2D hit)
    {
        var targetee = hit.transform.gameObject.GetComponent<ITargetee>();
        
        if (targetee == null || !TargeterObject.CheckTargeteeValid(targetee)) return null;
        
        return targetee;
    }
    
    
}
