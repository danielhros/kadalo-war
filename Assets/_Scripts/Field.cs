using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Field : MonoBehaviour
{
    private Color defaultColor;
    public GameObject assignedFifure = null;
    public TextMeshPro predictionText;

    public void Start()
    {
        defaultColor = GetComponent<Renderer>().material.color;
    }

    public void Green()
    {
        GetComponent<Renderer>().material.color = Color.green;
    }

    public void Yellow()
    {
        GetComponent<Renderer>().material.color = Color.yellow;
    }

    public void UnGreen()
    {
        GetComponent<Renderer>().material.color = defaultColor;
    }

    public void OnMouseDown()
    {
        if (GetComponent<Renderer>().material.color == Color.green)
            GameManager.Instance.AssignSelectedFigure(this);
    }

    public void OnMouseEnter()
    {
        GameManager.Instance.ShowPredictionField(this);
    }
}
