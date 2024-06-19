using System;
using UnityEngine;

[Serializable]
public class ScoreEntry {

    [SerializeField] private string name;
    [SerializeField] private int score;
    [SerializeField] private float averageSpeed;
    [SerializeField] private float accuracy;
    [SerializeField] private int wordsTyped;

    public string Name {
        get { return name; }
        set {
            name = ParseName(value);
        }
    }
    
    public int Score { get { return score; } set { score = value; } }
    public float AverageSpeed { get { return averageSpeed; } set { averageSpeed = value; } }
    public float Accuracy { get { return accuracy; } set { accuracy = value; } }
    public int WordsTyped { get { return wordsTyped; } set { wordsTyped = value; } }

    public ScoreEntry(string name, int score) {
        Name = name;
        Score = score;
    }

    public ScoreEntry(string name, int score, float averageSpeed, float accuracy, int wordsTyped) {
        Name = name;
        Score = score;
        AverageSpeed = averageSpeed;
        Accuracy = accuracy;
        WordsTyped = wordsTyped;
    }

    private string ParseName(string name) {
        if (name.Length > 3)
            return name.Substring(0, 3);
        return name;
    }
}