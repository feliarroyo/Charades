using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;
using UnityEngine.UI;

public class FlagOpacityController : MonoBehaviour
{
    const float alphaValue = 0.4f;
    private Image flag;
    private Color unselectedColor, selectedColor;
    public int locale;
    // Start is called before the first frame update
    void Start()
    {
        flag = GetComponent<Image>();
        Color color = flag.color;
        color.a = 1;
        selectedColor = color;
        color.a = alphaValue;
        unselectedColor = color;
        ToggleSelect(PlayerPrefs.GetInt("LocaleKey", 0) == locale);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void DebugPrint(string message)
    {
        Debug.Log(message);
    }

    private void OnEnable()
    {
        LocalizationSettings.SelectedLocaleChanged += OnLocaleChanged;
    }

    private void OnDisable()
    {
        LocalizationSettings.SelectedLocaleChanged -= OnLocaleChanged;
    }
    private void OnLocaleChanged(Locale locale)
    {
        ToggleSelect(LocalizationSettings.SelectedLocale == LocalizationSettings.AvailableLocales.Locales[this.locale]);
    }
    
    public void ToggleSelect(bool isSelected)
    {
        if (isSelected)
        {
            flag.color = selectedColor;
        }
        else
        {
            flag.color = unselectedColor;
        }
    }
}
