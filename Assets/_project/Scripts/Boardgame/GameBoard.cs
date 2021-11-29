using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameBoard : MonoBehaviour
{
    public int size;
    public List<GameTile> tilesList;
    public GameTile[,] tiles;

    private void Awake()
    {
        tiles = new GameTile[size, size];
        int x = 0;
        int y = size - 1;
        foreach(var t in tilesList)
        {
            t.x = x;
            t.y = y;
            t.board = this;

            tiles[x, y] = t;

            x += 1;
            if(x >= size)
            {
                x = 0;
                y -= 1;
            }
        }
    }

    public bool IsInbounds(int x, int y)
    {
        if (x < 0 || x >= size)
            return false;
        if (y < 0 || y >= size)
            return false;
        return true;
    }
}
