using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;
using System;

public class GameController : MonoBehaviour
{
    public GameObject[] figuresOrder;

    public void StartGame()
    {
        Debug.Log("game was started");

        figuresOrder = GameObject.FindGameObjectsWithTag("EnemyFigure").Concat(GameObject.FindGameObjectsWithTag("PlayerFigure")).ToArray();
        GameObject.FindGameObjectsWithTag("StartButton")[0].GetComponentInChildren<Text>().text = "Pause Game";

        SortArray();
        StartCoroutine(Steps());
    }


    private IEnumerator Steps()
    {
        while (true)
        {
            foreach (GameObject figure in figuresOrder)
            {
                figure.GetComponent<PawnScript>().Move();
                yield return new WaitForSeconds(1);

            }
        }
    }

    private void SortArray()
    {

        SortedDictionary<string, GameObject> sortedTiles = new SortedDictionary<string, GameObject>();
        for (int i = 0; i < figuresOrder.Length; i++)
        {
            sortedTiles.Add(figuresOrder[i].GetComponent<OrderScript>().OrderNum, figuresOrder[i]);
        }

        figuresOrder = sortedTiles.Values.ToArray();
    }

    public void RemoveFromArray(GameObject figure)
    {
        var index = -1;
        for (int i = 0; i < figuresOrder.Length; i++)
        {
            if (figuresOrder[i] == figure) index = i;
        }

        for (int a = index; a < figuresOrder.Length - 1; a++)
        {
            figuresOrder[a] = figuresOrder[a + 1];
        }
        Array.Resize(ref figuresOrder, figuresOrder.Length - 1);

    }
}
