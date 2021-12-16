using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

// this class handles order numbers of figures
// after set is is displayed on UI
// in resetMoveNumber I remover order number and set text to X
public class MoveNumber : MonoBehaviour
{
    public TextMeshPro Text;
    public int orderNum;

    public void SetMoveNumber(int moveNumber)
    {
        Text.SetText(moveNumber.ToString());
        orderNum = moveNumber;
    }

    public void ResetMoveNumber()
    {
        Text.SetText("X".ToString());
        orderNum = 0;
    }
}
