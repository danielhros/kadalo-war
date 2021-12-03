using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CancelSelect : MonoBehaviour
{
    public void OnMouseDown()
    {
        GameManager.Instance.UnselectAll();
        GameManager.Instance.HidePredictions();
    }
}
