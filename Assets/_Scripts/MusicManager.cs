using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class MusicManager : MonoBehaviour
{
    [SerializeField] private AudioMixer mixer;

    [SerializeField] private AudioClip[] clips;

    [SerializeField] private AudioSource backgroundMusicAudioSource;

    public static MusicManager Instance;

    public void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start() {
        backgroundMusicAudioSource.loop = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(!backgroundMusicAudioSource.isPlaying)
        {
            backgroundMusicAudioSource.clip = GetRandomClip();
            backgroundMusicAudioSource.Play();
        }
    }

    private AudioClip GetRandomClip()
    {
        return clips[Random.Range(0, clips.Length)];
    }

    public void SetVolume(string mixerName, float sliderValue) {
        mixer.SetFloat(mixerName, Mathf.Log10(sliderValue) * 20);
    }


}
