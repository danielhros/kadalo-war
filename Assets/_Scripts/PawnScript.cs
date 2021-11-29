using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PawnScript : MonoBehaviour
{
    public int NumberOfMoves;
    private int MovesDone;

    public void Move()
    {
        if (MovesDone >= NumberOfMoves)
        {
            GameObject.FindGameObjectsWithTag("Battlefield")[0].GetComponent<GameController>().RemoveFromArray(transform.gameObject);
            return;
        }
        var ray = new Ray(transform.position, transform.up * -1.0f);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            hit.transform.gameObject.GetComponent<FieldScript>().UnAsignFigure();
        }

        // order script

        GetComponent<Rigidbody>().isKinematic = true;
        transform.position = transform.position + (transform.forward) * 2;
        MovesDone++;
        GetComponent<Rigidbody>().isKinematic = true;

        var ray2 = new Ray(transform.position, transform.up * -1.0f);
        RaycastHit hit2;
        if (Physics.Raycast(ray2, out hit2))
        {
            if (hit2.transform.gameObject.GetComponent<FieldScript>())
                hit2.transform.gameObject.GetComponent<FieldScript>().AsignFigure(transform.gameObject);
            else
            {
                // stop moving this figure, it is out of the board
                MovesDone = NumberOfMoves;
            }
        }
    }

    void OnMouseEnter()
    {

        //If your mouse hovers over the GameObject with the script attached, output this message
        Debug.Log("Mouse is over GameObject.");
    }

    void OnMouseExit()
    {
        //The mouse is no longer hovering over the GameObject so output this message each frame
        Debug.Log("Mouse is no longer on GameObject.");
    }
}
