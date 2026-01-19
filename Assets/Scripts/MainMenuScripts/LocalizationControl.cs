using UnityEngine;
using UnityEngine.Localization.Settings;
using System.Collections;

public class LocalizationControl : MonoBehaviour
{
    private static int currentLocale = -1;

    IEnumerator Start()
    {
        yield return LocalizationSettings.InitializationOperation;

        int id = PlayerPrefs.GetInt("LocaleKey", 0);
        SetLocaleImmediate(id);
        CategoryDatabase.LoadAll();
    }

    public void ChangeLocale(int localeID)
    {
        StartCoroutine(ChangeLocaleRoutine(localeID));
    }

    IEnumerator ChangeLocaleRoutine(int localeID)
    {
        yield return LocalizationSettings.InitializationOperation;
        SetLocaleImmediate(localeID);
    }

    void SetLocaleImmediate(int localeID)
    {
        var locales = LocalizationSettings.AvailableLocales.Locales;

        if (localeID < 0 || localeID >= locales.Count)
        {
            Debug.LogWarning("Invalid locale ID: " + localeID);
            return;
        }

        if (currentLocale == localeID)
            return;

        LocalizationSettings.SelectedLocale = locales[localeID];
        PlayerPrefs.SetInt("LocaleKey", localeID);

        currentLocale = localeID;
        Debug.Log("Locale set to: " + locales[localeID].Identifier.Code);

        ResetCatSelect();
    }

    public void ResetCatSelect()
    {
        Competition.ClearCategories();
    }
}