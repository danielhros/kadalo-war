using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReadyButton : MonoBehaviour
{
    public void StartFight()
    {
        GameManager.Instance.UpdateGameState(GameState.Fight);
    }
}