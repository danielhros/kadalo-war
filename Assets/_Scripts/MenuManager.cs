using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{

    [SerializeField] private GameObject _menuCanvas;
    [SerializeField]


    private void Awake() {
        GameManager.OnGameStateChanged += GameManagerOnOnGameStateChanged;
    }

    private void OnDestroy() {
        GameManager.OnGameStateChanged -= GameManagerOnOnGameStateChanged;
    }

    private void GameManagerOnOnGameStateChanged(GameState state) {
        _menuCanvas.SetActive(state == GameState.MainMenu);
    }

    public void StartGame() {
        GameManager.Instance.UpdateGameState(GameState.FiguresArrange);
    }

    public void QuitGame() {
        Application.Quit();
    }
}
