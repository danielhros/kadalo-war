using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeSlider : MonoBehaviour
{
    [SerializeField] private Slider _musicSlider, _effectsSlider;
    // Start is called before the first frame update
    void Start() {
        if (_musicSlider) {
            SoundManager.Instance.ChangeMusicVolume(_musicSlider.value);
            _musicSlider.onValueChanged.AddListener(val => SoundManager.Instance.ChangeMusicVolume(val));
        }
        if (_effectsSlider) {
            SoundManager.Instance.ChangeEffectsVolume(_effectsSlider.value);
            _effectsSlider.onValueChanged.AddListener(val => SoundManager.Instance.ChangeEffectsVolume(val));
        } 
    }
}
