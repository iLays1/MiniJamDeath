using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LevelEndManager : MonoBehaviour
{

    [HideInInspector]
    public UnityEvent OnGameWin = new UnityEvent();
    [HideInInspector]
    public UnityEvent OnGameLose = new UnityEvent();

    LevelLoader level;
    GameboardManager boardManager;

    private void Start()
    {
        level = GetComponent<LevelLoader>();
        boardManager = GetComponent<GameboardManager>();

        foreach (var e in FindObjectsOfType<EnemyBehavior>())
        {
            e.unitSelf.combat.OnDeath.AddListener(() =>
            {
                boardManager.enemies.Remove(e);
                CheckWinState();
            });
        }
        foreach (var p in boardManager.pUnits)
        {
            p.unit.combat.OnDeath.AddListener(() =>
            {
                boardManager.pUnits.Remove(p);
                CheckWinState();
            });
        }

        if (level.data.startDialogue != null)
        {
            DialogueSystem.instance.PlayString(level.data.startDialogue);
        }
    }

    public void CheckWinState()
    {
        if (boardManager.enemies.Count <= 0)
        {
            Win();
        }
        if (boardManager.kingUnit == null || boardManager.kingUnit.combat.hp <= 0)
        {
            Lose();
        }
    }

    void Win()
    {
        StopAllCoroutines();
        StartCoroutine(WinCoroutine());
    }
    IEnumerator WinCoroutine()
    {
        OnHoverExpand.globalEnable = true;
        CameraManager.instance.inputsEnabled = true;

        DialogueSystem.instance.PlayString(level.data.winDialogue);
        yield return new WaitForSeconds(level.data.winDialogue.fullTime - 2f);

        AudioManager.instance.Play("WinJingle");

        CameraManager.instance.ChangeCam(1);
        foreach (var p in FindObjectsOfType<PlayerUnitBehavior>())
        {
            p.unit.combat.hp = p.unit.combat.basehp;
            p.unit.combat.UpdateUI();
            p.unit.GetComponent<SelectableUnit>()?.MoveToOrigin();
        }
        OnGameWin.Invoke();

        yield return new WaitForSeconds(3f);
        SceneLoader.instance.LoadNext(0.1f);
    }

    void Lose()
    {
        StopAllCoroutines();
        StartCoroutine(LoseCoroutine());
    }
    IEnumerator LoseCoroutine()
    {
        DialogueSystem.instance.PlayText("oof", 1f, 3f);

        OnHoverExpand.globalEnable = true;
        CameraManager.instance.inputsEnabled = true;

        yield return new WaitForSeconds(1f);

        AudioManager.instance.Play("LoseJingle");

        CameraManager.instance.ChangeCam(1);
        foreach (var p in FindObjectsOfType<PlayerUnitBehavior>())
        {
            p.unit.GetComponent<SelectableUnit>()?.MoveToOrigin();
        }

        yield return new WaitForSeconds(3f);

        SceneLoader.instance.ReloadScene(0.1f);
        OnGameLose.Invoke();
    }
}
