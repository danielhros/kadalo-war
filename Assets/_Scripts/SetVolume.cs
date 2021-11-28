using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SetVolume : MonoBehaviour {
    
    [SerializeField] private string mixerName;

    public void SetLevel(float sliderValue) {
        MusicManager.Instance.SetVolume(mixerName, sliderValue);
    }
}
