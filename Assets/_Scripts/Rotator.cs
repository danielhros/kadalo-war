using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    [SerializeField] private Vector3 _roatation;

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(_roatation * Time.deltaTime);
    }
}
