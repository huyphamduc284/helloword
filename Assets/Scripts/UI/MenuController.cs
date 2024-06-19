using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour {

    [SerializeField] private GameObject settingsMenu;
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject highscoresMenu;
    [SerializeField] private GameObject confirmExitDialogue;

    private void Start() {
        mainMenu.SetActive(true);
        settingsMenu.SetActive(false);
        highscoresMenu.SetActive(false);
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
}