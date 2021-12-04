using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkipFightButton : MonoBehaviour
{
    public void SkipFight()
    {
        GameManager.Instance.SkipFight();
    }
}
