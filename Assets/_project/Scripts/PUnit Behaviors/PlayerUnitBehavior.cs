using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerUnitBehavior : MonoBehaviour
{
    public abstract bool Act();
    protected GameBoard board;

    public GameUnit unit;
    
    protected virtual void Awake()
    {
        unit = GetComponentInChildren<GameUnit>();
        board = FindObjectOfType<GameBoard>();
    }
}
