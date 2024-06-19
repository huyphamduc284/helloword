using System.Collections;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TMP_Text))]
public class TypeWriterEffect : MonoBehaviour {

    private TMP_Text textBox;
    private int currentVisibleCharacterIndex;
    private Coroutine typewriterCoroutine;

    private WaitForSeconds simpleDelay;
    private WaitForSeconds initalDelay;

    [Header("Settings")]
    [SerializeField] private float charactersPerSecond = 20f;
    [SerializeField] private float initialSecondsDelay = 5f;

    private void Awake() {
        Time.timeScale = 1;
        textBox = GetComponent<TMP_Text>();

        simpleDelay = new WaitForSeconds(1 / charactersPerSecond);
        initalDelay = new WaitForSeconds(initialSecondsDelay);
    }

    private void OnEnable() {
        PlayTitleAnimation();
    }

    private void PlayTitleAnimation() {
        if (typewriterCoroutine != null) {
            StopCoroutine(typewriterCoroutine);
        }

        textBox.maxVisibleCharacters = 0;
        currentVisibleCharacterIndex = 0;

        typewriterCoroutine = StartCoroutine(Typewriter());
    }

    private IEnumerator Typewriter() {
        yield return initalDelay;
        TMP_TextInfo textInfo = textBox.textInfo;
        while (currentVisibleCharacterIndex < textInfo.characterCount) {
            textBox.maxVisibleCharacters++;

            yield return simpleDelay;

            currentVisibleCharacterIndex++;
        }

    }
}