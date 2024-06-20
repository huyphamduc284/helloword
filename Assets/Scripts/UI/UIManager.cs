using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Linq;
using System;
using UnityEditor;

public class UIManager : MonoBehaviour {
    private Theme theme;

    [Header("HUD")]
    [SerializeField] private Image hudBg;
    [SerializeField] private TMP_Text averageCPMLabel;
    [SerializeField] private TMP_Text averageCPMLast10Label;
    [SerializeField] private TMP_Text averageCPM;
    [SerializeField] private TMP_Text averageCPMLast10;
    [SerializeField] private TMP_Text scoreLabel;
    [SerializeField] private TMP_Text score;
    [SerializeField] private TMP_Text accuracyLabel;
    [SerializeField] private TMP_Text accuracy;
    [SerializeField] private TMP_Text wordsTypedLabel;
    [SerializeField] private TMP_Text wordsTyped;
    [SerializeField] private TMP_Text versionLabel;
    // Lives left:
    [SerializeField] private GameObject liveTemplate;
    [SerializeField] private Transform livesContainer;


    [Header("Game Over Screen")]
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private Image gameOverBackground;
    [SerializeField] private TMP_Text gameOverTitle;
    [SerializeField] private Button restartButton;
    [SerializeField] private Button gotoMenuButton;
    [SerializeField] private TMP_InputField nameInputField;
    [SerializeField] private TMP_Text gameOverScore;
    [SerializeField] private TMP_Text gameOverScoreLabel;
    [SerializeField] private TMP_Text gameOverAccuracy;
    [SerializeField] private TMP_Text gameOverAccuracyLabel;
    [SerializeField] private TMP_Text gameOverWordsTyped;
    [SerializeField] private TMP_Text gameOverWordsTypedLabel;


    [Header("Pause Screen")]
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private Image pauseBackground;
    [SerializeField] private TMP_Text pauseTitle;
    [SerializeField] private Button pauseRestartButton;
    [SerializeField] private Button pauseGotoMenuButton;
    [SerializeField] private Button pauseResumeGameButton;

    private void Start() {

        theme = GameManager.Instance.Theme;

        ColorBlock colorBlock = new ColorBlock {
            normalColor = theme.backgroundUI,
            highlightedColor = theme.backgroundUI,
            pressedColor = theme.backgroundUI,
            disabledColor = theme.backgroundUI,
            selectedColor = theme.backgroundUI,
            colorMultiplier = 1f
        };

        // Set HUD Colors
        Camera.main.backgroundColor = theme.background;
        averageCPMLast10Label.color = theme.foregroundUI;
        averageCPMLast10.color = theme.foregroundTyped;
        averageCPMLabel.color = theme.foregroundUI;
        averageCPM.color = theme.foregroundTyped;
        scoreLabel.color = theme.foregroundUI;
        score.color = theme.foregroundTyped;
        accuracyLabel.color = theme.foregroundUI;
        accuracy.color = theme.foregroundTyped;
        wordsTypedLabel.color = theme.foregroundUI;
        wordsTyped.color = theme.foregroundTyped;
        versionLabel.color = theme.foregroundUI;
        hudBg.color = theme.backgroundUI;

        // Set Game Over Screen colors
        gameOverPanel.SetActive(false);
        gameOverBackground.color = theme.background;
        gameOverTitle.color = theme.foregroundUI;
        gameOverScoreLabel.color = theme.foregroundUI;
        gameOverScore.color = theme.foregroundTyped;
        gameOverAccuracyLabel.color = theme.foregroundUI;
        gameOverAccuracy.color = theme.foregroundTyped;
        gameOverWordsTypedLabel.color = theme.foregroundUI;
        gameOverWordsTyped.color = theme.foregroundTyped;
        restartButton.colors = colorBlock;
        restartButton.GetComponentInChildren<MenuButtonHover>().baseColor = theme.foreground;
        restartButton.GetComponentInChildren<MenuButtonHover>().hoverColor = theme.foregroundTyped;
        gotoMenuButton.colors = colorBlock;
        gotoMenuButton.GetComponentInChildren<MenuButtonHover>().baseColor = theme.foreground;
        gotoMenuButton.GetComponentInChildren<MenuButtonHover>().hoverColor = theme.foregroundTyped;

        // Set pause menu colors
        pausePanel.SetActive(false);
        pauseBackground.color = theme.background;
        pauseTitle.color = theme.foregroundUI;
        pauseGotoMenuButton.colors = colorBlock;
        pauseGotoMenuButton.GetComponentInChildren<MenuButtonHover>().baseColor = theme.foreground;
        pauseGotoMenuButton.GetComponentInChildren<MenuButtonHover>().hoverColor = theme.foregroundTyped;
        pauseRestartButton.colors = colorBlock;
        pauseRestartButton.GetComponentInChildren<MenuButtonHover>().baseColor = theme.foreground;
        pauseRestartButton.GetComponentInChildren<MenuButtonHover>().hoverColor = theme.foregroundTyped;
        pauseResumeGameButton.colors = colorBlock;
        pauseResumeGameButton.GetComponentInChildren<MenuButtonHover>().baseColor = theme.foreground;
        pauseResumeGameButton.GetComponentInChildren<MenuButtonHover>().hoverColor = theme.foregroundTyped;

        // Set version text
        versionLabel.text = "URP " + Application.version;

        // Disable live left template
        liveTemplate.GetComponent<Image>().color = theme.foregroundTyped;
        SetLivesLeft(GameManager.Instance.config.missesAllowed);
    }

