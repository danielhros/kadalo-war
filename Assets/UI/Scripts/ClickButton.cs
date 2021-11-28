using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ClickButton : MonoBehaviour, IPointerDownHandler
{

    [SerializeField] private AudioClip _audio;
    [SerializeField] private AudioSource _source;

    public void OnPointerDown(PointerEventData eventData)
    {
        _source.PlayOneShot(_audio);
    }
}
