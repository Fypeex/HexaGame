using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    [field: SerializeField] public Slider MusicVolumeSlider { get; private set; }
    [field: SerializeField] public Slider SoundVolumeSlider { get; private set; }
    [field: SerializeField] public TextMeshProUGUI VersionText { get; private set; }

    private void Start()
    {
        VersionText.text = $"Version: {Application.version}";
        MusicVolumeSlider.value = PlayerPrefs.GetFloat("MusicVolume", 0.5f);
        SoundVolumeSlider.value = PlayerPrefs.GetFloat("SoundVolume", 0.5f);
    }

    public void SetMusicVolume()
    {
        Debug.Log(MusicVolumeSlider.value);
        PlayerPrefs.SetFloat("MusicVolume", MusicVolumeSlider.value);
    }

    public void SetSoundVolume()
    {
        Debug.Log(SoundVolumeSlider.value);
        PlayerPrefs.SetFloat("SoundVolume", SoundVolumeSlider.value);
    }
}