using UnityEngine;

using UniRx;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(DragAndDropComponent))]
[RequireComponent(typeof(EffectIncreaseComponent))]
public class ItemView : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private Rigidbody2D _rigidbody2D;
    [SerializeField] private DragAndDropComponent _dragAndDropComponent;
    [SerializeField] private EffectIncreaseComponent _effectIncreaseComponent;

    public DragAndDropComponent DragAndDropComponent => _dragAndDropComponent;
    public EffectIncreaseComponent EffectIncreaseComponent => _effectIncreaseComponent;
    public ReactiveCommand PlaceItemTriggerStayCommand = new();
    public ReactiveCommand DestroyItemCommand = new();

    public void SetRigidbodyType(RigidbodyType2D bodyType)
        => _rigidbody2D.bodyType = bodyType;

    private void Start()
    {
        ManagerUniRx.AddObjectDisposable(PlaceItemTriggerStayCommand);
        ManagerUniRx.AddObjectDisposable(DestroyItemCommand);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("PlaceItem"))
        {
            if (_rigidbody2D.bodyType == RigidbodyType2D.Dynamic)
                PlaceItemTriggerStayCommand.Execute();
        }
    }

    private void OnValidate()
    {
        if (_rigidbody2D == null)
            _rigidbody2D = GetComponent<Rigidbody2D>();

        if (_dragAndDropComponent == null)
            _dragAndDropComponent = GetComponent<DragAndDropComponent>();

        if (_effectIncreaseComponent == null)
            _effectIncreaseComponent = GetComponent<EffectIncreaseComponent>();
    }

    private void OnDestroy()
    {
        DestroyItemCommand.Execute();

        ManagerUniRx.Dispose(PlaceItemTriggerStayCommand);
        ManagerUniRx.Dispose(DestroyItemCommand);
    }
}
