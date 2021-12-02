using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MoveNumber : MonoBehaviour
{
    public TextMeshPro Text;

    public void SetMoveNumber(int moveNumber) {
        Text.SetText(moveNumber.ToString());
    }
}
