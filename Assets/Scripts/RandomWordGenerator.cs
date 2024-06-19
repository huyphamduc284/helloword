using UnityEngine;

public class RandomWordGenerator : MonoBehaviour {
    private string[] easyWords;
    private string[] hardWords;

    public Word generateWord(DifficultyLevel difficulty) {
        if (difficulty == DifficultyLevel.Easy)
            return new Word(easyWords[Random.Range(0, easyWords.Length)], difficulty);
        return new Word(hardWords[Random.Range(0, hardWords.Length)], difficulty);
    }

    private void Start() {
        TextAsset easyFile = Resources.Load<TextAsset>(GameManager.Instance.config.easyWordsFile);
        TextAsset hardFile = Resources.Load<TextAsset>(GameManager.Instance.config.hardWordsFile);
        
        // Split files by line feed
        easyWords = easyFile.text.Replace("\r", "").Split('\n');
        hardWords = hardFile.text.Replace("\r", "").Split('\n');

        for (int i = 0; i < easyWords.Length; i++) {
            easyWords[i] = easyWords[i].ToLower();
        }
        for (int i = 0; i < hardWords.Length; i++) {
            hardWords[i] = hardWords[i].ToLower();
        }
    }
}