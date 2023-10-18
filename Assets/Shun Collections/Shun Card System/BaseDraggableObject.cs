
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Shun_Card_System
{
    [RequireComponent(typeof(Collider2D))]
    public class BaseDraggableObject : MonoBehaviour, IMouseDraggable , IMouseHoverable
    {
        [SerializeField]
        private bool _interactable;
        public bool IsDraggable { get => _interactable; protected set => _interactable = value; }
        public bool IsDragging { get; }
        
        public bool IsHoverable { get; }
        public bool IsHovering { get; protected set; }
        
        public void StartDrag()
        {
            throw new System.NotImplementedException();
        }

        public void EndDrag()
        {
            throw new System.NotImplementedException();
        }
        
        [SerializeField] protected bool ActivateOnValidate = false;
        

        private void OnValidate()
        {
            if (ActivateOnValidate) ValidateInformation();
        }
        
        protected virtual void ValidateInformation()
        {
            
        }
        
        public virtual void StartHover()
        {
            IsHovering = true;
        }

        public virtual void EndHover()
        {
            IsHovering = false;
        }
        
        public virtual void DisableInteractable()
        {
            if (!IsDraggable) return;
            IsDraggable = false;
            if (IsHovering) EndHover();
        }
        
        public virtual void EnableInteractable()
        {
            if (IsDraggable) return;
            IsDraggable = true;
        }

        
    }
}
