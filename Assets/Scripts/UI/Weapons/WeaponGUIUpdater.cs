using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WeaponGUIUpdater : MonoBehaviour
{
    [SerializeField] private Image _ammoBarGUI;
    [SerializeField] private TextMeshProUGUI _magazineAmmoGUI;
    [SerializeField] private TextMeshProUGUI _storageAmmoGUI;

    public void UpdateAmmoGUI(int magazineAmmo, int storageAmmo)
    {
        _magazineAmmoGUI.text = magazineAmmo.ToString();
        _storageAmmoGUI.text = storageAmmo.ToString();
    }

    public void FillAmmoBar(float fillAmount)
    {
        _ammoBarGUI.fillAmount = fillAmount;
    }
}
