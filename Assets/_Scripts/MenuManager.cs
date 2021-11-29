using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{

    [SerializeField] private GameObject _menuCanvas;
    [SerializeField] private GameObject _figuresArrangeCanvas;


    private void Awake() {
        GameManager.OnGameStateChanged += GameManagerOnOnGameStateChanged;
        Debug.Log(_menuCanvas);
    }

    private void OnDestroy() {
        GameManager.OnGameStateChanged -= GameManagerOnOnGameStateChanged;
    }

    private void GameManagerOnOnGameStateChanged(GameState state) {
        _menuCanvas.SetActive(state == GameState.MainMenu);
        _figuresArrangeCanvas.SetActive(state == GameState.FiguresArrange);

    }

    public void StartLevel1() {
        GameManager.Instance.UpdateGameState(GameState.FiguresArrange);
    }

    public void QuitGame() {
        Application.Quit();
    }
}
