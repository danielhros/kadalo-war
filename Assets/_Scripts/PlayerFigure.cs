using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFigure : MonoBehaviour
{

    public void OnMouseDown()
    {
        if (transform.gameObject.GetComponent<MoveNumber>().orderNum == 0)
        {
            Green();
            GameManager.Instance.SelectFirstMoves(transform.gameObject);
        }
    }


    public void Green()
    {
        // change color of current figure
        for (int i = 0; i < gameObject.transform.childCount; i++)
        {
            GameObject child = gameObject.transform.GetChild(i).gameObject;
            child.GetComponent<Renderer>().material.color = Color.green;
        }
    }


    public void UnGreen()
    {
        // change color of current figure
        for (int i = 0; i < gameObject.transform.childCount; i++)
        {
            GameObject child = gameObject.transform.GetChild(i).gameObject;
            child.GetComponent<Renderer>().material.color = Color.white;
        }
    }
}
