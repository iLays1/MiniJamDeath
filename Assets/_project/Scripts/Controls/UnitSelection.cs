using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitSelection : MonoBehaviour
{
    public SelectableUnit selectedUnit;
    public Transform holdTransform;
    public bool playerAbleToAct = true;

    private void Awake()
    {
        SelectableUnit.OnUnitSelect.AddListener(SelectUnit);
        SelectableUnit.OnUnitDeselect.AddListener((SelectableUnit u) => SelectUnit(null));
        GameTile.OnTileClicked.AddListener(TryPlace);
    }

    public void SelectUnit(SelectableUnit selected)
    {
        if (!playerAbleToAct) return;
        
        var kingUnit = GameboardManager.instance.kingUnit;
        if (selected != null && !kingUnit.placed && selected.unit != kingUnit)
        {
            DialogueSystem.instance.PlayText("Play your king first", 3f);
            return;
        }
        
        if (selected == null)
        {
            selectedUnit?.MoveToOrigin();
            selectedUnit = selected;
            CameraManager.instance.ChangeCam(1);
            CameraManager.instance.inputsEnabled = true;
            return;
        }
        
        if (selectedUnit != null) return;
        selectedUnit = selected;
        
        selectedUnit.unit.MoveToTransform(holdTransform);
        CameraManager.instance.ChangeCam(2);
        CameraManager.instance.inputsEnabled = false;
    }

    public void TryPlace(GameTile tile)
    {
        if (!playerAbleToAct) return;
        if (selectedUnit == null) return;
        if (tile.occupantUnit != null) return;

        selectedUnit.unit.SetTile(tile);
        selectedUnit.playerBehavior.Act();

        var kingUnit = GameboardManager.instance.kingUnit;
        if (selectedUnit.unit != kingUnit)
            kingUnit.combat.TakeDamage(1);


        selectedUnit = null;
        
        CameraManager.instance.inputsEnabled = true;
    }
}
