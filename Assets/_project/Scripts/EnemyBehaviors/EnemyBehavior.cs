using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyBehavior : MonoBehaviour
{
    public Vector2Int startPos;
    public GameUnit unitSelf;
    protected GameboardManager boardManager;
    protected GameBoard board;

    protected virtual void Awake()
    {
        boardManager = GameboardManager.instance;
        board = FindObjectOfType<GameBoard>();
    }
    protected virtual void Start()
    {
        unitSelf.SetTile(board.tiles[startPos.x, startPos.y]);
    }

    public abstract bool Act();

    public bool MoveX(int dirx)
    {
        if (unitSelf.tile.x + dirx < 0 || unitSelf.tile.x + dirx >= board.size)
            return false;
        var newTile = board.tiles[unitSelf.tile.x + dirx, unitSelf.tile.y];

        if (newTile.occupantUnit == null)
        {
            unitSelf.SetTile(newTile);
            return true;
        }

        if(newTile.occupantUnit != null && !newTile.occupantUnit.isEnemy)
        {
            if (unitSelf == null) return false;
            unitSelf.combat.Attack(newTile.occupantUnit.combat);
            return true;
        }

        return false;
    }
    public bool MoveY(int diry)
    {
        if (unitSelf.tile.y + diry < 0 || unitSelf.tile.y + diry >= board.size)
            return false;
        var newTile = board.tiles[unitSelf.tile.x, unitSelf.tile.y + diry];

        if (newTile.occupantUnit == null)
        {
            unitSelf.SetTile(newTile);
            return true;
        }

        if (newTile.occupantUnit != null && !newTile.occupantUnit.isEnemy)
        {
            if (unitSelf == null) return false;
            unitSelf.combat.Attack(newTile.occupantUnit.combat);
            return true;
        }

        return false;
    }
}
