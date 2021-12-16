using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// class handles hud and menu in level
public class MenuManager : MonoBehaviour
{

    [SerializeField] private GameObject _arrangeFiguresCanvas;
    [SerializeField] private GameObject _fightCanvas;
    [SerializeField] private GameObject _sureExitCanvas;
    [SerializeField] private GameObject _victoryCanvas;
    [SerializeField] private GameObject _looseCanvas;

    private bool _sureExitActivate = false;

    private void Awake() {
        GameManager.OnGameStateChanged += GameManagerOnOnGameStateChanged;
    }

    private void OnDestroy() {
        GameManager.OnGameStateChanged -= GameManagerOnOnGameStateChanged;
    }

    // class subsribes to event and when state is changed, class shows each canvas accordingly
    private void GameManagerOnOnGameStateChanged(GameState state) {

        bool isVictoryState = state == GameState.Victory;
        bool isLooseState = state == GameState.Loose;

        if (isVictoryState || isLooseState) {
            _looseCanvas.SetActive(isLooseState);
            _victoryCanvas.SetActive(isVictoryState);
        } else {
            _arrangeFiguresCanvas.SetActive(state == GameState.FiguresArrange);
            _fightCanvas.SetActive(state == GameState.Fight);
        }
    }

    // When player press [esc], show "sure to exit" modal (only when state of the game is equal to FiguresArrange or Fight)
    private void Update() {
        GameState actualGameState = GameManager.Instance.State;
        if (Input.GetKeyDown(KeyCode.Escape) && (actualGameState == GameState.FiguresArrange || actualGameState == GameState.Fight)) {
            ToogleExitCanvas();
        }
    }

    public void ToogleExitCanvas() {
        _sureExitActivate = !_sureExitActivate;
        _sureExitCanvas.SetActive(_sureExitActivate);
    }
}
