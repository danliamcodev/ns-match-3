using System.Collections.Generic;
using UnityEngine;

public abstract class RuntimeSet<T> : ScriptableObject
{
    [SerializeField] protected List<T> _items = new List<T>();

    public List<T> items { get { return _items; } }

    public virtual void Add(T item)
    {
        if (!_items.Contains(item))
        {
            _items.Add(item);
        }
    }

    public virtual void Remove(T item)
    {
        if (_items.Contains(item))
        {
            _items.Remove(item);
        }
    }
}
