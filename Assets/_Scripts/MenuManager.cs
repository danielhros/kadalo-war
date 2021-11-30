using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{

    [SerializeField] private GameObject _arrangeFiguresCanvas;
    [SerializeField] private GameObject _fightCanvas;

    private void Awake() {
        GameManager.OnGameStateChanged += GameManagerOnOnGameStateChanged;
    }

    private void OnDestroy() {
        GameManager.OnGameStateChanged -= GameManagerOnOnGameStateChanged;
    }

    private void GameManagerOnOnGameStateChanged(GameState state) {
        _arrangeFiguresCanvas.SetActive(state == GameState.FiguresArrange);
        _fightCanvas.SetActive(state == GameState.Fight);
    }

    public void QuitGame() {
        Application.Quit();
    }
}
