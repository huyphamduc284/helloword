using System;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {

    [Serializable]
    private class SoundAudioClip {
        public Sound sound;
        public AudioClip audioClip;
    }

    [SerializeField]
    private SoundAudioClip[] soundAudioClipArray;
    public Dictionary<Sound, AudioClip> soundAudioClipMap;

    private void Start() {
        InitializeSoundAudioClipMap();
    }

    private void InitializeSoundAudioClipMap() {
        soundAudioClipMap = new Dictionary<Sound, AudioClip>();
        foreach (var sound in soundAudioClipArray) {
            soundAudioClipMap.Add(sound.sound, sound.audioClip);
        }
    }

    public void PlaySound(Sound sound) {
        if (!soundAudioClipMap.ContainsKey(sound)) {
            Debug.LogWarning($"Sound {sound} not implemented");
            return;
        }

        GameObject soundGo = new GameObject("Sound");
        AudioSource audioSource = soundGo.AddComponent<AudioSource>();
        audioSource.clip = soundAudioClipMap[sound];
        audioSource.Play();
        Destroy(soundGo, audioSource.clip.length);
    }
}