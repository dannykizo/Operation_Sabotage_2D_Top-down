using UnityEngine;
using System.Collections.Generic;

namespace TopDown.Pooling
{
    public class ObjectPool : MonoBehaviour
    {
        private List<GameObject> pooledObjects;
        private GameObject prefabObject;
        private Transform poolParent;
        private int maxPoolSize;

        //Constructor
        public void CreateObjectPool(Transform poolObject, GameObject prefabObject, int maxPoolSize)
        {
            this.prefabObject = prefabObject;
            this.maxPoolSize = maxPoolSize;
            pooledObjects = new List<GameObject>();
            poolParent = poolObject.transform;
        }

        //Return object method
        public GameObject GetObject()
        {
            //If too many projectiles return the first one
            if (pooledObjects.Count >= maxPoolSize)
            {
                pooledObjects[0].SetActive(false);
                return pooledObjects[0];
            } 

            //If any object available return it
            foreach (var item in pooledObjects)
            {
                if (!item.gameObject.activeInHierarchy)
                    return item;
            }

            //Else create new object and return it
            GameObject newObject = Instantiate(prefabObject, poolParent);
            pooledObjects.Add(newObject);
            return newObject;
        }
    }
}