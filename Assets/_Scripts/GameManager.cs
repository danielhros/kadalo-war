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
    [SerializeField] private GameObject[] playerFigures;
    [SerializeField] private GameObject[] _enemyFigures;
    [SerializeField] private bool playerGoFirst;
    [SerializeField] private TextMeshProUGUI enemyPointsText;
    [SerializeField] private TextMeshProUGUI playerPointsText;

    [SerializeField] private TextMeshProUGUI WinEnemyOverallPointsText;
    [SerializeField] private TextMeshProUGUI winPlayerOverallPointsText;
    [SerializeField] private TextMeshProUGUI LooseEnemyOverallPointsTex;
    [SerializeField] private TextMeshProUGUI LoosePlayerOverallPointsText;


    private int playerOrderNum;
    private GameObject[] firstMoves;
    private GameObject selectedFigure;
    private GameObject[] figuresOrder;

    private Field newField;
    private Field curField;
    private int playerPoints = 0;
    private int enemyPoints = 0;
    private bool skipFight = false;

    public void Awake()
    {
        Instance = this;
    }

    private Field getField(int y, int x)
    {
        if (y >= fieldsWidth || x >= fields.Length / fieldsWidth || x < 0 || y < 0)
        {
            return null;
        }
        return fields[(x * fieldsWidth) + y];
    }

    private void Start()
    {
        playerOrderNum = playerGoFirst ? 1 : 2;
        firstMoves = GameObject.FindGameObjectsWithTag("FirstMove");
        UpdatePoints();
        UpdateGameState(GameState.FiguresArrange);
    }

    public void UpdateGameState(GameState newState)
    {
        State = newState;

        switch (newState)
        {
            case GameState.FiguresArrange:
                SpawnEnemyFigures();
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
        UnselectAll();
        HidePredictions();
        figuresOrder = playerFigures.Concat(_enemyFigures).ToArray();
        SortArray();
        CheckSetFigures();
        UpdatePoints();
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
                    UpdatePoints();
                    if (figure.GetComponent<MoveFigure>().doneMoves >= figure.GetComponent<MoveFigure>().moves)
                    {
                        int numIndex = Array.IndexOf(figuresOrder, figure);
                        figuresOrder = figuresOrder.Where((val, idx) => idx != numIndex).ToArray();
                        continue;
                    }
                    // check number of moves
                    int posX = figure.GetComponent<MoveFigure>().posX;
                    int posY = figure.GetComponent<MoveFigure>().posY;

                    if (posX == -1 || posY == -1)
                        continue;

                    curField = getField(posX, posY);
                    curField.GetComponent<Field>().assignedFifure = null;

                    int moveOnX = figure.GetComponent<MoveFigure>().moveX;
                    int moveOnY = figure.GetComponent<MoveFigure>().moveY;

                    newField = getField(posX + moveOnX, posY + moveOnY);
                    if (!newField)
                    {
                        figure.GetComponent<MoveFigure>().doneMoves = 1000;
                        continue;
                    }
                    figure.GetComponent<MoveFigure>().posX = posX + moveOnX;
                    figure.GetComponent<MoveFigure>().posY = posY + moveOnY;
                    if (newField.GetComponent<Field>())
                    {
                        if (newField.GetComponent<Field>().assignedFifure == null)
                        {
                            newField.GetComponent<Field>().assignedFifure = figure;
                            figure.transform.position = newField.transform.position + new Vector3(0, 0.2f, 0);

                        }
                        else
                        {
                            var eliminatedFigure = newField.GetComponent<Field>().assignedFifure;
                            int numIndex = Array.IndexOf(figuresOrder, eliminatedFigure);
                            figuresOrder = figuresOrder.Where((val, idx) => idx != numIndex).ToArray();
                            DestroyImmediate(eliminatedFigure);
                            UpdatePoints();
                        }
                    }
                    figure.transform.position = newField.transform.position + new Vector3(0, 0.2f, 0);
                    figure.GetComponent<MoveFigure>().doneMoves++;
                    UpdatePoints();
                    if (!skipFight)
                    {
                        yield return new WaitForSeconds(1);
                    }
                }

            }
        }
        UpdatePoints();
        if (playerPoints > enemyPoints)
        {
            UpdateGameState(GameState.Victory);
            WinEnemyOverallPointsText.SetText(enemyPoints.ToString());
            winPlayerOverallPointsText.SetText(playerPoints.ToString());
        }
        else
        {
            UpdateGameState(GameState.Loose);
            LooseEnemyOverallPointsTex.SetText(enemyPoints.ToString());
            LoosePlayerOverallPointsText.SetText(playerPoints.ToString());
        }
    }

    private void UpdatePoints()
    {
        playerPoints = 0;
        enemyPoints = 0;
        foreach (GameObject enemyFigure in _enemyFigures)
        {
            if (enemyFigure)
            {
                enemyPoints += enemyFigure.GetComponent<Points>().points;
            }
        }
        enemyPointsText.SetText(enemyPoints.ToString());


        foreach (GameObject playerFigure in playerFigures)
        {
            if (playerFigure)
            {
                playerPoints += playerFigure.GetComponent<Points>().points;
            }
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

            enemyFigure.GetComponent<MoveFigure>().posX = SpawnPositionX;
            enemyFigure.GetComponent<MoveFigure>().posY = SpawnPositionY;

            Field FieldToSpawnOn = getField(SpawnPositionX, SpawnPositionY);
            enemyFigure.transform.position = FieldToSpawnOn.transform.position + new Vector3(0, 0.2f, 0);

            enemyFigure.GetComponent<MoveNumber>().SetMoveNumber(moveNumber);
            moveNumber += 2;

            enemyFigure.SetActive(true);
        }
    }

    public void AssignSelectedFigure(Field field)
    {
        if (selectedFigure)
        {
            if (!field.GetComponent<Field>().assignedFifure)
            {
                field.GetComponent<Field>().assignedFifure = selectedFigure;
                int ind = Array.IndexOf(fields, field);
                int x = ind % fieldsWidth;
                int y = ind / fieldsWidth;

                selectedFigure.GetComponent<MoveFigure>().posX = x;
                selectedFigure.GetComponent<MoveFigure>().posY = y;
                selectedFigure.GetComponent<MoveFigure>().arranged = true;
                curField = getField(x, y);

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
        figure.GetComponent<PlayerFigure>().Green();
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

        foreach (GameObject fig in _enemyFigures)
        {
            fig.GetComponent<MoveFigure>().UnGreen();
        }

        foreach (GameObject fig in playerFigures)
        {
            fig.GetComponent<MoveFigure>().UnGreen();
        }

        foreach (Field place in fields)
        {
            place.GetComponent<Field>().UnGreen();
        }

    }

    public void HidePredictions()
    {
        foreach (Field place in fields)
        {
            place.GetComponent<Field>().predictionText.text = "";
        }
    }

    public void CheckSetFigures()
    {
        foreach (GameObject fig in playerFigures)
        {
            if (fig.GetComponent<MoveFigure>().arranged == false)
            {
                DestroyImmediate(fig);
            }
        }
    }

    private void SortArray()
    {

        SortedDictionary<string, GameObject> sortedTiles = new SortedDictionary<string, GameObject>();
        for (int i = 0; i < figuresOrder.Length; i++)
        {
            if (figuresOrder[i].GetComponent<MoveNumber>().orderNum > 0)
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


    public void ShowPrediction(GameObject figure)
    {
        if (selectedFigure)
        {
            return;
        }
        if (State == GameState.FiguresArrange)
        {
            UnselectAll();
            HidePredictions();
            figure.GetComponent<MoveFigure>().Green();
            int posX = figure.GetComponent<MoveFigure>().posX;
            int posY = figure.GetComponent<MoveFigure>().posY;
            int moveOnX = figure.GetComponent<MoveFigure>().moveX;
            int moveOnY = figure.GetComponent<MoveFigure>().moveY;
            int moves = figure.GetComponent<MoveFigure>().moves;

            Field curField = getField(posX, posY);
            if (curField)
                curField.GetComponent<Field>().Green();
            for (int i = 1; i <= moves; i++)
            {
                Field newField = getField(posX + moveOnX, posY + moveOnY);
                posX += moveOnX;
                posY += moveOnY;
                if (newField)
                {
                    newField.GetComponent<Field>().Green();
                    newField.GetComponent<Field>().predictionText.text = i.ToString();
                }
            }
        }
    }

    public void ShowPredictionField(Field field)
    {
        if (State == GameState.FiguresArrange && selectedFigure)
        {
            var fig = selectedFigure;
            UnselectAll();
            HidePredictions();
            SelectFirstMoves(fig);
            field.GetComponent<Field>().Green();

            int moves = selectedFigure.GetComponent<MoveFigure>().moves;
            int moveOnX = selectedFigure.GetComponent<MoveFigure>().moveX;
            int moveOnY = selectedFigure.GetComponent<MoveFigure>().moveY;
            int ind = 0;
            foreach (Field place in fields)
            {
                if (field == place)
                {
                    break;
                }
                ind++;
            }
            int posX = ind % fieldsWidth;
            int posY = ind / fieldsWidth;

            for (int i = 1; i <= moves; i++)
            {
                Field newField = getField(posX + moveOnX, posY + moveOnY);
                posX += moveOnX;
                posY += moveOnY;
                if (newField)
                {
                    newField.GetComponent<Field>().Green();
                    newField.GetComponent<Field>().predictionText.text = i.ToString();
                }
            }
        }
    }

    public void ResetFigures()
    {
        foreach (GameObject fig in playerFigures)
        {
            UnselectAll();
            HidePredictions();
            fig.GetComponent<PlayerFigure>().ResetPosition();
            curField = getField(fig.GetComponent<MoveFigure>().posX, fig.GetComponent<MoveFigure>().posY);
            if (curField)
                curField.GetComponent<Field>().assignedFifure = null;
            fig.GetComponent<MoveFigure>().posX = -1;
            fig.GetComponent<MoveFigure>().posY = -1;
            fig.GetComponent<MoveFigure>().arranged = false;
            fig.GetComponent<MoveNumber>().ResetMoveNumber();
        }
        playerOrderNum = playerGoFirst ? 1 : 2;
    }

    public void SkipFight()
    {
        skipFight = true;
    }

}

public enum GameState
{
    FiguresArrange,
    Fight,
    Victory,
    Loose
}