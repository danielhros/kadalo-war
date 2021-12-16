using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// rotates gameObject
public class Rotator : MonoBehaviour
{
    [SerializeField] private Vector3 _roatation;

    void Update() {
        transform.Rotate(_roatation * Time.deltaTime);
    }
}
