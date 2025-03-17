using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

using UniRx;

public class DragAndDropComponent : EventTrigger
{
    private bool _dragging;
    private Transform _transform;

    public ReactiveCommand DraggingStartCommand = new();
    public ReactiveCommand DraggingEndCommand = new();

    private void Start()
    {
        _transform = transform;
        _dragging = false;

        ManagerUniRx.AddObjectDisposable(DraggingStartCommand);
        ManagerUniRx.AddObjectDisposable(DraggingEndCommand);
    }

    public override void OnPointerDown(PointerEventData eventData)
    {
        DraggingStartCommand.Execute();

        _dragging = true;
    }

    public override void OnPointerUp(PointerEventData eventData)
    {
        DraggingEndCommand.Execute();

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
        ManagerUniRx.Dispose(DraggingStartCommand);
        ManagerUniRx.Dispose(DraggingEndCommand);
    }
}
