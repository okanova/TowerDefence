using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Game.Scripts.Managers
{
    public class ObjectPooling : MonoBehaviour
    {
        [SerializeField] private Transform _poolParent;
            
        [TabGroup("GOBLIN")] public GameObject Goblin;
        [TabGroup("GOBLIN")] public ObjectPoolType GoblinType;
        [TabGroup("GOBLIN")] public int GoblinPoolSize;
        [TabGroup("GOBLIN")] public List<GameObject> GoblinObjectPool;
        
        public void Initialize()
        {
            InitializeObjectPool(Goblin, GoblinPoolSize, GoblinObjectPool);
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
            if (type == GoblinType)
            {
                return GetObjectFromPool(Vector3.zero, Quaternion.identity, GoblinObjectPool, Goblin);
            }
            
            return GetObjectFromPool(Vector3.zero, Quaternion.identity, GoblinObjectPool, Goblin);
        }
        
        private GameObject GetObjectFromPool(Vector3 position, Quaternion rotation, List<GameObject> objectPool, GameObject newObj)
        {
            foreach (GameObject obj in objectPool)
            {
                if (!obj.activeInHierarchy)
                {
                    obj.transform.localPosition = position;
                    obj.transform.localRotation = rotation;
                    obj.SetActive(true);
                    return obj;
                }
            }
            
            GameObject temp = Instantiate(newObj, position, rotation);
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
