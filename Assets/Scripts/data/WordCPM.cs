public struct WordCPM {
    public string word;
    public float seconds;
    public int misses;

    public WordCPM(string word, float seconds, int misses) {
        this.word = word;
        this.seconds = seconds;
        this.misses = misses;
    }
}