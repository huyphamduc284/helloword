#if UNITY_EDITOR
using System;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

[InitializeOnLoad]
public class AutoSave
{
    private static DateTime nextSaveTime;
    
    // Static constructor that gets called when unity fires up.
    static AutoSave()
    {
        EditorApplication.playModeStateChanged += (PlayModeStateChange state) => {
            // If we're about to run the scene...
            if (!EditorApplication.isPlayingOrWillChangePlaymode || EditorApplication.isPlaying) return;
        
            // Save the scene and the assets.
            Debug.Log("Auto-saving all open scenes... " + state);
            EditorSceneManager.SaveOpenScenes();
            AssetDatabase.SaveAssets();
        };
        
        // Also, every five minutes.
        nextSaveTime = DateTime.Now.AddMinutes(4);
        EditorApplication.update += Update;
        // Debug.Log("Added callback.");
    }

    private static void Update()
    {
        if (nextSaveTime > DateTime.Now || EditorApplication.isPlaying) return;
        
        nextSaveTime = nextSaveTime.AddMinutes(4);
        
        Debug.Log("Auto-saving scenes... "+DateTime.Now.ToShortTimeString());
        EditorSceneManager.SaveOpenScenes();
        AssetDatabase.SaveAssets();
    }
}
#endif