using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pattern.ObjectPools
{
    public class ObjectPoolPattern : MonoBehaviour
    {
        [SerializeField] private GameObject _prefab;
        private Queue<GameObject> _objectPool = new Queue<GameObject>();

        public GameObject GetObjectFromPool()
        {
            if (_objectPool.Count > 0)
            {
                GameObject obj = _objectPool.Dequeue();
                obj.SetActive(true);
                return obj;
            }
            else
            {
                return Instantiate(_prefab);
            }
        }

        public void ReturnObjectToPool(GameObject obj)
        {
            obj.SetActive(false);
            _objectPool.Enqueue(obj);
        }
    } 
}
