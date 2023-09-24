using System;
using System.Collections.Generic;
using UnityEngine;

namespace Actions
{
    public class ObjectPool<T> where T : MonoBehaviour
    {
        public Action OnObjectDestroyedByTime;

        private List<T> _objects;
        private T _prefab;
        private Transform _parent;
        private Func<T> _createObjectFunc;

        public ObjectPool(T prefab, Transform parent, Func<T> createObjectFunc, int initialSize = 10)
        {
            _prefab = prefab;
            _parent = parent;
            _createObjectFunc = createObjectFunc;

            _objects = new List<T>(initialSize);

            for (int i = 0; i < initialSize; i++)
            {
                T obj = CreateObject();
                obj.gameObject.SetActive(false);
                _objects.Add(obj);
            }
        }

        private T CreateObject()
        {
            T obj = _createObjectFunc();
            obj.gameObject.SetActive(false);
            return obj;
        }

        public T GetObject()
        {
            foreach (var obj in _objects)
            {
                if (!obj.gameObject.activeInHierarchy)
                {
                    obj.gameObject.SetActive(true);
                    return obj;
                }
            }

            T newObj = CreateObject();
            _objects.Add(newObj);
            newObj.gameObject.SetActive(true);
            return newObj;
        }
    }
}