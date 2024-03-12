using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStaminaBar : MonoBehaviour
{
    [SerializeField] private Slider _staminaBar;
    [SerializeField] private Slider _easeStaminaBar;
    [SerializeField] private float _lerpSpeed = 1f;

    private float _currentStamina = 0f;

    private void Update()
    {
        if (_staminaBar && _easeStaminaBar && _staminaBar.value != _easeStaminaBar.value)
        {
            _easeStaminaBar.value = Mathf.Lerp(_easeStaminaBar.value, _currentStamina, _lerpSpeed);
        }
    }
    public void UpdateStaminaBar(float stamina, float maxStamina)
    {
        _currentStamina = stamina / maxStamina;
        _staminaBar.value = _currentStamina;
    }
}