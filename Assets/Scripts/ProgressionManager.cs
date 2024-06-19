using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressionManager : MonoBehaviour {
    private EntitySpawner spawner;
    private GameManager gameManager;
    private GameConfig config;

    private TypingSpeedCalculator speedCalculator;

    private bool canSpawnHardWord = true;
    private int currentMisses = 0;

    private Dictionary<DifficultyLevel, float> baseSpeedDictionary;

    private float spawnDelayTimedMultiplier = 1f; // changes based on time
    private ScoreCalculator scoreCalculator;

    private void Start() {
        gameManager = GameManager.Instance;
        speedCalculator = gameManager.SpeedCalculator;
        spawner = GetComponent<EntitySpawner>();
        scoreCalculator = gameManager.ScoreCalculator;
        config = gameManager.config;
        baseSpeedDictionary = new Dictionary<DifficultyLevel, float> {
            [DifficultyLevel.Easy] = config.easyFallSpeed,
            [DifficultyLevel.Medium] = config.mediumFallSpeed,
            [DifficultyLevel.Hard] = config.hardFallSpeed,
        };

        StartCoroutine(SpawnRepeating());
    }

    IEnumerator SpawnRepeating() {
        while (gameManager.IsGameActive) {
            WordController spawnedWord;
            DifficultyLevel wordLevel;

            // decide whether to generate hard or easy word
            wordLevel = GenerateDifficultyLevel();
            Debug.Log(wordLevel);
            spawnedWord = spawner.SpawnWordGameObject(wordLevel);

            // set word fallspeed
            spawnedWord.FallSpeed = GenerateFallSpeed(wordLevel);

            // wait for spawn delay
            yield return new WaitForSeconds(CalculateSpawnDelay());
        }
    }
    private float CalculateSpawnDelay() {
        float spawnDelay = config.initialSpawnDelay
            * spawnDelayTimedMultiplier
            * gameManager.getCPMSpawnDelayMultiplier(speedCalculator.AverageCPMLast10);

        spawnDelayTimedMultiplier *= gameManager.config.spawnDelayTimedPercentage;

        return spawnDelay;
    }

    IEnumerator WaitForHardWordCoolDown() {
        canSpawnHardWord = false;
        yield return new WaitForSeconds(config.hardWordCoolDown);
        canSpawnHardWord = true;
    }

    private DifficultyLevel GenerateDifficultyLevel() {
        int randomNumber = Random.Range(0, 10);
        Debug.Log(randomNumber);
        if (randomNumber <= 4)
        {
            return DifficultyLevel.Easy;
        }
        else if (randomNumber <= 6)
        {
            return DifficultyLevel.Medium;
        }
        else if (randomNumber <= 10 && canSpawnHardWord /*&& Random.Range(0f, 1f) < config.hardWordChance*/) {
            StartCoroutine(WaitForHardWordCoolDown());
            return DifficultyLevel.Hard;
        }
        return DifficultyLevel.Easy;
    }

    private float GenerateFallSpeed(DifficultyLevel level) {
        return baseSpeedDictionary[level] * gameManager.GetCPMFallSpeedMultiplier(speedCalculator.AverageCPMLast10);
    }

    public void AddToAverageCPM(WordController word) {
        WordCPM wordCPM = new WordCPM(word.GetWordString(), word.GetSecondsElapsed(), word.StrokesMissed);

        speedCalculator.AddWordCPM(wordCPM);
        scoreCalculator.WordTyped(wordCPM, speedCalculator.AverageCPMLast10);

        gameManager.UIManager.SetAverageCPMText(speedCalculator.AverageCPM);
        gameManager.UIManager.SetAverageCPMLast10Text(speedCalculator.AverageCPMLast10);
        gameManager.UIManager.SetScoreText(scoreCalculator.CurrentScore);
    }

    public void MissWord() {
        currentMisses++;
        gameManager.SoundManager.PlaySound(Sound.WordMissed);
        gameManager.UIManager.SetLivesLeft(gameManager.config.missesAllowed - currentMisses);
        if (currentMisses > gameManager.config.missesAllowed) {
            gameManager.GameOver();
        }
    }
}