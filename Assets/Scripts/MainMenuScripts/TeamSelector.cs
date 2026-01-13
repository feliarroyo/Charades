using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;
using System.Collections;

public class TeamSelector : MonoBehaviour
{
    private static readonly string[] labels = { "Sí", "No", "Enabled", "Disabled" };

    [Header("UI Elements")]
    [SerializeField] private TextMeshProUGUI textSpace;
    [SerializeField] private Image buttonImage;
    [SerializeField] private Sprite[] buttonSprites;
    [SerializeField] private GameObject[] teamSpaces;

    [Header("Localization")]
    [SerializeField] private LocalizedString localizedLabel;

    private void OnEnable()
    {
        // Subscribe
        LocalizationSettings.SelectedLocaleChanged += OnLocaleChanged;
        localizedLabel.StringChanged += UpdateText;

        // Force update
        StartCoroutine(DelayedRefresh());
    }

    private IEnumerator DelayedRefresh()
    {
        yield return null; // wait 1 frame

        localizedLabel.RefreshString();
        ShowTeamsUI(PlayerPrefs.GetInt(Const.PREF_TEAM_COUNT, 1) != 1);
    }

    private void OnDisable()
    {
        // Unsubscribe
        LocalizationSettings.SelectedLocaleChanged -= OnLocaleChanged;
        localizedLabel.StringChanged -= UpdateText;
    }

    private void Start()
    {
        ShowTeamsUI(PlayerPrefs.GetInt(Const.PREF_TEAM_COUNT, 1) != 1);
    }

    private void UpdateText(string value)
    {
        textSpace.text = value;
    }

    private void OnLocaleChanged(Locale _)
    {
        ShowTeamsUI(PlayerPrefs.GetInt(Const.PREF_TEAM_COUNT, 1) != 1);
    }

    private void UpdateLabel(string label)
    {
        localizedLabel.Arguments = new object[] { label };
        localizedLabel.RefreshString();
    }

    public void ChangeTeams()
    {
        int current = PlayerPrefs.GetInt(Const.PREF_TEAM_COUNT, 1);
        PlayerPrefs.SetInt(Const.PREF_TEAM_COUNT, current == 1 ? 2 : 1);

        ShowTeamsUI(PlayerPrefs.GetInt(Const.PREF_TEAM_COUNT, 1) != 1);
    }

    private void ShowTeamsUI(bool enabled)
    {
        if (Const.EnglishLocaleActive())
            UpdateLabel(enabled ? labels[2] : labels[3]);
        else
            UpdateLabel(enabled ? labels[0] : labels[1]);

        buttonImage.sprite = buttonSprites[enabled ? 0 : 1];

        foreach (var go in teamSpaces)
            go.SetActive(enabled);

        ChangeTextColor();
    }

    public void ChangeTextColor()
    {
        textSpace.color = (PlayerPrefs.GetInt(Const.PREF_TEAM_COUNT, 1) > 1)
            ? Const.TeamsEnabledTextColor
            : Const.TeamsDisabledTextColor;
    }
}