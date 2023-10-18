namespace Shun_Card_System
{
    public interface IMouseDraggable
    {
        public bool IsDragging { get; }
        public void StartDrag();
        public void EndDrag();
        public void DisableDrag();
        public void EnableDrag();
    }
}