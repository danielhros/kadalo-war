using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldScript : MonoBehaviour
{
    private Color defaultColor;
    private GameObject assignedFifure;

    public void Start()
    {
        defaultColor = GetComponent<Renderer>().material.color;
    }

    // check if this component is green, if so move selected figure on this field
    public void OnMouseDown()
    {
        if (GetComponent<Renderer>().material.color == Color.green)
        {
            foreach (GameObject figure in GameObject.FindGameObjectsWithTag("PlayerFigure"))
            {
                if (figure.GetComponent<PlayerFigureScript>().selected)
                {
                    AsignFigure(figure);
                    figure.GetComponent<OrderScript>().SelectedForBattle();
                    figure.GetComponent<PlayerFigureScript>().UnGreen();
                    figure.transform.position = transform.position + new Vector3(0, 1, 0);
                    // change color of places where figure can move
                    foreach (GameObject place in GameObject.FindGameObjectsWithTag("FirstMove"))
                    {
                        place.GetComponent<FieldScript>().UnGreen();
                    }
                }
            }
        }
    }

    public void AsignFigure(GameObject figure)
    {
        if (assignedFifure)
        {
            Debug.Log("vyhadzovanie!!!!");
            GameObject.FindGameObjectsWithTag("Battlefield")[0].GetComponent<GameController>().RemoveFromArray(assignedFifure);
        }
        assignedFifure = figure;
    }

    public void UnAsignFigure()
    {
        assignedFifure = null;
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