using System.Collections.Generic;
using System.Linq;

public class TypingSpeedCalculator {

    private List<WordCPM> typedWords = new List<WordCPM>();
    private float defaultTypingSpeed = 100;
    public float AverageCPM {
        get {
            if (typedWords.Count() == 0)
                return defaultTypingSpeed;
            
            return CalculateTypingSpeed(
                typedWords.Select(c => c.word.Count()).Sum(),
                typedWords.Select(c => c.seconds).Sum()
            );
        }
    }

    public float AverageCPMLast10 {
        get {
            if (typedWords.Count() <= 10)
                return AverageCPM;

            return CalculateTypingSpeed(
                typedWords.TakeLast(10).Select(c => c.word.Count()).Sum(),
                typedWords.TakeLast(10).Select(c => c.seconds).Sum()
            );
        }
    }

    public float Accuracy {
        get {
            if (typedWords.Count() == 0)
                return 1;
            
            return (float)typedWords.Select(c => c.word.Count()).Sum() /
               typedWords.Select(c => c.word.Count() + c.misses).Sum();
        }
    }

    public float AccuracyLast10 {
        get {
            if (typedWords.Count() <= 10)
                return Accuracy;
            
            return (float)typedWords.TakeLast(10).Select(c => c.word.Count()).Sum() /
               typedWords.TakeLast(10).Select(c => c.word.Count() + c.misses).Sum();
        }
    }

    public int WordsTyped {
        get { return typedWords.Count(); }
    }

    public void AddWordCPM(WordCPM wordSpeed) {
        typedWords.Add(wordSpeed);
    }

    private float CalculateTypingSpeed(int characters, float secondsElapsed) {
        return characters / (secondsElapsed / 60);
    }
}