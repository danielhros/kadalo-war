using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CancelSelect : MonoBehaviour
{
    private GameObject[] firstMoves;
    private GameObject[] playerFigures;

    public void Start()
    {
        firstMoves = GameObject.FindGameObjectsWithTag("FirstMove");

        playerFigures = GameObject.FindGameObjectsWithTag("PlayerFigure");
    }


    public void OnMouseDown()
    {
        // change color of places where figure can move
        foreach (GameObject place in firstMoves)
        {
            place.GetComponent<FieldScript>().UnGreen();
        }

        // change color of figures
        foreach (GameObject figure in playerFigures)
        {
            figure.GetComponent<MoveFigure>().UnGreen();
        }

    }
}
