using System.Linq;

public class ScoreCalculator {
    public int CurrentScore { get; private set; }

    public void WordTyped(WordCPM wordCPM, float averageCPMLast10) {
        GameManager gm = GameManager.Instance;

        int perfectBonus = gm.config.perfectWordBonus;
        int pointsPerCharacter = gm.config.pointsPerCharacter;

        float cpmMultiplier = gm.getCPMScoreMultiplier(averageCPMLast10);
        float wordAccuracy = (float)wordCPM.word.Count() /
            (wordCPM.word.Count() + wordCPM.misses);

        CurrentScore += (int)(wordCPM.word.Count() * pointsPerCharacter * cpmMultiplier * wordAccuracy);
        CurrentScore += wordAccuracy == 1f ? perfectBonus : 0;
    }
}