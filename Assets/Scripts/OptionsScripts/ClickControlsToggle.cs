using UnityEngine;
using UnityEngine.UI;

public class ClickControlsToggle : MonoBehaviour
{
    public Toggle toggle;

    void Start()
    {
        #if UNITY_ANDROID // Any Android build created should omit this toggle.
            gameObject.SetActive(false);
            return;
        #endif
        // Cargar estado inicial
        toggle.isOn = PlayerPrefs.GetInt(Const.PREF_USE_CLICK_CONTROLS, 0) == 1;
        toggle.onValueChanged.AddListener(OnToggleChanged);
    }

    void OnToggleChanged(bool value)
    {
        switch (value)
        {
            case true:
                Config.SetClickControls(true);
                Config.SetScreenControls(false);
                break;
            case false:
                Config.SetClickControls(false);
                break;
        }
    }
}
