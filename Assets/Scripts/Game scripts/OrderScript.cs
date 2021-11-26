using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class OrderScript : MonoBehaviour
{
    public TextMeshPro Text;
    public string OrderNum;

    public void Start()
    {
        Text.SetText(OrderNum);
    }

    public void SelectedForBattle()
    {
        OrderNum = "1";
        Text.SetText(OrderNum);
    }

}
