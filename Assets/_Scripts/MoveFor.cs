using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveFor : MonoBehaviour
{

    [SerializeField] private Vector3 _moveFor;
    [SerializeField] private float t;

    private bool canMove = false;
    private Vector3 finalPosition;

    private void Start() {
        finalPosition = transform.position + _moveFor;
    }

    private void Awake() {
        GameManager.OnGameStateChanged += GameManagerOnOnGameStateChanged;
    }

    private void OnDestroy() {
        GameManager.OnGameStateChanged -= GameManagerOnOnGameStateChanged;
    }

    private void GameManagerOnOnGameStateChanged(GameState state)
    {
        if(state == GameState.Fight) {
            canMove = true;
        }
    }
    // Update is called once per frame
    void Update() {
        if (canMove) {
            MoveGameObject();
        };
    }

    void MoveGameObject() {
        Vector3 actualPosition = transform.position;        
        transform.position = Vector3.Lerp(actualPosition, finalPosition, t);
    }

   
}
