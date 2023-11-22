using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [Header("References")]
    [SerializeField] ObjectPoolController _objectPoolController;

    Queue<GameObject> _objects = new Queue<GameObject>();

    public Queue<GameObject> objects { get { return _objects; } }

    private void Awake()
    {
        _objectPoolController.AssignObjectPool(this);
    }

    public void AddObjectsToPool(GameObject p_object, int p_objectCount)
    {
        for (int i = 0; i < p_objectCount; i++)
        {
            GameObject obj = Instantiate(p_object, this.transform.position, p_object.transform.rotation, this.transform);
            _objects.Enqueue(obj);
        }
    }
}
