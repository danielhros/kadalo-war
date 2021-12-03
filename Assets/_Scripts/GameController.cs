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
        Debug.Log("Game started");

        figuresOrder = GameObject.FindGameObjectsWithTag("EnemyFigure").Concat(GameObject.FindGameObjectsWithTag("PlayerFigure")).ToArray();
        //GameObject.FindGameObjectsWithTag("StartButton")[0].GetComponentInChildren<Text>().text = "Pause Game";

        SortArray();

        GameManager.Instance.UpdateGameState(GameState.Fight);

        StartCoroutine(Steps());
    }


    private IEnumerator Steps()
    {
        while (figuresOrder.Length > 0)
        {
            foreach (GameObject figure in figuresOrder)
            {
                if (figure)
                {
                    figure.GetComponent<PawnScript>().Move();
                    yield return new WaitForSeconds(1);
                }

            }
        }
        EndGame();
    }

    private void EndGame()
    {
        Debug.Log("all figures are done moving ==== GAME END");

        var playerFiguresLeft = GameObject.FindGameObjectsWithTag("PlayerFigure");
        var enemyFiguresLeft = GameObject.FindGameObjectsWithTag("EnemyFigure");
        var playerPoints = 0;
        var enemyPoints = 0;
        foreach (GameObject figure in playerFiguresLeft)
        {
            //playerPoints += figure.GetComponent<PointsScript>().points;
        }

        foreach (GameObject figure in enemyFiguresLeft)
        {
            //enemyPoints += figure.GetComponent<PointsScript>().points;
        }

        Debug.Log("GAME STATS");
        Debug.Log("Player points: " + playerPoints);
        Debug.Log("Enemy points: " + enemyPoints);

        if (enemyPoints >= playerPoints)
        {
            GameManager.Instance.UpdateGameState(GameState.Loose);
        }
        else
        {
            GameManager.Instance.UpdateGameState(GameState.Victory);
        }
    }

    private void SortArray()
    {

        SortedDictionary<string, GameObject> sortedTiles = new SortedDictionary<string, GameObject>();
        for (int i = 0; i < figuresOrder.Length; i++)
        {
            //sortedTiles.Add(figuresOrder[i].GetComponent<OrderScript>().OrderNum, figuresOrder[i]);
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
        if (index == -1)
            return;

        for (int a = index; a < figuresOrder.Length - 1; a++)
        {
            figuresOrder[a] = figuresOrder[a + 1];
        }
        Array.Resize(ref figuresOrder, figuresOrder.Length - 1);

    }

    public void ResetGame()
    {
        Debug.Log("reset game");
    }
}
