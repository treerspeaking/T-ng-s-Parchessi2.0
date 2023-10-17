using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Device;

public abstract class DragAndDropSelection<T> : MonoBehaviour where T : MonoBehaviour
{
    [SerializeField] private T _dragObject;
    [SerializeField] private TargetType _targetType;
    
    [SerializeField] private Vector2 _defaultPosition;
    Collider2D _collider;
    bool _isDragging;
    // Start is called before the first frame update

    private void Awake()
    {
        _dragObject = GetComponent<T>();
    }

    void Start()
    {
        _collider = GetComponent<Collider2D>();
    }
    private void OnEnable()
    {
        _isDragging = false;
    }
    // Update is called once per frame
    void Update()
    {
        HandleDrag();
        
    }
    void HandleDrag()
    {
        if (_isDragging)
        {
            Vector2 mousepos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = mousepos;
        }
        if (Input.GetMouseButtonUp(0))
        {
            _isDragging = false;
            Collider2D[] overlapCircleAll = Physics2D.OverlapCircleAll(transform.position, 0.2f);
            foreach (Collider2D hit in overlapCircleAll)
            {
                DropTarget target = hit.gameObject.GetComponent<DropTarget>();
                if (target != null && CheckValid(target))
                {
                    Debug.Log("Hit");
                    target.ExecuteDrop(_dragObject);
                    return;
                }
            }
            transform.position = _defaultPosition;
        }
    }

    protected virtual bool CheckValid(DropTarget dropTarget)
    {
        return _targetType == dropTarget.GetTargetType();
    }
    
    
    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _isDragging=true;
        }
    }
}
