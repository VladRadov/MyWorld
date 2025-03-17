using UnityEngine;

using UniRx;

public class Item
{
    public Item()
    {
        ManagerUniRx.AddObjectDisposable(RigidbodyType);
    }

    public ReactiveProperty<RigidbodyType2D> RigidbodyType = new();

    public void SetRigidbodyType(RigidbodyType2D bodyType)
        => RigidbodyType.Value = bodyType;
}
