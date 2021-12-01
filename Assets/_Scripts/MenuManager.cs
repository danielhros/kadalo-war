using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{

    [SerializeField] private GameObject _arrangeFiguresCanvas;
    [SerializeField] private GameObject _fightCanvas;
    [SerializeField] private GameObject _sureExitCanvas;

    private bool _sureExitActivate = false;

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

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            ToogleExitCanvas();
        }
    }

    public void ToogleExitCanvas() {
        _sureExitActivate = !_sureExitActivate;
        _sureExitCanvas.SetActive(_sureExitActivate);
    }
}
