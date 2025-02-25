using UnityEngine;
using System;
using System.Collections.Generic;

public class SoundManager : MonoBehaviour
{
    private const string SoundVolumeKey = "SoundVolume";
    public static SoundManager Instance { get; private set; }

    [Serializable]
    public class SoundClip
    {
        public string soundName;
        public AudioClip clip;
    }

    public List<SoundClip> sounds = new List<SoundClip>();
    public int maxAudioSources = 10; // Лимит активных источников звука
    private Queue<AudioSource> availableSources = new Queue<AudioSource>();
    private float soundVolume = 1f;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            InitializeAudioSources();
            LoadVolume();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void InitializeAudioSources()
    {
        for (int i = 0; i < maxAudioSources; i++)
        {
            AudioSource source = gameObject.AddComponent<AudioSource>();
            source.playOnAwake = false;
            availableSources.Enqueue(source);
        }
    }

    private void LoadVolume()
    {
        soundVolume = PlayerPrefs.GetFloat(SoundVolumeKey, 1f);
    }

    private void SaveVolume()
    {
        PlayerPrefs.SetFloat(SoundVolumeKey, soundVolume);
        PlayerPrefs.Save();
    }

    public void PlaySound(string name)
    {
        SoundClip sound = sounds.Find(s => s.soundName == name);
        if (sound == null)
        {
            Debug.LogWarning($"SoundManager: Звук '{name}' не найден!");
            return;
        }

        if (availableSources.Count > 0)
        {
            AudioSource source = availableSources.Dequeue();
            source.clip = sound.clip;
            source.volume = soundVolume;
            source.Play();
            StartCoroutine(ReturnSourceToPool(source, sound.clip.length));
        }
        else
        {
            Debug.LogWarning("SoundManager: Достигнут лимит активных звуков!");
        }
    }

    private System.Collections.IEnumerator ReturnSourceToPool(AudioSource source, float delay)
    {
        yield return new WaitForSeconds(delay);
        source.Stop();
        availableSources.Enqueue(source);
    }

    public void SetVolume(float volume)
    {
        soundVolume = Mathf.Clamp01(volume);
        SaveVolume();
    }

    public float GetVolume()
    {
        return soundVolume;
    }
}
