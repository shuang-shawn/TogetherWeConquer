using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ComboTimer : MonoBehaviour
{
    private Slider slider;

    public float downTick = 0.5f;
    private float target = 1.0f;

    private void Awake()
    {
        slider = gameObject.GetComponent<Slider>();
    }

    void Start()
    {
        Decrement(0.0f);
    }

    void Update()
    {
        if(slider.value > target)
        {
            slider.value -= downTick * Time.deltaTime;
        }
    }

    public void Decrement(float value)
    {
        target = slider.value - value;
    }
}
