using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Device;

public class DiceDragnDrop : MonoBehaviour
{
    [SerializeField]
    Vector2 defaultpos;
    Collider2D coll;
    bool isdragging;
    // Start is called before the first frame update
    void Start()
    {
        coll = GetComponent<Collider2D>();
    }
    private void OnEnable()
    {
        isdragging = false;
    }
    // Update is called once per frame
    void Update()
    {
        HandleDrag();
        
    }
    void HandleDrag()
    {
        if (isdragging)
        {
            Vector2 mousepos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = mousepos;
        }
        if (Input.GetMouseButtonUp(0))
        {
            isdragging = false;
            Collider2D[] pieces = Physics2D.OverlapCircleAll(transform.position, 0.2f);
            foreach (Collider2D piece in pieces)
            {

                PieceMoving movepce = piece.gameObject.GetComponent<PieceMoving>();
                if (movepce != null)
                {
                    Debug.Log("MOVE");
                    movepce.Move(6);
                    return;
                }
            }
            transform.position = defaultpos;
        }
    }
    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            isdragging=true;
        }
    }
}
