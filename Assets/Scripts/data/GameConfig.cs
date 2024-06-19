using System;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "GameConfig", menuName = "UT/GameConfig", order = 0)]
[Serializable]
public class GameConfig : ScriptableObject {
    [Header("Random words text files")]
    public string easyWordsFile;
    public string mediumWordsFile;
    public string hardWordsFile;

    [Header("Game parameters")]
    public float initialSpawnDelay;
    public float easyFallSpeed;
    public float mediumFallSpeed;
    public float hardFallSpeed;

    [Tooltip("This value is a percentage applied to the timed delay multiplier every time a word is spawned, it should be between 0 and 1")]
    [Min(0f)]
    public float spawnDelayTimedPercentage;
    public int missesAllowed;

    [Tooltip("Between 0 and one")]
    [Min(0f)]
    public float hardWordChance;
    public float hardWordCoolDown;

    [Header("Multipliers")]
    public List<Interval> CPMIntervals;


    [Header("Score variables")]
    public int perfectWordBonus;
    public int pointsPerCharacter;
    


    [HideInInspector]
    public TypingSpeedUnit speedUnit;
    public string typingSpeedUnitString;
    private void Start() {
        speedUnit = TypingSpeedUnits.getUnit(typingSpeedUnitString);
    }

}