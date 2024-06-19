/// <summary>
/// Min inclusive, max exclusive
/// </summary>
[System.Serializable]
public struct Interval {
    public float minCPMValue;
    public float maxCPMValue;

    public float fallSpeedMultiplier;
    public float spawnDelayMultiplier;

    public float scoreMultiplier;
}