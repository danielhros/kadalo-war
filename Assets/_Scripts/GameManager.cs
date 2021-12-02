using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static GameManager Instance;

    public GameState State;

    public static event Action<GameState> OnGameStateChanged;

    [SerializeField] private int fieldsWidth;
    [SerializeField] private Field[] fields;
    [SerializeField] private GameObject[] playerFigures;
    [SerializeField] private GameObject[] _enemyFigures;

    [SerializeField] private bool playerGoFirst;

    public void Awake() {
        //Debug.Log(getField(0, 0));
        Instance = this;
    }

    private Field getField(int x, int y) {
        return fields[x * fieldsWidth + y];
    }

    private void Start() {
        SpawnEnemyFigures();
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

    private void SpawnEnemyFigures() {

        int moveNumber = playerGoFirst ? 2 : 1;

        foreach(GameObject enemyFigure in _enemyFigures) {
            int SpawnPositionX = enemyFigure.GetComponent<EnemyFigureSpawnPosition>().SpawnPositionX;
            int SpawnPositionY = enemyFigure.GetComponent<EnemyFigureSpawnPosition>().SpawnPositionY;

            Field FieldToSpawnOn = getField(SpawnPositionX, SpawnPositionY);
            enemyFigure.transform.position = FieldToSpawnOn.transform.position + new Vector3(0, 0.2f, 0);

            enemyFigure.GetComponent<MoveNumber>().SetMoveNumber(moveNumber);
            moveNumber += 2;

            enemyFigure.SetActive(true);
        }
    }

}

public enum GameState {
    FiguresArrange,
    Fight,
    Victory,
    Loose
}