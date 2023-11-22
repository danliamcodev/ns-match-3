# Scriptable Objects

The purpose of this document is to describe how Scriptable Objects are being used in the project.

## Table of Contents

- [Data Containers](#data-containers)
- [Variables](#variables)
- [Runtime Sets](#runtime-sets)
- [Event System](#event-system)
  - [Events](#events)
  - [Event Listeners](#event-listeners)
- [Additional Resources](#additional-resources)

## Data Containers

Scriptable Objects can be used as data containers. They are a convenient way to store and manage various types of data that don't need to be attached to a GameObject. Examples of data stored in Scriptable Objects include game settings, item data, and character attributes.

## Variables

Scriptable Objects variables are small scriptable objects that only hold one variable. This allows variables to be modular and easily accessible by different components.

 Base Variable Class:

```csharp
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
```

Float Variable Class:

```csharp
[CreateAssetMenu(fileName = "Float Variable", menuName = "Variables/Float Variable")]
public class FloatVariable : BaseVariable<float>
{
    public void ApplyChange(float p_amount)
    {
        _value += p_amount;
    }

    public void ApplyChange(FloatVariable p_amount)
    {
        _value += p_amount.value;
    }
}
```

String Variable Class:

```csharp
[CreateAssetMenu(fileName = "String Variable", menuName = "Variables/String Variable")]
public class StringVariable : BaseVariable<string>
{

}
```

## Runtime Sets

Runtime Sets, implemented as Scriptable Objects, are used to manage collections of objects during runtime. They are especially useful for dynamically changing lists of objects, such as active enemies, active quests, or in-game items.

Base Runtime Set Class:

```csharp
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
```

GameObjects Set Class:

```csharp
[CreateAssetMenu(fileName = "Game Objects Set", menuName = "Runtime Sets/Game Objects Set")]
public class GameObjectsSet : RuntimeSet<GameObject>
{

}
```

## Event System

Scriptable Objects are employed to create an event system within the project. This system is crucial for decoupling different parts of the code and allowing them to communicate seamlessly. The event system comprises two main components:

### Events

Scriptable Object Events are used to trigger specific actions or responses in the game. These events can be easily created, modified, and extended without changing the core code. Examples include "PlayerDeath," "QuestCompleted," and "ItemPickedUp" events.

Base Event Class:

```csharp
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
```

Void Event Class:

```csharp
public class VoidEventListener : BaseGameEventListener<Void, VoidEvent, VoidUnityEvent>
{

}
```

Float Event Class:

```csharp
public class FloatEventListener : BaseGameEventListener<float, FloatEvent, FloatUnityEvent>
{

}
```

Editor scripts are added to allow testing of events in the inspector.

Base Event Editor Class:

```csharp
public abstract class BaseEventEditor<Type, Event> : Editor where Event : BaseGameEvent<Type>
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        GUI.enabled = Application.isPlaying;

        Event gameEvent = target as Event;
        if (GUILayout.Button("Raise"))
            gameEvent.Raise(gameEvent.testValue);
    }
}
```

Void Event Editor Class:

```csharp
[CustomEditor(typeof(VoidEvent))]
public class VoidEventEditor : BaseEventEditor<Void, VoidEvent>
{

}
```

Float Event Editor Class:

```csharp
[CustomEditor(typeof(FloatEvent))]
public class FloatEventEditor : BaseEventEditor<float, FloatEvent>
{

}
```

### Event Listeners

Event Listeners are monobehaviors used to listen for and respond to specific events. They allow different game components to react to events without having direct dependencies on each other.

Base Event Listener Class:

```csharp
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
```

Void Event Listener Class:

```csharp
public class VoidEventListener : BaseGameEventListener<Void, VoidEvent, VoidUnityEvent>
{

}
```

Float Event Listener Class:

```csharp
public class FloatEventListener : BaseGameEventListener<float, FloatEvent, FloatUnityEvent>
{

}
```

## Additional Resources

- [Unite Austin 2017 - Game Architecture with Scriptable Objects](https://youtu.be/raQ3iHhE_Kk?si=ekF3ELxLr_4_xFvY)
- [Unite 2016 - Overthrowing the MonoBehaviour Tyranny in a Glorious Scriptable Object Revolution](https://youtu.be/6vmRwLYWNRo?si=loPCfOMS6mVVg6OY)
- [GameDev Architecture - Scriptable Object Event System - Unity - Part 1](https://youtu.be/iXNwWpG7EhM?si=cBDycKGzE3ZSQZMk)
- [GameDev Architecture - Scriptable Object Events With Custom Data - Unity - Part 2](https://youtu.be/P-U7GPXMtLY?si=g6weGRjNmSlXy6Hc)
