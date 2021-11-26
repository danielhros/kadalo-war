using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PawnScript : MonoBehaviour
{
    public void Move()
    {
        transform.position = transform.position + (transform.forward) * 2;
    }
}
