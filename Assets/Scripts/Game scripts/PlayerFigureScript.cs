using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFigureScript : MonoBehaviour
{
    public GameObject[] firstMoves;
    public bool selected;

    public void Start()
    {
        firstMoves = GameObject.FindGameObjectsWithTag("FirstMove");
    }

    public void OnMouseDown()
    {
        // change color of places where figure can move
        foreach (GameObject place in firstMoves)
        {
            place.GetComponent<FieldScript>().Green();
        }

        Green();
    }


    public void Green()
    {
        selected = true;
        // change color of current figure
        for (int i = 0; i < gameObject.transform.childCount; i++)
        {
            GameObject child = gameObject.transform.GetChild(i).gameObject;
            child.GetComponent<Renderer>().material.color = Color.green;
        }
    }


    public void UnGreen()
    {
        selected = false;
        // change color of current figure
        for (int i = 0; i < gameObject.transform.childCount; i++)
        {
            GameObject child = gameObject.transform.GetChild(i).gameObject;
            child.GetComponent<Renderer>().material.color = Color.white;
        }
    }
}
