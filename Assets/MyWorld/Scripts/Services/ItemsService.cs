using System.Collections.Generic;
using UnityEngine;

public class ItemsService : BaseService
{
    private ItemsController _itemsController;

    [SerializeField] private List<ItemView> _itemViews;

    public override void Initialize()
    {
        _itemsController = new ItemsController(_itemViews);
        _itemsController.Initialize();
    }
}
