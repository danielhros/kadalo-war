using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// this class handles moving of figures
// in moveX and moveY atributes are settings of movig for figure
// posX and posY are current position of figure on the field
public class MoveFigure : MonoBehaviour
{
    // starting from left top corner
    // if x = 1 meaning from players camera right
    // if x = -1 meaning from players camera left
    // if y = 1 meaning from players camera to player
    // if y = -1 meaning from players camera from player
    public int moveX;
    public int moveY;

    public int posX;
    public int posY;

    public int moves;
    public int doneMoves;

    public bool arranged;

    public void Awake()
    {
        arranged = false;
    }

    public void OnMouseEnter()
    {
        GameManager.Instance.ShowPrediction(transform.gameObject);
    }

    // set color of figure on green
    public void Green()
    {
        for (int i = 0; i < gameObject.transform.childCount; i++)
        {
            GameObject child = gameObject.transform.GetChild(i).gameObject;
            child.GetComponent<Renderer>().material.color = Color.green;
        }
    }

    // set color of figure on default
    public void UnGreen()
    {
        for (int i = 0; i < gameObject.transform.childCount; i++)
        {
            GameObject child = gameObject.transform.GetChild(i).gameObject;
            child.GetComponent<Renderer>().material.color = Color.white;
        }
    }
}
