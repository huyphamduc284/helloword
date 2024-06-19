public class Word {
    public static readonly Word defaultWord = new Word("default", DifficultyLevel.Easy);
    public string text { get; private set; }
    private DifficultyLevel wordDifficulty;

    public Word(string text, DifficultyLevel wordDifficulty) {
        this.text = text;
        this.wordDifficulty = wordDifficulty;
    }

}