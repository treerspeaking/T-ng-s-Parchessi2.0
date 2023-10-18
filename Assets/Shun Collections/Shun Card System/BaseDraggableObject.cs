
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Shun_Card_System
{
    [RequireComponent(typeof(Collider2D))]
    public class BaseDraggableObject : MonoBehaviour, IMouseDraggable, IMouseHoverable
    {
        
        public bool IsDraggable; 
        public bool IsDragging { get; protected set; }

        public bool IsHoverable;
        public bool IsHovering { get; protected set; }
        
        public virtual void StartDrag()
        {
            IsDragging = true;
        }

        public virtual void EndDrag()
        {
            IsDragging = false;
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
        
        public virtual void DisableDrag()
        {
            if (!IsDraggable) return;
            IsDraggable = false;
            if (IsHovering) EndHover();
        }
        
        public virtual void EnableDrag()
        {
            if (IsDraggable) return;
            IsDraggable = true;
        }

        
    }
}
