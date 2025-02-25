using UnityEngine;
using UnityEngine.UI;

public class MusicVolumeSlider : MonoBehaviour
{
    public Slider volumeSlider;

    private void Start()
    {
        if (volumeSlider == null)
        {
            Debug.LogError("MusicVolumeSlider: Slider не назначен!");
            return;
        }

        volumeSlider.value = BackgroundMusicManager.Instance.GetVolume();
        volumeSlider.onValueChanged.AddListener(BackgroundMusicManager.Instance.SetVolume);
    }

    private void OnDestroy()
    {
        volumeSlider.onValueChanged.RemoveListener(BackgroundMusicManager.Instance.SetVolume);
    }
}
