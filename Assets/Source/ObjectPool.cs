using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Lomztein.BruteForceAttackSequel
{
    public class ObjectPool : IDisposable
    {
        private List<GameObject> _objects = new List<GameObject>();
        private GameObject _objectPrefab;

        public ObjectPool (GameObject prefab)
        {
            _objectPrefab = prefab;
        }
        
        public GameObject Get ()
        {
            GameObject obj = _objects.FirstOrDefault(x => !x.activeSelf);
            if (obj == null)
            {
                obj = UnityEngine.Object.Instantiate(_objectPrefab);
            }
            return obj;
        }

        public void SetPrefab (GameObject prefab)
        {
            _objectPrefab = prefab;
        }

        public void Dispose()
        {
            Clear();
        }

        public void Clear ()
        {
            foreach (GameObject obj in _objects)
            {
                UnityEngine.Object.Destroy(obj);
            }
        }
    }
}
