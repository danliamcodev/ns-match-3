using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Time Manager", menuName = "Managers/Time Manager")]
public class TimeManager : ScriptableObject
{
    [SerializeField] float _normalTimeScale = 1f;
    public void SetGameTimeScale(float p_timeScale)
    {
        Time.timeScale = p_timeScale;
    }

    public void ReturnNormalTime()
    {
        Time.timeScale = _normalTimeScale;
    }

    public void PauseTime()
    {
        Time.timeScale = 0f;
    }
}
