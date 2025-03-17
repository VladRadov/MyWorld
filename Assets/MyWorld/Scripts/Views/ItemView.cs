using UnityEngine;

using UniRx;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(DragAndDropComponent))]
public class ItemView : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private Rigidbody2D _rigidbody2D;
    [SerializeField] private DragAndDropComponent _dragAndDropComponent;

    private void Start()
    {
        _dragAndDropComponent.DraggingStart.Subscribe(_ => { SetRigidbodyType(RigidbodyType2D.Kinematic); });
        _dragAndDropComponent.DraggingEnd.Subscribe(_ => { SetRigidbodyType(RigidbodyType2D.Dynamic); });
    }

    private void SetRigidbodyType(RigidbodyType2D bodyType)
        => _rigidbody2D.bodyType = bodyType;

    private void OnValidate()
    {
        if (_rigidbody2D == null)
            _rigidbody2D = GetComponent<Rigidbody2D>();

        if (_dragAndDropComponent == null)
            _dragAndDropComponent = GetComponent<DragAndDropComponent>();
    }
}
