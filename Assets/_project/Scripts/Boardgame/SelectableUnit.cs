using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SelectableUnitEvent : UnityEvent<SelectableUnit> { }
public class SelectableUnit : MonoBehaviour
{
    public static SelectableUnitEvent OnUnitSelect = new SelectableUnitEvent();
    public static SelectableUnitEvent OnUnitDeselect = new SelectableUnitEvent();

    [HideInInspector]
    public Vector3 oPosition;
    [HideInInspector]
    public Quaternion oRotation;

    public GameUnit unit;
    public PlayerUnitBehavior playerBehavior;

    private void Awake()
    {
        oPosition = unit.parent.position;
        oRotation = unit.parent.localRotation;
    }

    private void Update()
    {
        if (!unit.placed && unit.combat.hp > 0 && Input.GetMouseButtonDown(1))
        {
            OnUnitDeselect.Invoke(this);
        }
    }
    private void OnMouseOver()
    {
        if (!unit.placed && unit.combat.hp > 0 && Input.GetMouseButtonDown(0))
        {
            AudioManager.instance.Play("StoneTap");
            OnUnitSelect.Invoke(this);
        }
    }

    public void MoveToOrigin()
    {
        if (unit.tile != null)
        {
            unit.tile.occupantUnit = null;
        }

        unit.placed = false;
        unit.parent.DOKill();
        unit.parent.SetParent(null);
        unit.parent.DOMove(oPosition, 0.4f);
        unit.parent.DORotateQuaternion(oRotation, 0.4f);
        GetComponent<Collider>().enabled = true;
    }
}
