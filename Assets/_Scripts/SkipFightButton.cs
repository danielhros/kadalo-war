using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// call method SkipFight in gamemanager singleton instacne
public class SkipFightButton : MonoBehaviour
{
    public void SkipFight()
    {
        GameManager.Instance.SkipFight();
    }
}
