using System.IO;
using UnityEngine;
using UnityEditor;

[CreateAssetMenu(fileName = "Event Scripts Generator", menuName = "Utility/Event Scripts Generator")]
public class EventScriptsGenerator : ScriptableObject
{
#if UNITY_EDITOR
    [Header("References")]
    [SerializeField] DefaultAsset _eventsFolder;
    [SerializeField] DefaultAsset _eventListenersFolder;
    [SerializeField] DefaultAsset _eventEditorsFolder;
    [SerializeField] MonoScript _eventsTemplate;
    [SerializeField] MonoScript _eventListenersTemplate;
    [SerializeField] MonoScript _eventEditorsTemplate;
#endif

    [Header("Variables")]
    [SerializeField] string _eventType;
    public void GenerateScripts()
    {
#if UNITY_EDITOR
        GenerateEventScript();
        GenerateEventEditorScript();
        GenerateEventListenerScript();
        AssetDatabase.Refresh();
#endif
    }

    private void GenerateEventScript()
    {
#if UNITY_EDITOR
        string scriptName = $"{_eventType}Event";
        string scriptText = _eventsTemplate.text;

        // Replace the placeholders with the actual event type
        scriptText = scriptText.Replace("TemplateVariable", _eventType);

        string folderPath = AssetDatabase.GetAssetPath(_eventsFolder);
        string scriptPath = Path.Combine(folderPath, scriptName + ".cs");

        File.WriteAllText(scriptPath, scriptText);
#endif
    }

    private void GenerateEventEditorScript()
    {
#if UNITY_EDITOR
        string scriptName = $"{_eventType}EventEditor";
        string scriptText = _eventEditorsTemplate.text;

        // Replace the placeholders with the actual event type
        scriptText = scriptText.Replace("TemplateVariable", _eventType);

        string folderPath = AssetDatabase.GetAssetPath(_eventEditorsFolder);
        string scriptPath = Path.Combine(folderPath, scriptName + ".cs");

        File.WriteAllText(scriptPath, scriptText);
#endif
    }

    private void GenerateEventListenerScript()
    {
#if UNITY_EDITOR
        string scriptName = $"{_eventType}EventListener";
        string scriptText = _eventListenersTemplate.text;

        // Replace the placeholders with the actual event type
        scriptText = scriptText.Replace("TemplateVariable", _eventType);

        string folderPath = AssetDatabase.GetAssetPath(_eventListenersFolder);
        string scriptPath = Path.Combine(folderPath, scriptName + ".cs");

        File.WriteAllText(scriptPath, scriptText);
#endif
    }
}
