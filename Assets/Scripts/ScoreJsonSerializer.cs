using System.Collections.Generic;
using UnityEngine;

public class ScoreJsonSerializer {

    private static SerializableHighscores LoadScoresFromJson() {
        if (PlayerPrefs.HasKey("scores")) {
            return JsonUtility.FromJson<SerializableHighscores>(PlayerPrefs.GetString("scores"));
        }

        return new SerializableHighscores{scores = new List<ScoreEntry>()};
    }    

    private static void SaveScoresToJson(SerializableHighscores serializableHighscores) {
        string json = JsonUtility.ToJson(serializableHighscores);
        PlayerPrefs.SetString("scores", json);
    }

    public static void AddScore(ScoreEntry scoreEntry) {
        SerializableHighscores highscores = LoadScoresFromJson();
        highscores.scores.Add(scoreEntry);
        SaveScoresToJson(highscores);
    }

    public static void ClearHighScores() {
        SerializableHighscores serializableHighscores = new SerializableHighscores{scores = new List<ScoreEntry>()};
        string json = JsonUtility.ToJson(serializableHighscores);
        PlayerPrefs.SetString("scores", json);
    }

    public static List<ScoreEntry> GetHighscores() {
        return LoadScoresFromJson().scores;
    }

    private class SerializableHighscores {
        public List<ScoreEntry> scores;
    }

}