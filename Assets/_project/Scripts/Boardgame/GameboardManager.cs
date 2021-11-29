using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameboardManager : MonoBehaviour
{
    public static GameboardManager instance;

    [HideInInspector]
    public UnityEvent OnTurnEnd = new UnityEvent();

    public GameUnit kingUnit;
    public List<EnemyBehavior> enemies;
    public List<PlayerUnitBehavior> pUnits;
    UnitSelection pManager;
    bool enemyTurn = false;

    private void Awake()
    {
        instance = this;
        pManager = GetComponent<UnitSelection>();

        CameraManager.instance.ChangeCam(1);
        enemies = new List<EnemyBehavior>();
        foreach (var e in FindObjectsOfType<EnemyBehavior>())
        {
            enemies.Add(e);
        }
        pUnits = new List<PlayerUnitBehavior>();
        foreach (var p in FindObjectsOfType<PlayerUnitBehavior>())
        {
            pUnits.Add(p);
        }

        StartTurn();
    }


    public void StartTurn()
    {
        enemyTurn = false;
        pManager.playerAbleToAct = true;
        OnHoverExpand.globalEnable = true;
        CameraManager.instance.inputsEnabled = true;
    }

    public void EndTurn()
    {
        if (enemyTurn) return;

        AudioManager.instance.Play("EndTurn");

        if (!kingUnit.placed)
        {
            DialogueSystem.instance.PlayText("Play your king first", 3f);
            return;
        }
        
        enemyTurn = true;
        pManager.playerAbleToAct = false;
        OnHoverExpand.globalEnable = false;
        CameraManager.instance.inputsEnabled = false;
        CameraManager.instance.ChangeCam(2);

        StartCoroutine(AiAction());
        OnTurnEnd.Invoke();
    }
    IEnumerator AiAction()
    {
        yield return new WaitForSeconds(0.4f);
        foreach (var unit in pUnits)
        {
            if (unit == null) continue;
            if (unit.unit.placed)
            {
                if (unit.Act())
                {
                    yield return new WaitForSeconds(0.4f);
                    continue;
                }
            }
        }
        yield return new WaitForSeconds(1f);

        //DialougeSystem.instance.PlayText("They hunt you...", 1f, 1f);

        List<EnemyBehavior> nonActs = new List<EnemyBehavior>();
        yield return new WaitForSeconds(0.2f);
        foreach (var e in enemies)
        {
            if (e == null) continue;
            if (e.unitSelf.combat.hp <= 0) continue;

            if(e.Act())
            {
                yield return new WaitForSeconds(0.3f);
                continue;
            }

            nonActs.Add(e);
        }
        foreach (var e in nonActs)
        {
            if (e == null) continue;
            if (e.unitSelf.combat.hp <= 0) continue;
            if (e.Act())
                yield return new WaitForSeconds(0.3f);
        }
        StartTurn();
    }
}
