using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStaminaBar : MonoBehaviour
{
    [SerializeField] private Image _staminaBar;

    public void UpdateStaminaBar(float stamina, float maxStamina)
    {
        _staminaBar.fillAmount = stamina / maxStamina;
    }
}
