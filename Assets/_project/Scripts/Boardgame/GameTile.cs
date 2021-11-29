using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameTileEvent : UnityEvent<GameTile> { }
public class GameTile : MonoBehaviour
{
    public static GameTileEvent OnTileClicked = new GameTileEvent();

    public GameUnit occupantUnit;
    public GameBoard board;
    public int x;
    public int y;

    private void OnMouseOver()
    {
        if(Input.GetMouseButtonDown(0))
        {
            OnTileClicked.Invoke(this);
        }
    }
}
