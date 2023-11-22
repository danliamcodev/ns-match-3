using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ToggleButton : MonoBehaviour
{
    [Header("Events")]
    [SerializeField] UnityEvent _toggleOn;
    [SerializeField] UnityEvent _toggleOff;

    [Header("Variables")]
    [SerializeField] BoolVariable _isToggleOn;
    [SerializeField] Color _colorOn;
    [SerializeField] Color _colorOff;

    private void Start()
    {
        if (_isToggleOn.value == true)
        {
            GetComponent<Image>().color = _colorOn;
        }
        else
        {
            GetComponent<Image>().color = _colorOff;
        }
    }

    public void Toggle()
    {
        if (_isToggleOn.value == true)
        {
            _isToggleOn.SetValue(false);
            GetComponent<Image>().color = _colorOff;
            _toggleOff.Invoke();
        } else
        {
            _isToggleOn.SetValue(true);
            GetComponent<Image>().color = _colorOn;
            _toggleOn.Invoke();
        }
    }

    public void ResetDefaults()
    {
        GetComponent<Image>().color = _colorOn;
    }
}
