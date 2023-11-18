using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthBar : MonoBehaviour
{
    [SerializeField] private Image _healthBar;

    public void UpdateHealthBar(float health, float maxHealth)
    {
        _healthBar.fillAmount = health / maxHealth ;
    }
}
