using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CancelSelect : MonoBehaviour
{
    // Call gamemener methods for unselect all figures/fields (set default color)
    // and hide predictions (hide numbers on fields).
    public void OnMouseDown()
    {
        GameManager.Instance.UnselectAll();
        GameManager.Instance.HidePredictions();
    }
}
