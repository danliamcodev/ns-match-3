using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderUI : MonoBehaviour
{
    [Header("Variables")]
    [SerializeField] FloatVariable _value;
    void Start()
    {
        GetComponent<Slider>().value = _value.value;
    }
}
