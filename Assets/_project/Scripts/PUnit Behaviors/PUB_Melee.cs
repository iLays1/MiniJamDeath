using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PUB_Melee : PlayerUnitBehavior
{
    List<UnitCombatant> targets = new List<UnitCombatant>();

    protected void Start()
    {
        unit.combat.OnDeath.AddListener(StopAllCoroutines);
    }

    public override bool Act()
    {
        targets.Clear();

        CheckTargetAt(1, 0);
        CheckTargetAt(-1, 0);
        CheckTargetAt(0, 1);
        CheckTargetAt(0, -1);

        if(targets.Count > 0)
        {
            StartCoroutine(ActCoroutine());
            return true;
        }

        return false;
    }
    IEnumerator ActCoroutine()
    {
        UnitCombatant closestTarget = null;
        float closestDis = Mathf.Infinity;
        var king = GameboardManager.instance.kingUnit;

        foreach (var t in targets)
        {
            float tDistance = Mathf.Abs(king.tile.x - t.unit.tile.x) + Mathf.Abs(king.tile.y - t.unit.tile.y);
            
            if(tDistance < closestDis)
            {
                closestTarget = t;
                closestDis = tDistance;
            }
        }

        unit.combat.Attack(closestTarget);
        yield return new WaitForSeconds(0.2f);
    }

    public void CheckTargetAt(int xDir, int yDir)
    {
        var b = unit.tile.board;
        if (b.IsInbounds(unit.tile.x + xDir, unit.tile.y + yDir ))
        {
            var target = b.tiles[unit.tile.x + xDir, unit.tile.y + yDir].occupantUnit;
            if (target != null && target.isEnemy)
                targets.Add(target.combat);
        }
    }
}
