using UnityEngine;
using UnityEngine.UI;

public class SoundVolumeSlider : MonoBehaviour
{
    public Slider volumeSlider;

    private void Start()
    {
        if (volumeSlider == null)
        {
            Debug.LogError("SoundVolumeSlider: Slider не назначен!");
            return;
        }

        volumeSlider.value = SoundManager.Instance.GetVolume();
        volumeSlider.onValueChanged.AddListener(SoundManager.Instance.SetVolume);
    }

    private void OnDestroy()
    {
        volumeSlider.onValueChanged.RemoveListener(SoundManager.Instance.SetVolume);
    }
}
