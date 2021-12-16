using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// This class is so called "adapter" used betweet SoundManager singleton instance nad sliders
// for changing volume of music and effects independently. 
public class VolumeSlider : MonoBehaviour
{
    [SerializeField] private Slider _musicSlider, _effectsSlider;
    
    void Start() {
        if (_musicSlider) {
            _musicSlider.value = SoundManager.Instance.MusicSource.volume;
            _musicSlider.onValueChanged.AddListener(val => SoundManager.Instance.ChangeMusicVolume(val));
        }
        if (_effectsSlider) {
            _effectsSlider.value = SoundManager.Instance.EffectSource.volume;
            _effectsSlider.onValueChanged.AddListener(val => SoundManager.Instance.ChangeEffectsVolume(val));
        } 
    }
}
