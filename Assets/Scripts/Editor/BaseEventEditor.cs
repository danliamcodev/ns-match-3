using UnityEditor;
using UnityEngine;

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
