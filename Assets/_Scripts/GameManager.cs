using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static GameManager Instance;

    public GameState State;

    public static event Action<GameState> OnGameStateChanged;

    [SerializeField] private int fieldsWidth;
    [SerializeField] private int fieldsHeight;
    [SerializeField] private GameObject fields;


    public void Awake() {
        Instance = this;
    }

    private void Start()
    {
        UpdateGameState(GameState.MainMenu);
    }

    public void UpdateGameState(GameState newState) {
        State = newState;

        switch (newState) {
            case GameState.MainMenu:
                break;
            case GameState.FiguresArrange:
                HandleFiguresArrange();
                break;
            case GameState.Fight:
                break;
            case GameState.Victory:
                break;
            case GameState.Loose:
                break;
            case GameState.PauseMenu:
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(newState), newState, null);
        }

        OnGameStateChanged?.Invoke(newState);

    }

    private void HandleFiguresArrange() {

    }

}



public enum GameState {
    MainMenu,
    PauseMenu,
    FiguresArrange,
    Fight,
    Victory,
    Loose
}