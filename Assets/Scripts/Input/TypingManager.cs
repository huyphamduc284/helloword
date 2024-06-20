using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

/// <summary>
/// Translates user input into actions and effects
/// Only one instance of this script should exist on scene
/// </summary>
public class TypingManager : MonoBehaviour {
    private List<WordController> wordControllers;
    public WordController activeWord = null;
    public Vector3 oldActiveWordPosition;
    private ProgressionManager progressionManager;
    [SerializeField] private GameObject explosionEffect;
    [SerializeField] private GameObject letterMissEffect;
    public static TypingManager tm;

    private void Awake()
    {
        tm = this;
    }

    private void Start() {
        wordControllers = GameManager.Instance.WordControllers;
        progressionManager = GetComponent<ProgressionManager>();
    }

    public void TypeLetter(char letter) {
        // If there's no active word and no word starts by the typed letter then return. 
        if (!activeWord && !TryFindWord(letter)) {
            GameManager.Instance.SoundManager.PlaySound(Sound.LetterMissed);
            return;
        }

        Assert.IsNotNull(activeWord);
        if (activeWord.IsNextLetter(letter)) {
            GameManager.Instance.SoundManager.PlaySound(Sound.LetterTyped);
            CameraController.c.Shake(0.05f, 0.1f, 15.0f);
            Turret.t.Shoot();

            activeWord.TypeLetter();
            if (GameManager.Instance.Powerups.IsPowerActive("InstantKill"))
            {
                if (activeWord.isPowerup)
                {
                    GameManager.Instance.Powerups.ActivatePowerup("InstantKill", 5);
                }
                DestroyWord(activeWord, 0.1f);
            }
            if (activeWord.WordTyped()) {
                if (activeWord.isPowerup)
                {
                    GameManager.Instance.Powerups.ActivatePowerup("InstantKill", 5);
                }
                // remove word if already typed
                activeWord.ToggleTimer();
                progressionManager.AddToAverageCPM(activeWord);

                DestroyWord(activeWord, 0.1f);
                activeWord = null;
            }
        } else {
            Instantiate(letterMissEffect, Turret.t.transform.position, Quaternion.identity);
		    CameraController.c.Shake(0.1f, 0.2f, 20.0f);
            GameManager.Instance.SoundManager.PlaySound(Sound.LetterMissed);

            activeWord?.AddMiss();
        }
    }

    public void DestroyMissedWord(WordController word, bool spawnEffect = true) {

        if (activeWord == word) {
            activeWord = null;
        }

        DestroyWord(word, spawnEffect: spawnEffect);
    }

    private void DestroyWord(WordController word, float delay = 0f, bool spawnEffect = true)
    {
        if (spawnEffect)
        {
            Instantiate(explosionEffect, word.transform.position, Quaternion.identity);
            GameManager.Instance.SoundManager.PlaySound(Sound.WordTyped);
            CameraController.c.isShakingCam = false;
            CameraController.c.Shake(0.1f, 0.2f, 40.0f);
        }

        word.DestroySelf(delay); // POSSIBLE RACE CONDITION
        wordControllers.Remove(word);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Word") && other.TryGetComponent<WordController>(out WordController word))
        {
            Transform child = other.transform.Find("Enemy");
            if (child != null && child.CompareTag("Enemy"))
            {
                DestroyMissedWord(word);
                progressionManager.MissWord();
                return;
            }
            DestroyMissedWord(word, spawnEffect: false);
        }
    }


    // Find the word starting by `letter` that has the lowest y position
    private bool TryFindWord(char letter) {
        foreach (var word in wordControllers) {
            bool validLetter = word.IsNextLetter(letter);
            bool activeWordIsNullOrFar = !activeWord || (activeWord && word.transform.position.y < activeWord.transform.position.y);
            if (validLetter && activeWordIsNullOrFar) {
                activeWord = word;
                if (activeWord != null)
                {
                    //Debug.Log(oldActiveWordPosition);
                    oldActiveWordPosition = activeWord.transform.position;
                }
            }
        }
        if (activeWord) {
            activeWord.ToggleTimer();
            return true;
        }
        return false;
    }

    public void StopFocus() {
        if (activeWord != null) {
            activeWord.ToggleTimer();
            activeWord = null;
        }
    }
}
