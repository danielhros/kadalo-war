using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static GameManager Instance;

    public GameState State;

    public static event Action<GameState> OnGameStateChanged;

    [SerializeField] private int fieldsWidth;
    [SerializeField] private Field[] fields;
    //private List<GameObject> playerFigures = new List<GameObject>();
    [SerializeField] private GameObject[] playerFigures;
    [SerializeField] private GameObject[] _enemyFigures;
    [SerializeField] private bool playerGoFirst;
    [SerializeField] private TextMeshProUGUI enemyPointsText;
    [SerializeField] private TextMeshProUGUI playerPointsText; // tbd not working cannot set from here value

    private int playerOrderNum;
    private GameObject[] firstMoves;
    private GameObject selectedFigure;
    private GameObject[] figuresOrder;

    public void Awake()
    {
        Instance = this;
    }

    private Field getField(int x, int y)
    {
        return fields[x * fieldsWidth + y];
    }

    private void Start()
    {
        playerOrderNum = playerGoFirst ? 1 : 2;
        firstMoves = GameObject.FindGameObjectsWithTag("FirstMove");
        SpawnEnemyFigures();
        UpdateGameState(GameState.FiguresArrange);
    }

    public void UpdateGameState(GameState newState)
    {
        State = newState;

        switch (newState)
        {
            case GameState.FiguresArrange:
                break;
            case GameState.Fight:
                StartFight();
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

    private void StartFight()
    {
        figuresOrder = playerFigures.Concat(_enemyFigures).ToArray();
        SortArray();
        UpdatePoints();  // update ui points
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
                    figure.GetComponent<Pawn>().Move();
                    // nejako vymysliet system pohybu aby bol univerzalny
                    yield return new WaitForSeconds(1);
                }

            }
        }
        // end game according to point call set victory or loose state
    }

    private void UpdatePoints()
    {
        var enemyPoints = 0;
        foreach (GameObject enemyFigure in _enemyFigures)
        {
            enemyPoints += enemyFigure.GetComponent<Points>().points;
        }
        enemyPointsText.SetText(enemyPoints.ToString());

        var playerPoints = 0;
        foreach (GameObject playerFigure in playerFigures)
        {
            playerPoints += playerFigure.GetComponent<Points>().points;
        }
        playerPointsText.SetText(playerPoints.ToString());
    }

    private void SpawnEnemyFigures()
    {

        int moveNumber = playerGoFirst ? 2 : 1;

        foreach (GameObject enemyFigure in _enemyFigures)
        {
            int SpawnPositionX = enemyFigure.GetComponent<EnemyFigureSpawnPosition>().SpawnPositionX;
            int SpawnPositionY = enemyFigure.GetComponent<EnemyFigureSpawnPosition>().SpawnPositionY;

            Field FieldToSpawnOn = getField(SpawnPositionX, SpawnPositionY);
            enemyFigure.transform.position = FieldToSpawnOn.transform.position + new Vector3(0, 0.2f, 0);

            enemyFigure.GetComponent<MoveNumber>().SetMoveNumber(moveNumber);
            moveNumber += 2;

            enemyFigure.SetActive(true);
        }
    }

    public void AssignSelectedFigure(GameObject field)
    {
        if (selectedFigure)
        {
            if (!field.GetComponent<Field>().assignedFifure)
            {
                //playerFigures.Add(selectedFigure);
                field.GetComponent<Field>().assignedFifure = selectedFigure;

                selectedFigure.transform.position = field.transform.position + new Vector3(0, 0.2f, 0);
                selectedFigure.GetComponent<MoveNumber>().SetMoveNumber(playerOrderNum);
                playerOrderNum += 2;
                UnselectAll();
            }
        }
    }

    public void SelectFirstMoves(GameObject figure)
    {
        UnselectAll();
        selectedFigure = figure;
        foreach (GameObject place in firstMoves)
        {
            place.GetComponent<Field>().Green();
        }

    }

    public void UnselectAll()
    {
        if (selectedFigure)
        {
            selectedFigure.GetComponent<PlayerFigure>().UnGreen();
            selectedFigure = null;
        }

        foreach (GameObject place in firstMoves)
        {
            place.GetComponent<Field>().UnGreen();
        }

    }

    private void SortArray()
    {

        SortedDictionary<string, GameObject> sortedTiles = new SortedDictionary<string, GameObject>();
        for (int i = 0; i < figuresOrder.Length; i++)
        {
            sortedTiles.Add(figuresOrder[i].GetComponent<MoveNumber>().orderNum.ToString(), figuresOrder[i]);
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

}

public enum GameState
{
    FiguresArrange,
    Fight,
    Victory,
    Loose
}