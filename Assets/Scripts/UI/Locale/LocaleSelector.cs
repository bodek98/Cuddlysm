using System.Collections;
using UnityEngine;

using UnityEngine.Localization.Settings;

public class LocaleSelector : MonoBehaviour
{
    private bool _changeGuard = false;

    private void Start()
    {
        int id = PlayerPrefs.GetInt("LocaleID", 0);
        ChangeLocale(id);
    }

    public void ChangeLocale(int localeID)
    {
        if (_changeGuard == true) return;
        StartCoroutine(SetLocale(localeID));
    }

    IEnumerator SetLocale(int localeID)
    {
        _changeGuard = true;
        yield return LocalizationSettings.InitializationOperation;
        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[localeID];
        PlayerPrefs.SetInt("LocaleID", localeID);
        _changeGuard = false;
    }
}
