using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// on button click call updateGameState with argument fight
// this starts fight and ends arranging figures
public class ReadyButton : MonoBehaviour
{
    public void StartFight()
    {
        GameManager.Instance.UpdateGameState(GameState.Fight);
    }
}