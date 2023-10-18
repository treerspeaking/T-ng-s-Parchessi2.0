namespace Shun_Card_System
{
    public interface IMouseDraggable
    {
        public bool IsDraggable { get; }
        public bool IsDragging { get; }
        public void StartDrag();
        public void EndDrag();
        public void DisableInteractable();
        public void EnableInteractable();
    }
}