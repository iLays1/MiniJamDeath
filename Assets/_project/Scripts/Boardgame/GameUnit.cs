using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameUnit : MonoBehaviour
{
    [HideInInspector]
    public bool placed = false;
    public bool isEnemy = false;

    public Transform parent;
    public Collider col;

    [HideInInspector]
    public GameTile tile;
    [HideInInspector]
    public UnitCombatant combat;

    private void Awake()
    {
        combat = GetComponent<UnitCombatant>();
    }

    public void SetTile(GameTile targetTile)
    {
        if (tile != null)
        {
            tile.occupantUnit = null;
        }

        AudioManager.instance.Play("StoneTap");
        tile = targetTile;
        tile.occupantUnit = this;
        MoveToTransform(targetTile.transform);
        parent.SetParent(targetTile.transform);
        if(col != null)
            col.enabled = false;
        placed = true;
    }

    public void MoveToTransform(Transform target)
    {
        transform.DOKill();
        parent.DOMove(target.position, 0.3f);
        parent.DORotateQuaternion(target.rotation, 0.4f);
    }
}
