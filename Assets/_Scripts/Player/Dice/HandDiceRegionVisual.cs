
using System;
using _Scripts.Scriptable_Objects;
using Shapes;
using UnityEngine;

public class HandDiceRegionVisual : MonoBehaviour
{
    [SerializeField] PlayerPaletteDescription _playerPaletteDescription;
    
    [SerializeField] ShapeRenderer _outline;
    [SerializeField] ShapeRenderer _background;


    private void Start()
    {
        LoadVisual();
    }

    public void LoadVisual()
    {
        if (_playerPaletteDescription == null)
        {
            return;
        }
        
        _outline.Color = _playerPaletteDescription.PrimaryOutlineColor;
        _background.Color = _playerPaletteDescription.PrimaryColor;
        
    }
    
    
}
