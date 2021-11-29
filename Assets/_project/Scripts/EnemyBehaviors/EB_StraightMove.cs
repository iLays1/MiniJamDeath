using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EB_StraightMove : EnemyBehavior
{
    [Tooltip("Moves X Then Y. this reverses it to move first to Y then X")]
    public bool reverse = false;

    public override bool Act()
    {
        if (!reverse)
            return XtoY();
        else
            return YtoX();
    }

    bool XtoY()
    {
        var king = GameboardManager.instance.kingUnit;
        if (king.tile.x > unitSelf.tile.x)
        {
            return MoveX(1);
        }
        if (king.tile.x < unitSelf.tile.x)
        {
            return MoveX(-1);
        }
        if (king.tile.y > unitSelf.tile.y)
        {
            return MoveY(1);
        }
        if (king.tile.y < unitSelf.tile.y)
        {
            return MoveY(-1);
        }

        return true;
    }
    bool YtoX()
    {
        var king = GameboardManager.instance.kingUnit;
        if (king.tile.y > unitSelf.tile.y)
        {
            return MoveY(1);
        }
        if (king.tile.y < unitSelf.tile.y)
        {
            return MoveY(-1);
        }
        if (king.tile.x > unitSelf.tile.x)
        {
            return MoveX(1);
        }
        if (king.tile.x < unitSelf.tile.x)
        {
            return MoveX(-1);
        }

        return true;
    }
}
