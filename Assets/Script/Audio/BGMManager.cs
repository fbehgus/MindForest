using UnityEngine;
using UnityEngine.UI;

public class BGMManager : MonoBehaviour {
    public AudioClip bgmClip;
    public AudioSource audioSource;
    public Slider volumeSlider;


    void Start() {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = bgmClip;
        audioSource.loop = true;
        audioSource.playOnAwake = true;
        audioSource.volume = 0.1f;
        volumeSlider.value = audioSource.volume;
        volumeSlider.onValueChanged.AddListener(SetVolume);
        audioSource.Play();

    }

    public void ToggleMute() {
        audioSource.mute = !audioSource.mute;
    }

    public void SetVolume(float volume) {
        audioSource.volume = Mathf.Clamp01(volume);
    }
}
