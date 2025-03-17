using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private List<BaseService> _services;

    private void Awake()
    {
        //foreach (var service in _services)
            //service.Initialize();

        //var dragAndDropInputSystem = GetService<DragAndDropInputSystem>();


    }

    private T GetService<T>() where T : BaseService
    {
        var findedService = _services.Find(service => service is T);
        return (T)findedService;
    }
}
