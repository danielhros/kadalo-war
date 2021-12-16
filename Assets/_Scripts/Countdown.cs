using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

// This class handles timer for arranging figues, when there is no time left, timer emits method in
// GameManager which changes game state to GameState.Fight.
public class Countdown : MonoBehaviour
{
    [SerializeField] private int numberOfSeconds;
    [SerializeField] private TextMeshProUGUI text;
    private float timer;
    private int seconds;

    void Update()
    {
        timer += Time.deltaTime;
        seconds = (int)(timer % 60);
        if (numberOfSeconds - seconds < 0)
        {
            GameManager.Instance.UpdateGameState(GameState.Fight);
        }
        text.SetText((numberOfSeconds - seconds).ToString() + "s");
    }
}
