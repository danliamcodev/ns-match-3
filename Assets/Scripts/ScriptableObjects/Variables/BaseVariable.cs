using UnityEngine;

public abstract class BaseVariable<T> : ScriptableObject
{
    [SerializeField] protected T _value;

    public T value { get { return _value; } }

    public void SetValue(T p_value)
    {
        _value = p_value;
    }

    public void SetValue(BaseVariable<T> p_value)
    {
        _value = p_value.value;
    }
}

