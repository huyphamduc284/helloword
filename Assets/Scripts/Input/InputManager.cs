using UnityEngine;

public class InputManager : MonoBehaviour {

    private TypingManager typingManager;

    private void Start() {
        typingManager = GetComponent<TypingManager>();
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.Tab)) {
            typingManager.StopFocus();
        }

        if (Input.GetKeyDown(KeyCode.Escape)) {
            GameManager.Instance.TogglePause();
        }

        foreach (char letter in Input.inputString) {
            typingManager.TypeLetter(letter);
        }
    }

}
