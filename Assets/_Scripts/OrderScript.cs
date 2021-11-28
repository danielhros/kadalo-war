using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class OrderScript : MonoBehaviour
{
    public TextMeshPro Text;
    public string OrderNum;
    public int NextOrderNum = 1;

    public void Start()
    {
        Text.SetText(OrderNum);
    }

    public void SelectedForBattle()
    {
        // TBD if already was selected for battle change numbers
        OrderNum = NextOrderNum.ToString();
        Text.SetText(OrderNum);

        foreach (GameObject figure in GameObject.FindGameObjectsWithTag("PlayerFigure"))
        {
            figure.GetComponent<OrderScript>().NextOrderNum += 2;
        }
    }

}
