using UnityEngine;

public class BackgroundMusicManager : MonoBehaviour
{
    private const string MusicVolumeKey = "MusicVolume";
    public static BackgroundMusicManager Instance { get; private set; }

    public AudioSource audioSource;
    private float defaultVolume = 0.5f;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            Instance = this;
            InitializeMusic();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void InitializeMusic()
    {
        if (audioSource == null)
        {
            Debug.LogError("BackgroundMusicManager: AudioSource не назначен!");
            return;
        }

        float savedVolume = PlayerPrefs.GetFloat(MusicVolumeKey, defaultVolume);
        SetVolume(savedVolume);
    }

    public void SetVolume(float volume)
    {
        audioSource.volume = Mathf.Clamp01(volume);
        PlayerPrefs.SetFloat(MusicVolumeKey, volume);
        PlayerPrefs.Save();
    }

    public float GetVolume()
    {
        return PlayerPrefs.GetFloat(MusicVolumeKey, defaultVolume);
    }
}
