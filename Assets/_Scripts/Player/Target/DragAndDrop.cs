using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Device;

public class DragAndDrop : MonoBehaviour
{
    [SerializeField] private MonoBehaviour _dragObject; 
    [SerializeField] private Vector2 _defaultPosition;
    Collider2D _collider;
    bool _isDragging;
    // Start is called before the first frame update
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
            Collider2D[] pieces = Physics2D.OverlapCircleAll(transform.position, 0.2f);
            foreach (Collider2D piece in pieces)
            {

                DropTarget movePiece = piece.gameObject.GetComponent<DropTarget>();
                if (movePiece != null)
                {
                    Debug.Log("MOVE");
                    movePiece.ExecuteDrop(this);
                    return;
                }
            }
            transform.position = _defaultPosition;
        }
    }
    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _isDragging=true;
        }
    }
}
