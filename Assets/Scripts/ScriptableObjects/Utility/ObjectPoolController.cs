using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Object Pool Controller", menuName = "Controllers/Object Pool Controller")]
public class ObjectPoolController : ScriptableObject
{
    [Header("Variables")]
    [SerializeField] GameObject _objectPrefab;

    ObjectPool _objectPool;

    public GameObject prefab { get { return _objectPrefab; } }
    public void AssignObjectPool(ObjectPool p_objectPool)
    {
        _objectPool = p_objectPool;
    }

    public void AddObjectsToPool(int p_objectCount)
    {
        _objectPool.AddObjectsToPool(_objectPrefab, p_objectCount);
    }

    public GameObject GetObject()
    {
        if (_objectPool.objects.Count < 1)
        {
            _objectPool.AddObjectsToPool(_objectPrefab, 1);
        }

        GameObject obj = _objectPool.objects.Dequeue();
        return obj;
    }

    public void ReturnObject(GameObject p_object)
    {
        p_object.SetActive(false);
        _objectPool.objects.Enqueue(p_object);
    }
}
