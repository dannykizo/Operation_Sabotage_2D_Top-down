using UnityEngine;

namespace TopDown.Pooling
{
    public class ObjectPoolCreator : Singleton<ObjectPoolCreator>
    {
        private GameObject poolObject;

        public ObjectPool CreateObjectPool(GameObject creatorObject, GameObject poolPrefab, int poolMaxSize)
        {
            //Create pool object | assign name/parent | Add Object pool component
            poolObject = new GameObject($"{creatorObject.name}_{poolPrefab.name}_Pool");
            poolObject.AddComponent(typeof(ObjectPool));
            poolObject.transform.parent = transform;

            //Create pool object
            ObjectPool returnPool;
            returnPool = poolObject.GetComponent<ObjectPool>();
            returnPool.CreateObjectPool(poolObject.transform, poolPrefab, poolMaxSize);

            return returnPool;
        }
    }
}