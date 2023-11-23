using System.IO;
using UnityEngine;
using UnityEditor;

[CreateAssetMenu(fileName = "Event Scripts Generator", menuName = "Utility/Event Scripts Generator")]
public class EventScriptsGenerator : ScriptableObject
{
    [Header("References")]
    [SerializeField] DefaultAsset _eventsFolder;
    [SerializeField] DefaultAsset _eventListenersFolder;
    [SerializeField] DefaultAsset _eventEditorsFolder;
    [SerializeField] MonoScript _eventsTemplate;
    [SerializeField] MonoScript _eventListenersTemplate;
    [SerializeField] MonoScript _eventEditorsTemplate;

    [Header("Variables")]
    [SerializeField] string _eventType;
    public void GenerateScripts()
    {
        GenerateEventScript();
        GenerateEventEditorScript();
        GenerateEventListenerScript();
        AssetDatabase.Refresh();
    }

    private void GenerateEventScript()
    {
        string scriptName = $"{_eventType}Event";
        string scriptText = _eventsTemplate.text;

        // Replace the placeholders with the actual event type
        scriptText = scriptText.Replace("TemplateVariable", _eventType);

        string folderPath = AssetDatabase.GetAssetPath(_eventsFolder);
        string scriptPath = Path.Combine(folderPath, scriptName + ".cs");

        File.WriteAllText(scriptPath, scriptText);
    }

    private void GenerateEventEditorScript()
    {
        string scriptName = $"{_eventType}EventEditor";
        string scriptText = _eventEditorsTemplate.text;

        // Replace the placeholders with the actual event type
        scriptText = scriptText.Replace("TemplateVariable", _eventType);

        string folderPath = AssetDatabase.GetAssetPath(_eventEditorsFolder);
        string scriptPath = Path.Combine(folderPath, scriptName + ".cs");

        File.WriteAllText(scriptPath, scriptText);
    }

    private void GenerateEventListenerScript()
    {
        string scriptName = $"{_eventType}EventListener";
        string scriptText = _eventListenersTemplate.text;

        // Replace the placeholders with the actual event type
        scriptText = scriptText.Replace("TemplateVariable", _eventType);

        string folderPath = AssetDatabase.GetAssetPath(_eventListenersFolder);
        string scriptPath = Path.Combine(folderPath, scriptName + ".cs");

        File.WriteAllText(scriptPath, scriptText);
    }
}
