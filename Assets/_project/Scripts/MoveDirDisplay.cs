using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveDirDisplay : MonoBehaviour
{
    public GameObject[] directionArrows;
    public GameUnit unitSelf;
    public bool YtoX = false;

    public void Update()
    {
        UpdateDisplay();
    }

    public void UpdateDisplay()
    {
        foreach(var go in directionArrows)
        {
            go.SetActive(false);
        }


        if (!YtoX)
        {
            var num = GetDirXtoY();
            if (num >= 0)
                directionArrows[GetDirXtoY()].SetActive(true);
        }
        else
        {
            var num = GetDirYtoX();
            if (num >= 0)
                directionArrows[GetDirYtoX()].SetActive(true);
        }
    }

    public int GetDirXtoY()
    {
        var boardManager = GameboardManager.instance;
        var king = GameboardManager.instance.kingUnit;

        if (king.tile == null)
            return -1;

        if (king.tile.x > unitSelf.tile.x)
        {
            return 0;
        }
        if (king.tile.x < unitSelf.tile.x)
        {
            return 1;
        }
        if (king.tile.y > unitSelf.tile.y)
        {
            return 2;
        }
        if (king.tile.y < unitSelf.tile.y)
        {
            return 3;
        }

        return 0;
    }
    public int GetDirYtoX()
    {
        var boardManager = GameboardManager.instance;
        var king = GameboardManager.instance.kingUnit;

        if (king.tile == null)
            return -1;

        if (king.tile.y > unitSelf.tile.y)
        {
            return 2;
        }
        if (king.tile.y < unitSelf.tile.y)
        {
            return 3;
        }
        if (king.tile.x > unitSelf.tile.x)
        {
            return 0;
        }
        if (king.tile.x < unitSelf.tile.x)
        {
            return 1;
        }

        return 0;
    }
}
