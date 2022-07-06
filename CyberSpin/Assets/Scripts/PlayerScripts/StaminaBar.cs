using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StaminaBar : MonoBehaviour
{
    public Slider staminaSlider;
    public Slider staminaSlider2;

    public static StaminaBar instanceStaminaBar;

    private void Awake()
    {
        instanceStaminaBar = this;
    }

    public void SetMaxStamina(float stamina)
    {
        staminaSlider.maxValue = stamina;
        staminaSlider.value = stamina;
        staminaSlider2.maxValue = stamina;
        staminaSlider2.value = stamina;
    }

    public void SetStamina(float stamina)
    {
        staminaSlider.value = stamina;
        staminaSlider2.value = stamina;
        //staminaSlider.value = Mathf.Lerp(staminaSlider.value, stamina, 5f * Time.deltaTime);
    }
}
