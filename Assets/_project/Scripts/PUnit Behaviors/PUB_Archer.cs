using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PUB_Archer : PlayerUnitBehavior
{
    List<UnitCombatant> targets = new List<UnitCombatant>();

    protected void Start()
    {
        unit.combat.OnDeath.AddListener(StopAllCoroutines);
    }

    public override bool Act()
    {
        targets.Clear();

        for (int i = 0; i < board.size + 1; i++)
        {
            CheckTargetAt(unit.tile.x, i);
        }
        for (int i = 0; i < board.size + 1; i++)
        {
            CheckTargetAt(i, unit.tile.y);
        }
        
        if (targets.Count > 0)
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
            
            if (tDistance < closestDis)
            {
                closestTarget = t;
                closestDis = tDistance;
            }
        }

        unit.combat.Attack(closestTarget);
        yield return new WaitForSeconds(0.2f);
    }
    
    void CheckTargetAt(int x, int y)
    {
        var b = unit.tile.board;
        if (b.IsInbounds(x, y))
        {
            var target = b.tiles[x, y].occupantUnit;
            if (target != null && target.isEnemy)
                targets.Add(target.combat);
        }
    }
}
