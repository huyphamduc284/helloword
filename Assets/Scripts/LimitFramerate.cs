using UnityEngine;

public class LimitFramerate {
    [RuntimeInitializeOnLoadMethod]
    static void OnRuntimeInitialized() {
        // Limit the framerate to 144 fps
        Application.targetFrameRate = 144;
    }
}