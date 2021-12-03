using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Countdown : MonoBehaviour
{
    [SerializeField] private int numberOfSeconds;
    [SerializeField] private TextMeshProUGUI text;
    private float timer = 0.0f;
    private int seconds = 0;

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
            seconds = 0;
        }
    }

    void Update()
    {
        timer += Time.deltaTime;
        seconds = (int)(timer % 60);
        if (numberOfSeconds - seconds < 0)
        {
            GameManager.Instance.UpdateGameState(GameState.Fight);
        }
        text.SetText((numberOfSeconds - seconds).ToString());
    }
}
