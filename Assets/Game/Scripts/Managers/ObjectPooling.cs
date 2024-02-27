using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;

namespace Game.Scripts.Managers
{
    public class ObjectPooling : MonoBehaviour
    {
        [SerializeField] private Transform _poolParent;
            
        [TabGroup("ENEMY")] public GameObject Enemy;
        [TabGroup("ENEMY")] public ObjectPoolType EnemyType;
        [TabGroup("ENEMY")] public int EnemyPoolSize;
        [TabGroup("ENEMY")] public List<GameObject> EnemyObjectPool;
        
        public void Initialize()
        {
            InitializeObjectPool(Enemy, EnemyPoolSize, EnemyObjectPool);
        }
        
        private void InitializeObjectPool(GameObject obj, int poolSize, List<GameObject> objectPool)
        {
            for (int i = 0; i < poolSize; i++)
            {
                GameObject temp = Instantiate(obj);
                temp.transform.SetParent(_poolParent);
                temp.transform.localPosition = Vector3.zero;
                temp.transform.localEulerAngles = Vector3.zero;
                temp.SetActive(false);
                objectPool.Add(temp);
            }
        }

        public GameObject GetObject(ObjectPoolType type, Transform parent = null)
        {

            if (parent == null)
                parent = _poolParent;
            
            if (type == EnemyType)
            {
                return GetObjectFromPool(parent, Vector3.zero, Quaternion.identity, EnemyObjectPool, Enemy);
            }
            
            return GetObjectFromPool(parent, Vector3.zero, Quaternion.identity, EnemyObjectPool, Enemy);
        }
        
        private GameObject GetObjectFromPool(Transform parent, Vector3 position, Quaternion rotation, List<GameObject> objectPool, GameObject newObj)
        {
            foreach (GameObject obj in objectPool)
            {
                if (!obj.activeInHierarchy)
                {
                    obj.transform.SetParent(parent);
                    obj.transform.localPosition = position;
                    obj.transform.localRotation = rotation;
                    obj.SetActive(true);
                    return obj;
                }
            }
            
            GameObject temp = Instantiate(newObj, position, rotation);
            temp.transform.SetParent(parent);
            temp.transform.localPosition = position;
            temp.transform.localRotation = rotation;
            objectPool.Add(temp);
            return temp;
        }
        
        public void ReturnObjectToPool(GameObject obj)
        {
            obj.transform.SetParent(_poolParent);
            obj.transform.localPosition = Vector3.zero;
            obj.transform.localEulerAngles = Vector3.zero;
            obj.SetActive(false);
        }
    }
}
