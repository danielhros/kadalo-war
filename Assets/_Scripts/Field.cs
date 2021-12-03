using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Field : MonoBehaviour
{
    private Color defaultColor;
    public GameObject assignedFifure;

    public void Start()
    {
        defaultColor = GetComponent<Renderer>().material.color;
    }

    public void Green()
    {
        GetComponent<Renderer>().material.color = Color.green;
    }

    public void UnGreen()
    {
        GetComponent<Renderer>().material.color = defaultColor;
    }

    public void OnMouseDown()
    {
        GameManager.Instance.AssignSelectedFigure(transform.gameObject);
    }
}
