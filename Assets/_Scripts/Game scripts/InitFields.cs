using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitFields : MonoBehaviour
{
    public void Start()
    {
        // set all fileds scripts
        for (int i = 0; i < gameObject.transform.childCount; i++)
        {
            GameObject child = gameObject.transform.GetChild(i).gameObject;
            child.AddComponent<FieldScript>();
        }
    }
}
