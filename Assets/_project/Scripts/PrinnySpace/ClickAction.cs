using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ClickAction : MonoBehaviour
{
    public UnityEvent OnClick = new UnityEvent();

    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
            OnClick.Invoke();
    }
}
