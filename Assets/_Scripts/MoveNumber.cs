using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

// This class handles order numbers of figures and their order number canvas.
public class MoveNumber : MonoBehaviour
{
    public TextMeshPro Text;
    public int orderNum;

    public void SetMoveNumber(int moveNumber)
    {
        Text.SetText(moveNumber.ToString());
        orderNum = moveNumber;
    }

    // method removes order number and sets text to value of X
    public void ResetMoveNumber()
    {
        Text.SetText("X".ToString());
        orderNum = 0;
    }
}
