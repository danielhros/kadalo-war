using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

// This class is handling chage of field color, assigning figure on click and
// showing predictions when arranging figures.
public class Field : MonoBehaviour
{
    private Color defaultColor;
    public GameObject assignedFifure = null;
    public TextMeshPro predictionText;

    public void Start()
    {
        defaultColor = GetComponent<Renderer>().material.color;
    }

    public void Green()
    {
        GetComponent<Renderer>().material.color = Color.green;
    }

    public void UnGreen()
    {
        GetComponent<Renderer>().material.color = defaultColor;
    }

    // if field is clicked and has tag first move (which means is available for assignening figure)
    // call method of gamemanager and assign figure to this field
    public void OnMouseDown()
    {
        if (gameObject.tag == "FirstMove")
        {
            GameManager.Instance.AssignSelectedFigure(this);
        }
    }

    // possibly show predictions if game state is arrage figure and figure is selected
    public void OnMouseEnter()
    {
        GameManager.Instance.ShowPredictionField(this);
    }
}
