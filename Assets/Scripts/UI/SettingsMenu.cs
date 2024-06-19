using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SettingsMenu : MonoBehaviour {
    [SerializeField] private GameConfig config;
    [SerializeField] private TMP_Dropdown themesDropdown;
    [SerializeField] private TMP_Dropdown langDropdown;
    List<string> stringThemes;

    private void Start() {
        themesDropdown.ClearOptions();
        stringThemes = Themes.GetThemeNames();
        themesDropdown.AddOptions(stringThemes);

        int selectedIndex = stringThemes.IndexOf(GameManager.selectedTheme.name);
        if (selectedIndex != -1) {
            themesDropdown.value = selectedIndex;
        }

        if (config.easyWordsFile == "easy") {
            langDropdown.value = 0;
        } else {
            langDropdown.value = 1;
        }
    }

    public void HandleThemeChange(int val) {
        Theme selectedTheme = Themes.GetTheme(stringThemes[val]);
        GameManager.selectedTheme = selectedTheme;
    }

    public void HandleLanguageChange(int val) {
        if (val == 0) {
            config.easyWordsFile = "easy";
            config.hardWordsFile = "hard";
        } 
        if (val == 1) {
            config.easyWordsFile = "easy_es";
            config.hardWordsFile = "hard_es";
        }
    }
}