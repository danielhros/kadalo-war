using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetFigureButton : MonoBehaviour
{
    public void Reset()
    {
        GameManager.Instance.ResetFigures();
    }
}
