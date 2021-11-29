using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class UnitCombatant : MonoBehaviour
{
    [HideInInspector]
    public UnityEvent OnDeath = new UnityEvent();
    public GameUnit unit;

    public int hp = 2;
    [HideInInspector]
    public int basehp;
    public int atk = 1;

    public TextMeshPro atkText;
    public TextMeshPro hpText;

    private void Start()
    {
        basehp = hp;
        UpdateUI();
        unit = GetComponent<GameUnit>();
    }

    public void Attack(UnitCombatant target)
    {
        if (target == null) return;
        if (atk <= 0) return;
        if (hp <= 0) return;

        Vector3 dir = target.transform.position - transform.position;
        dir.Normalize();
        transform.DOPunchPosition(dir * 0.4f, 0.2f, 2, 0f).SetEase(Ease.InOutBack);
        target.TakeDamage(atk);
    }

    public void TakeDamage(int damage)
    {
        hp -= damage;

        AudioManager.instance.Play("TileHit");

        Vector3 dir = Vector3.up;
        dir.Normalize();
        transform.DOPunchPosition(dir * 0.4f, 0.2f, 2, 0f).SetEase(Ease.InOutBack);

        if (hp <= 0)
        {
            hp = 0;
            AudioManager.instance.Play("TileBreak");
            OnDeath.Invoke();
            unit.parent.gameObject.SetActive(false);
        }

        UpdateUI();
    }

    public void UpdateUI()
    {
        atkText.text = atk.ToString();
        hpText.text = hp.ToString();
    }
}
