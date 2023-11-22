using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseGameEvent<Type> : ScriptableObject
{
    [SerializeField] Type _testValue;
    /// <summary>
    /// The list of listeners that this event will notify if it is raised.
    /// </summary>
    private readonly List<IGameEventListener<Type>> _eventListeners = new List<IGameEventListener<Type>>();

    public Type testValue { get { return _testValue; } }

    public void Raise(Type p_parameter)
    {
        for (int i = _eventListeners.Count - 1; i >= 0; i--)
            _eventListeners[i].OnEventRaised(p_parameter);
    }

    public void RegisterListener(IGameEventListener<Type> p_listener)
    {
        if (!_eventListeners.Contains(p_listener))
            _eventListeners.Add(p_listener);
    }

    public void UnregisterListener(IGameEventListener<Type> p_listener)
    {
        if (_eventListeners.Contains(p_listener))
            _eventListeners.Remove(p_listener);
    }
}

public interface IGameEventListener<Type>
{
    void OnEventRaised(Type p_parameter);
}

