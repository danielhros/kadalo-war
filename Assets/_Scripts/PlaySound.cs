 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySound : MonoBehaviour
{
    [SerializeField] private AudioClip _clip;

    // Play sound once this method is called.
    // It issues PlaySound method of singleton SoundManager instance
    public void PlaySoundOnce() {
        SoundManager.Instance.PlaySound(_clip);
    }

}
