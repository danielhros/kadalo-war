using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveFigure : MonoBehaviour
{
    // starting from left top corner
    // if x = 1 meaning from players camera right
    // if x = -1 meaning from players camera left
    // if y = 1 meaning from players camera to player
    // if y = -1 meaning from players camera from player
    public int moveX;
    public int moveY;

    public int posX;
    public int posY;

    public int moves;
    public int doneMoves;
}
