using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// call method resetFigures in gamemanager
public class ResetFigureButton : MonoBehaviour
{
    public void Reset()
    {
        GameManager.Instance.ResetFigures();
    }
}
