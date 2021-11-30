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

    private void Start() {
        UpdateGameState(GameState.FiguresArrange);
    }

    public void UpdateGameState(GameState newState) {
        State = newState;

        switch (newState) {
            case GameState.FiguresArrange:
                break;
            case GameState.Fight:
                break;
            case GameState.Victory:
                break;
            case GameState.Loose:
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(newState), newState, null);
        }

        OnGameStateChanged?.Invoke(newState);

    }

}

public enum GameState {
    FiguresArrange,
    Fight,
    Victory,
    Loose
}