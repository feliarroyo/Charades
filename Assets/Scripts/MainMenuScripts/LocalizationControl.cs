using UnityEngine;
using UnityEngine.Localization.Settings;
using System.Collections;
using UnityEngine.SceneManagement;

public class LocalizationControl : MonoBehaviour
{
    private static int currentLocale = 0;

    private void Start()
    {
        int ID = PlayerPrefs.GetInt("LocaleKey", 0);
        Debug.Log("ID de start:" + ID);
        ChangeLocale(ID);
        CategoryDatabase.LoadAll();
    }

    public void ChangeLocale(int _localeID)
    {
        Debug.Log("EN CHANGE: CurrentLocale " + currentLocale + " LocaleID " + _localeID);
        if (currentLocale == _localeID)
            return;
        StartCoroutine(SetLocale(_localeID));
    }

    IEnumerator SetLocale(int _localeID)
    {
        yield return LocalizationSettings.InitializationOperation;
        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[_localeID];
        PlayerPrefs.SetInt("LocaleKey", _localeID);
        Debug.Log("EN SET: CurrentLocale " + currentLocale + " LocaleID " + _localeID);
        if (currentLocale != _localeID)
        {
            currentLocale = _localeID;
            Debug.Log("EN IF-SET: CurrentLocale " + currentLocale + " LocaleID " + _localeID);
            ResetCatSelect();
        }
    }

    public void ResetCatSelect()
    {
        Competition.ClearCategories(); // in order to not use other languages files
    }
}
