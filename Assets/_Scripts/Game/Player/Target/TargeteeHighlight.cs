using System;
using UnityEngine;

namespace _Scripts.Player.Target
{
    public class TargeteeHighlight : MonoBehaviour
    {
        private ITargetee _targetee;

        private void Awake()
        {
            _targetee = GetComponent<ITargetee>();
        }
        
        
        
    }
}