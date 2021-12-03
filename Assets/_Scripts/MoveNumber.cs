using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MoveNumber : MonoBehaviour
{
    public TextMeshPro Text;
    public int orderNum;

    public void SetMoveNumber(int moveNumber)
    {
        Text.SetText(moveNumber.ToString());
        orderNum = moveNumber;
    }
}
