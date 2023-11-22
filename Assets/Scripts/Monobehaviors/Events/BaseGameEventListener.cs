using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class BaseGameEventListener<Type, Event, UnityEventObject> : MonoBehaviour, IGameEventListener<Type>
    where Event : BaseGameEvent<Type> where UnityEventObject : UnityEvent<Type>
{
    [Tooltip("Event to register with.")]
    [SerializeField] Event _event;

    [Tooltip("Response to invoke when Event is raised.")]
    [SerializeField] UnityEventObject _response;

    private void OnEnable()
    {
        _event.RegisterListener(this);
    }

    private void OnDisable()
    {
        _event.UnregisterListener(this);
    }

    public void OnEventRaised(Type p_parameter)
    {
        _response.Invoke(p_parameter);
    }
}

