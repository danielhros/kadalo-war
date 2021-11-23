using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldScript : MonoBehaviour
{
    private Color defaultColor;

    public void Start()
    {
        defaultColor = GetComponent<Renderer>().material.color;
    }

    public void OnMouseDown()
    {
        if (GetComponent<Renderer>().material.color == Color.green)
        {
            foreach (GameObject figure in GameObject.FindGameObjectsWithTag("PlayerFigure"))
            {
                if (figure.GetComponent<MoveFigure>().selected)
                {
                    Debug.Log("find figure");
                    figure.GetComponent<MoveFigure>().UnGreen();
                    // tbd fix this currently moving but to wrong place
                    Vector3 v = new Vector3(transform.position.x, transform.position.y, transform.position.z - 2);
                    figure.transform.Translate(v);
                }
            }
        }
    }

    public void Green()
    {
        GetComponent<Renderer>().material.color = Color.green;
    }

    public void UnGreen()
    {
        GetComponent<Renderer>().material.color = defaultColor;
    }
}