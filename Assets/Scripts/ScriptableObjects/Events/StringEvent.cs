using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "New String Event", menuName = "Events/String Event")]
public class StringEvent : BaseGameEvent<string>
{

}

[System.Serializable]
public class StringUnityEvent : UnityEvent<string>
{

}

