using System.Collections.Generic;

using UniRx;
using UnityEngine;

public class ItemsController
{
    private List<Item> _items;
    private List<ItemView> _itemsView;

    public ItemsController(List<ItemView> itemsView)
    {
        _itemsView = itemsView;
        _items = new List<Item>();
    }

    public void Initialize()
    {
        foreach (var itemView in _itemsView)
        {
            var item = new Item();
            item.RigidbodyType.Subscribe(value => { itemView.SetRigidbodyType(value); });
            itemView.DragAndDropComponent.DraggingStartCommand.Subscribe(_ =>
            {
                item.SetRigidbodyType(RigidbodyType2D.Kinematic);
                itemView.EffectIncreaseComponent.Execute();
            });
            itemView.DragAndDropComponent.DraggingEndCommand.Subscribe(_ =>
            {
                item.SetRigidbodyType(RigidbodyType2D.Dynamic);
                itemView.EffectIncreaseComponent.Cancel();
            });
            itemView.PlaceItemTriggerStayCommand.Subscribe(_ =>
            {
                item.SetRigidbodyType(RigidbodyType2D.Static);
            });
            itemView.DestroyItemCommand.Subscribe(_ => { ManagerUniRx.Dispose(item.RigidbodyType); });

            _items.Add(item);
        }
    }
}
