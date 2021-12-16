using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFigure : MonoBehaviour
{
    private Vector3 startPos;

    public void Start()
    {
        startPos = transform.position;
    }

    public void OnMouseDown()
    {
        if (transform.gameObject.GetComponent<MoveNumber>().orderNum == 0)
        {
            ChangeColor(Color.green);
            GameManager.Instance.SelectFirstMoves(transform.gameObject);
        }
    }


    public void ChangeColor(Color color)
    {
        // change color of current figure
        for (int i = 0; i < gameObject.transform.childCount; i++)
        {
            GameObject child = gameObject.transform.GetChild(i).gameObject;
            child.GetComponent<Renderer>().material.color = color;
        }
    }


    //public void UnGreen()
    //{
    //    // change color of current figure
    //    for (int i = 0; i < gameObject.transform.childCount; i++)
    //    {
    //        GameObject child = gameObject.transform.GetChild(i).gameObject;
    //        child.GetComponent<Renderer>().material.color = Color.white;
    //    }
    //}

    // set position on starting table
    public void ResetPosition()
    {
        transform.position = startPos;
    }
}
