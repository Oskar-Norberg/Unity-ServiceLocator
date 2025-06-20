using System;
using System.Collections.Generic;
using UnityEngine;

namespace ringo.ServiceLocator
{
    public class GlobalServiceLocator : MonoBehaviour
    {
        public static GlobalServiceLocator Instance
        {
            get
            {
                if (_instance == null)
                    Debug.LogWarning("ServiceLocator is not initialized");

                return _instance;
            }
        }
        private static GlobalServiceLocator _instance;

        private Dictionary<Type, System.Object> _services = new();

        private void Awake()
        {
            if (_instance != null)
            {
                Destroy(gameObject);
                return;
            }

            _instance = this;
        }
    
        public void Register<T>(T service) where T : class
        {
            if (service == null)
            {
                Debug.LogError("Cannot register a null service");
                throw new ArgumentNullException(nameof(service), "Service cannot be null");
            }
            
            var type = typeof(T);
        
            // Dont override existing service
            if (_services.ContainsKey(type))
            {
                Debug.LogWarning($"Service of type {type} is already registered");
                return;
            }

            _services.Add(type, service);
        }
    
        public void Unregister<T>() where T : class
        {
            var type = typeof(T);
        
            if (!_services.ContainsKey(type))
            {
                Debug.LogWarning($"Service of type {type} is not registered");
                return;
            }

            _services.Remove(type);
        }

        public T GetService<T>() where T : class
        {
            if (_services.TryGetValue(typeof(T), out var service))
            {
                return service as T;
            }

            return null;
        }
        
        public bool TryGetService<T>(out T service) where T : class
        {
            if (_services.TryGetValue(typeof(T), out var foundService))
            {
                service = foundService as T;
                return true;
            }

            service = null;
            return false;
        }
        
        public bool HasService<T>() where T : class
        {
            return _services.ContainsKey(typeof(T));
        }
    }
}