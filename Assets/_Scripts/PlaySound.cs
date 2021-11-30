 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySound : MonoBehaviour
{
    [SerializeField] private AudioClip _clip;

    // Start is called before the first frame update
    public void PlaySoundOnce() {
        Debug.Log("Play sound");
        SoundManager.Instance.PlaySound(_clip);
    }

}