    public void SetAverageCPMText(float speed) {
        if (float.IsInfinity(speed))
        {
            averageCPM.text = "!!!";
        }
        else
        {
            averageCPM.text = $"{speed:0}";
        }
    }

    public void SetAverageCPMLast10Text(float speed)
    {
        if (float.IsInfinity(speed))
        {
            averageCPMLast10.text = "!!!";
        }
        else
        {
            averageCPMLast10.text = $"({speed:0})";
        }
    }

    public void SetScoreText(int score) {
        TypingSpeedCalculator speedCalculator = GameManager.Instance.SpeedCalculator;

        this.score.text = score.ToString();
        accuracy.text = $"{speedCalculator.Accuracy * 100:0.0}";
        wordsTyped.text = $"{speedCalculator.WordsTyped}";
    }

    public void SetLivesLeft(int livesLeft) {
        for (int i = 0; i < livesContainer.childCount; i++) {
            Destroy(livesContainer.GetChild(i).gameObject);
        }

        for (int i = 0; i <= livesLeft; i++) {
            Instantiate(liveTemplate, livesContainer).GetComponent<Image>().enabled = true;
        }
    }

    public void ShowGameOverMenu() {
        TypingSpeedCalculator speedCalculator = GameManager.Instance.SpeedCalculator;

        gameOverPanel.SetActive(true);
        gameOverScore.text = score.text;
        gameOverAccuracy.text = $"{speedCalculator.Accuracy * 100:0.0}%";
        gameOverWordsTyped.text = $"{speedCalculator.WordsTyped}";
        nameInputField.Select();
        nameInputField.characterLimit = 3;
        nameInputField.onValueChanged.AddListener(delegate { ValueChangeCheck(); });
    }

    public void ValueChangeCheck() {
        nameInputField.text = nameInputField.text.Trim().ToUpper();
    }

    public void ExitToMenu() {
        SavePlayToJson();
        SceneManager.LoadScene(0);
    }

    public void Restart() {
        SavePlayToJson();
        GameManager.Instance.RestartGame();
    }

    public void Resume() {
        GameManager.Instance.TogglePause();
    }

    private void SavePlayToJson() {
        TypingSpeedCalculator speedCalculator = GameManager.Instance.SpeedCalculator;
        ScoreCalculator scoreCalculator = GameManager.Instance.ScoreCalculator;

        ScoreEntry scoreEntry = new(
            name: ParseName(nameInputField.text),
            score: scoreCalculator.CurrentScore,
            averageSpeed: speedCalculator.AverageCPM,
            accuracy: speedCalculator.Accuracy,
            wordsTyped: speedCalculator.WordsTyped
        );
        ScoreJsonSerializer.AddScore(scoreEntry);
    }

    private string ParseName(string name) {
        if (name.Count() == 3)
            return name;
        if (name.Count() == 0)
            return "???";
        return name.PadRight(3, '-');
    }

    public void TogglePauseScreen() {
        pausePanel.SetActive(!pausePanel.activeSelf);
    }
}