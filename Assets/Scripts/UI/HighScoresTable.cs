using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class HighScoresTable : MonoBehaviour {
    [SerializeField] private Transform entryTemplate;
    [SerializeField] private Transform entryContainer;
    [SerializeField] private GameObject placeholder;
    private List<Transform> entries = new List<Transform>();
    private ScoreEntry devScore = new("MAR", 12364); // My personal best

    private void Awake() {
        entryTemplate.gameObject.SetActive(false);

        List<ScoreEntry> scores = ScoreJsonSerializer.GetHighscores();

        bool scoresIsEmpty = scores.Count == 0;
        placeholder.SetActive(scoresIsEmpty);

        if (!scoresIsEmpty) {
            SetHighScores(scores);
        }
    }

    public void ClearHighScores() {
        // delete highscores from storage
        ScoreJsonSerializer.ClearHighScores();

        // remove highscores from UI
        entries.ForEach(entry => Destroy(entry.gameObject));
        entries.Clear();

        placeholder.SetActive(true);
    }

    private void SetHighScores(List<ScoreEntry> scores) {
        scores.Add(devScore);
        scores = scores.OrderBy(s => s.Score).Reverse().Take(10).ToList();

        float templateHeight = entryTemplate.GetComponent<RectTransform>().sizeDelta.y;

        for (int i = 0; i < scores.Count(); i++) {
            Transform entryTransform = Instantiate(entryTemplate, entryContainer);
            entries.Add(entryTransform);
            RectTransform entryRectTransform = entryTransform.GetComponent<RectTransform>();

            entryRectTransform.anchoredPosition = new Vector2(0, -templateHeight * i);
            entryTransform.gameObject.SetActive(true);

            int rank = i + 1;
            entryTransform.Find("posText").GetComponent<TMP_Text>().text = rank.ToString();
            entryTransform.Find("scoreText").GetComponent<TMP_Text>().text = scores[i].Score.ToString();
            entryTransform.Find("nameText").GetComponent<TMP_Text>().text = scores[i].Name;

            HandleExceptions(scores[i], entryTransform);
        }
    }

    private void HandleExceptions(ScoreEntry score, Transform entryTransform) {
        if (score.Name == "MCR") {
            entryTransform.Find("nameText").GetComponent<TMP_Text>().color = Themes.serikaDark.foregroundUI;
        }
        if (score == devScore) {
            entryTransform.Find("nameText").GetComponent<TMP_Text>().color = Themes.serikaDark.foregroundTyped;
        }
    }
}
