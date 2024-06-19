using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Assertions;

/// <summary>
/// There is one for every active word
/// Responsible for displaying and moving a single word
/// </summary>
public class WordController : MonoBehaviour {

    public static WordController wc;

    private float startTime;
    private float secondsElapsed = 0f;

    private string styleSelectedOpen = "<style=\"Selected\">";
    private string styleSelectedClose = "</style>";
    private string styleTypedOpen = "<style=\"typed\">";
    private string styleTypedClose = "</style>";
    private int index = 0; // Number of letters already typed in this word
    private TMP_Text displayText;

    public Word word = Word.defaultWord;
    public int StrokesMissed { get; private set; } = 0;
    public float FallSpeed { get; set; } = 1f; // to be set by GameManager / ProgressManager...
    public bool TimerRunning { get; private set; } = false;
    [SerializeField] private Sprite[] sprites; // Assign your 4 sprites in the Unity Editor
    [SerializeField] private SpriteRenderer prefabSpriteRenderer; // Assign your prefab's SpriteRenderer in the Unity Editor

    public void SetSpriteBasedOnWordLength(string word)
    {
        int spriteIndex;

        if (word.Length <= 4)
        {
            spriteIndex = 3;
            SetColor(new Color(50 / 255f, 173 / 255f, 214 / 255f, 1));
            AdjustScale(-0.15f);
            SetTrailTime(1.5f);
        }
        else if (word.Length <= 6)
        {
            spriteIndex = 0;
            SetColor(new Color(214 / 255f, 50 / 255f, 50 / 255f, 1));
            SetTrailTime(2f);
        }
        else if (word.Length <= 10)
        {
            spriteIndex = 1;
            SetColor(new Color(230 / 255f, 167 / 255f, 54 / 255f, 1));
            SetTrailTime(5f);
        }
        else if (word.Length > 10)
        {
            spriteIndex = 2;
            SetColor(new Color(50 / 255f, 173 / 255f, 214 / 255f, 1));
            SetTrailTime(5f);
        }
        else
        {
            spriteIndex = 0;
            SetColor(new Color(50 / 255f, 173 / 255f, 214 / 255f, 1));
        }
        //Debug.Log(spriteIndex);

        prefabSpriteRenderer.sprite = sprites[spriteIndex];
    }

    public void SetTrailTime(float time)
    {
        TrailRenderer trail = prefabSpriteRenderer.GetComponent<TrailRenderer>();
        trail.time = time;
    }

    public void SetColor(Color color)
    {
        prefabSpriteRenderer.color = color;
    }

    public void AdjustScale(float scaleChange)
    {
        this.transform.localScale += new Vector3(scaleChange, scaleChange, scaleChange);
    }

    public void SetScale(float scale)
    {
        this.transform.localScale = new Vector3(scale, scale, scale);
    }

    private void Awake() {
        wc = this;
        displayText = GetComponent<TMP_Text>();
    }

    private void Start() {
        Theme theme = GameManager.Instance.Theme;
        displayText.color = theme.foreground;

        styleTypedOpen += $"<color=#{theme.foregroundTyped.ToHexString()}>";
        styleTypedClose = "</color>" + styleTypedClose;
    }

    private void Update() {
        transform.Translate(0f, -FallSpeed * Time.deltaTime, 0f);
    }

    public void SetWord(Word word) {
        this.word = word;
        displayText.text = word.text;
        displayText.text = displayText.text.Replace(' ', '·');
        SetSpriteBasedOnWordLength(word.text);
    }

    public bool IsNextLetter(char letter) {
        return letter == word.text[index];
    }

    public void TypeLetter() {
        index++;
        Assert.IsFalse(index > word.text.Length);

        // add rich text tags
        string newText = 
            $"{styleSelectedOpen}{styleTypedOpen}{word.text.Substring(0, index)}{styleTypedClose}{word.text.Substring(index)}{styleSelectedClose}";

        displayText.text = newText;
        displayText.text = displayText.text.Replace(' ', '·');
    }

    public void DestroySelf(float delay = 0f) {
        Destroy(gameObject, delay);
    }

    public bool WordTyped() {
        return word.text.Length == index;
    }

    public void ToggleTimer() {
        if (TimerRunning) {
            secondsElapsed += Time.time - startTime;
            string newText = $"{styleTypedOpen}{word.text.Substring(0, index)}{styleTypedClose}{word.text.Substring(index)}";
            displayText.text = newText;
            displayText.text = displayText.text.Replace(' ', '·');
        } else {
            startTime = Time.time;
        }
        TimerRunning = !TimerRunning;
    }

    public float GetSecondsElapsed() {
        Assert.IsFalse(TimerRunning);
        return secondsElapsed;
    }

    public string GetWordString() {
        return word.text;
    }

    public void AddMiss() {
        StrokesMissed++;
    }
}