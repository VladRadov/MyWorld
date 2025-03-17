using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

using UniRx;

public class DragAndDropComponent : EventTrigger
{
    private bool _dragging;
    private Transform _transform;

    public ReactiveCommand DraggingStart = new();
    public ReactiveCommand DraggingEnd = new();

    private void Start()
    {
        _transform = transform;
        _dragging = false;

        ManagerUniRx.AddObjectDisposable(DraggingStart);
        ManagerUniRx.AddObjectDisposable(DraggingEnd);
    }

    public override void OnPointerDown(PointerEventData eventData)
    {
        DraggingStart.Execute();

        _dragging = true;
    }

    public override void OnPointerUp(PointerEventData eventData)
    {
        DraggingEnd.Execute();

        _dragging = false;
    }

    private void FixedUpdate()
    {
        if (_dragging)
        {
            float mouseX = Mouse.current.position.x.ReadValue();
            float mouseY = Mouse.current.position.y.ReadValue();

            var mousePosition = Camera.main.ScreenToWorldPoint(new Vector3(mouseX, mouseY, 0));
            _transform.position = new Vector2(mousePosition.x, mousePosition.y);
        }
    }

    private void OnDestroy()
    {
        ManagerUniRx.Dispose(DraggingStart);
        ManagerUniRx.Dispose(DraggingEnd);
    }
}
