using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Countdown : MonoBehaviour
{
    [SerializeField] private int numberOfSeconds;

    private void Awake()
    {
        GameManager.OnGameStateChanged += GameManagerOnOnGameStateChanged;
    }

    private void OnDestroy()
    {
        GameManager.OnGameStateChanged -= GameManagerOnOnGameStateChanged;
    }

    private void GameManagerOnOnGameStateChanged(GameState state)
    {
        if (state == GameState.FiguresArrange)
        {
            timerStart();
        }
    }

    private void timerStart()
    {
        // timer end
        // start
        //GameManager.Instance.UpdateGameState(GameState.Fight);
    }
}
