using System;
using System.Collections.Generic;
using UnityEngine;

public class ServiceLocator : MonoBehaviour
{
    public static ServiceLocator Instance
    {
        get
        {
            if (_instance == null)
                Debug.LogWarning("ServiceLocator is not initialized");

            return _instance;
        }
    }
    private static ServiceLocator _instance;

    private Dictionary<Type, object> _services = new Dictionary<Type, object>();

    private void Awake()
    {
        if (_instance != null)
        {
            Destroy(gameObject);
            return;
        }

        _instance = this;
    }
    
    public void Register<T>(T service)
    {
        var type = typeof(T);
        
        if (_services.ContainsKey(type))
        {
            Debug.LogWarning($"Service of type {type} is already registered");
            return;
        }

        _services.Add(type, service);
    }
    
    public void Unregister<T>()
    {
        var type = typeof(T);
        
        if (!_services.ContainsKey(type))
        {
            Debug.LogWarning($"Service of type {type} is not registered");
            return;
        }

        _services.Remove(type);
    }
}
