using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "New SC_Gem Event", menuName = "Events/SC_Gem Event")]
public class SC_GemEvent : BaseGameEvent<SC_Gem>
{

}

[System.Serializable]
public class SC_GemUnityEvent : UnityEvent<SC_Gem>
{

}
