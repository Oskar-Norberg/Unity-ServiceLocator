using UnityEditor;
using UnityEngine;

namespace ServiceLocator.Editor
{
    public static class ServiceLocatorEditor
    {
        [MenuItem("GameObject/ServiceLocator")]
        public static void AddServiceLocatorToScene()
        {
            var serviceLocator = new GameObject("ServiceLocator").AddComponent<ServiceLocator>();
            EditorGUIUtility.PingObject(serviceLocator);
        }
    }
}