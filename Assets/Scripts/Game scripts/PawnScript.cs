using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PawnScript : MonoBehaviour
{
    public void Move()
    {
        transform.position = transform.position + (transform.forward) * 2;

        var ray = new Ray(transform.position, transform.up * -1.0f);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            hit.transform.gameObject.GetComponent<FieldScript>().Green();
            hit.transform.gameObject.GetComponent<FieldScript>().AsignFigure(transform.gameObject);
        }
    }
}
