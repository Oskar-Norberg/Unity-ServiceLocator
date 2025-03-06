using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace ServiceLocator
{
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

        public T GetService<T>() where T : Object
        {
            var service = _services[typeof(T)];

            if (service == null)
            {
                Debug.LogWarning($"Service of type {typeof(T)} is not registered");
                return null;
            }

            return _services[typeof(T)] as T;
        }
    }
}