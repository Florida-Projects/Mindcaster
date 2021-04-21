using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReflectionSkillController : MonoBehaviour
{
    public Slider slider;
    public void SetMaxCooldown(float cooldown)
    {
        slider.maxValue = cooldown;
        slider.value = 0;
    }

    public void SetCooldown(float cooldown)
    {
        slider.value = cooldown;
    }

    public void DecreaseCooldown(float cooldown)
    {
        slider.value -= cooldown;
        //Debug.Log(slider.value);
    }
}
