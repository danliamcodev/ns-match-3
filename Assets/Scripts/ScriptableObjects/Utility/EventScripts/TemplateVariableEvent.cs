using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "New TemplateVariable Event", menuName = "Events/TemplateVariable Event")]
public class TemplateVariableEvent : BaseGameEvent<TemplateVariable>
{

}

[System.Serializable]
public class TemplateVariableUnityEvent : UnityEvent<TemplateVariable>
{

}
