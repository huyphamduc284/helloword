using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SettingsMenu : MonoBehaviour {
    [SerializeField] private GameConfig config;
    [SerializeField] private TMP_Dropdown themesDropdown;
    [SerializeField] private TMP_Dropdown langDropdown;
    [SerializeField] private TMP_InputField difficultyInput;
    [SerializeField] private TMP_InputField spawnDelayInput;
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

        difficultyInput.text = PlayerPrefs.GetFloat("DifficultyMult", 1).ToString();
        difficultyInput.onValueChanged.AddListener(HandleDifficultyChange);

        spawnDelayInput.text = PlayerPrefs.GetFloat("SpawnDelay", 2).ToString();
        spawnDelayInput.onValueChanged.AddListener(HandleSpawnDelayChange);
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
            config.easyWordsFile = "easy";
            config.hardWordsFile = "hard";
        }
    }

    void HandleDifficultyChange(string value)
    {
        if (float.TryParse(value, out float difficultyValue))
        {
            PlayerPrefs.SetFloat("DifficultyMult", difficultyValue);
            PlayerPrefs.Save();
        }
        else
        {
            PlayerPrefs.SetFloat("DifficultyMult", config.difficultyMultiplier);
            PlayerPrefs.Save();
        }
    }

    void HandleSpawnDelayChange(string value)
    {
        if (float.TryParse(value, out float delayValue))
        {
            PlayerPrefs.SetFloat("SpawnDelay", delayValue);
            PlayerPrefs.Save();
        }
        else
        {
            PlayerPrefs.SetFloat("SpawnDelay", config.initialSpawnDelay);
            PlayerPrefs.Save();
        }
    }
}