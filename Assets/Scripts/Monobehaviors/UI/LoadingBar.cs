using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadingBar : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] Slider _slider;

    public void ProgressLoadingBar(float p_progress)
    {
        _slider.value = p_progress;
    }
}
