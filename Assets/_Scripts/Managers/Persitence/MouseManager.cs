using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MouseManager : MonoBehaviour
{
    public bool IsOnUI => EventSystem.current.IsPointerOverGameObject();
    
    [SerializeField] private Texture2D _defaultCursorTexture;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.SetCursor(_defaultCursorTexture, Vector2.zero, CursorMode.Auto);
    }


    private void Update()
    {
        
    }
}
