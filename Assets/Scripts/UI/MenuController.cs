using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MenuController : MonoBehaviour {

    [SerializeField] private GameConfig config;
    [SerializeField] public TMP_Text difficultyText;
    [SerializeField] private GameObject settingsMenu;
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject highscoresMenu;
    [SerializeField] private GameObject confirmExitDialogue;

    private void Start() {
        mainMenu.SetActive(true);
        settingsMenu.SetActive(false);
        highscoresMenu.SetActive(false);
        UpdateDifficultyUI();
    }

    public void Play() {
        SceneManager.LoadScene(1);
    }

    public void ShowSettings() {
        settingsMenu.SetActive(true);
        mainMenu.SetActive(false);
    }

    public void HideSettings() {
        settingsMenu.SetActive(false);
        mainMenu.SetActive(true);
        UpdateDifficultyUI();
    }

    public void ShowHighscores() {
        highscoresMenu.SetActive(true);
        mainMenu.SetActive(false);
    }

    public void HideHighscores() {
        highscoresMenu.SetActive(false);
        mainMenu.SetActive(true);
    }

    public void ShowConfirmExitDialogue() {
        confirmExitDialogue.SetActive(true);
        mainMenu.SetActive(false);
    }

    public void HideConfirmExitDialogue() {
        confirmExitDialogue.SetActive(false);
        mainMenu.SetActive(true);
    }

    public void QuitGame() {
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
        Application.Quit();
    }

    void UpdateDifficultyUI()
    {
        float difficulty = PlayerPrefs.GetFloat("DifficultyMult", config.difficultyMultiplier);
        difficultyText.text = "Difficulty: " + difficulty.ToString();
    }
}