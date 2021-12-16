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

    // on awake create singleton instance
    public void Awake()
    {
        Instance = this;
    }

    // Get concrete filed by x and y coordinates.
    private Field getField(int x, int y)
    {
        if (x >= fieldsWidth || y >= fields.Length / fieldsWidth || x < 0 || y < 0)
        {
            return null;
        }
        return fields[(y * fieldsWidth) + x];
    }


    private void Start()
    {
        playerOrderNum = playerGoFirst ? 1 : 2;
        firstMoves = GameObject.FindGameObjectsWithTag("FirstMove");
        UpdatePoints();
        UpdateGameState(GameState.FiguresArrange);
    }

    // Handler for game state update.
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


    // main loop of the game
    // going throught all figures and handling elimination of figures
    private IEnumerator Steps()
    {
        while (figuresOrder.Length > 0)
        {
            foreach (GameObject figure in figuresOrder)
            {
                if (figure)
                {
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
                        curField.GetComponent<Field>().assignedFifure = figure;
                        figure.GetComponent<MoveFigure>().doneMoves = 1000;
                        continue;
                    }

                    figure.GetComponent<MoveFigure>().posX = posX + moveOnX;
                    figure.GetComponent<MoveFigure>().posY = posY + moveOnY;

                    if (newField.GetComponent<Field>())
                    {
                        if (newField.GetComponent<Field>().assignedFifure != null)
                        {
                            // on field already is another figure, so first eliminate that figure and update points
                            var eliminatedFigure = newField.GetComponent<Field>().assignedFifure;
                            int numIndex = Array.IndexOf(figuresOrder, eliminatedFigure);
                            figuresOrder = figuresOrder.Where((val, idx) => idx != numIndex).ToArray();
                            DestroyImmediate(eliminatedFigure);
                            UpdatePoints();
                        }
                        // move figure to new field
                        newField.GetComponent<Field>().assignedFifure = figure;
                        figure.transform.position = newField.transform.position + new Vector3(0, 0.2f, 0);
                    }

                    figure.GetComponent<MoveFigure>().doneMoves++;
                    UpdatePoints();

                    // chcek if skipfight, if false 1 sec wait for every move
                    if (!skipFight)
                    {
                        yield return new WaitForSeconds(1);
                    }
                }

            }
        }

        UpdatePoints();
        // end check who wins the game
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

    // update point of player and enemy according to figures left
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

    // spawn enemy figures accoriding to set position 
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

            FieldToSpawnOn.GetComponent<Field>().assignedFifure = enemyFigure;

            enemyFigure.GetComponent<MoveNumber>().SetMoveNumber(moveNumber);
            moveNumber += 2;

            enemyFigure.SetActive(true);
        }
    }

    // if player has selected figure and click on field this method
    // assign that figure on field, and move on that field
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

    // show green fields where player can assign figure
    public void SelectFirstMoves(GameObject figure)
    {
        UnselectAll();
        selectedFigure = figure;
        figure.GetComponent<PlayerFigure>().ChangeColor(Color.green);
        foreach (GameObject place in firstMoves)
        {
            place.GetComponent<Field>().Green();
        }

    }

    // set all color to default and selectedfigure set to null
    public void UnselectAll()
    {
        if (selectedFigure)
        {
            selectedFigure.GetComponent<PlayerFigure>().ChangeColor(Color.white);
            selectedFigure = null;
        }

        foreach (GameObject fig in _enemyFigures)
        {
            fig.GetComponent<MoveFigure>().UnGreen();
        }

        foreach (GameObject fig in playerFigures)
        {
            if (fig != null)
            {
                fig.GetComponent<MoveFigure>().UnGreen();
            }
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

        SortedDictionary<int, GameObject> sortedTiles = new SortedDictionary<int, GameObject>();
        for (int i = 0; i < figuresOrder.Length; i++)
        {
            if (figuresOrder[i].GetComponent<MoveNumber>().orderNum > 0)
                sortedTiles.Add(figuresOrder[i].GetComponent<MoveNumber>().orderNum, figuresOrder[i]);
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

    // show prediction on hovered figure that is alredy on battleground
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

    // if selecteed figure and hovering over field show prediction
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

    // reset all players figures out of battleground
    public void ResetFigures()
    {
        foreach (GameObject fig in playerFigures)
        {
            UnselectAll();
            HidePredictions();

            fig.GetComponent<PlayerFigure>().ResetPosition();
            curField = getField(fig.GetComponent<MoveFigure>().posX, fig.GetComponent<MoveFigure>().posY);

            if (curField)
            {
                curField.GetComponent<Field>().assignedFifure = null;
            }

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