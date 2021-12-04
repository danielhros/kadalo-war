using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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


    public void OnMouseEnter()
    {
        GameManager.Instance.ShowPrediction(transform.gameObject);
    }


    public void Green()
    {
        for (int i = 0; i < gameObject.transform.childCount; i++)
        {
            GameObject child = gameObject.transform.GetChild(i).gameObject;
            child.GetComponent<Renderer>().material.color = Color.green;
        }
    }


    public void UnGreen()
    {
        for (int i = 0; i < gameObject.transform.childCount; i++)
        {
            GameObject child = gameObject.transform.GetChild(i).gameObject;
            child.GetComponent<Renderer>().material.color = Color.white;
        }
    }
}
