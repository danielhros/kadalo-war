using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour {

    public static SoundManager Instance;

    //[SerializeField] private AudioClip[] clips;

    [SerializeField] private AudioSource _effectsSource;
    [SerializeField] private AudioSource _musicSource;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlaySound(AudioClip clip) {
        Debug.Log("I am here");
        _effectsSource.PlayOneShot(clip);
    }

    public void ChangeEffectsVolume(float value) {
        _effectsSource.volume = value;
    }

    public void ChangeMusicVolume(float value) {
        _musicSource.volume = value;
    }

    //// Start is called before the first frame update
    //void Start()
    //{
    //    backgroundMusicAudioSource.loop = false;
    //}

    //// Update is called once per frame
    //void Update()
    //{
    //    if (!backgroundMusicAudioSource.isPlaying)
    //    {
    //        backgroundMusicAudioSource.clip = GetRandomClip();
    //        backgroundMusicAudioSource.Play();
    //    }
    //}

    //private AudioClip GetRandomClip()
    //{
    //    return clips[Random.Range(0, clips.Length)];
    //}

    //public void SetVolume(string mixerName, float sliderValue)
    //{
    //    mixer.SetFloat(mixerName, Mathf.Log10(sliderValue) * 20);
    //}


}
