using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour {

    public static SoundManager Instance;

    [SerializeField] private AudioClip[] backgroundMusicClips;
    [SerializeField] private AudioSource _effectsSource;
    [SerializeField] private AudioSource _musicSource;

    public AudioSource EffectSource {
        get { return _effectsSource; }
    }

    public AudioSource MusicSource {
        get { return _musicSource; }
    }


    void Awake()
    {
        if (Instance == null)
        {
            // set volume according that
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlaySound(AudioClip clip) {
        _effectsSource.PlayOneShot(clip);
    }

    public void ChangeEffectsVolume(float value) {
        _effectsSource.volume = value;
    }

    public void ChangeMusicVolume(float value) {
        _musicSource.volume = value;
    }

    // Start is called before the first frame update
    void Start() {
        _musicSource.loop = false;
    }

    //// Update is called once per frame
    void Update() {
        if (!_musicSource.isPlaying) {
            _musicSource.clip = GetRandomClip();
            _musicSource.Play();
        }
    }

    private AudioClip GetRandomClip() {
        return backgroundMusicClips[Random.Range(0, backgroundMusicClips.Length)];
    }
}
