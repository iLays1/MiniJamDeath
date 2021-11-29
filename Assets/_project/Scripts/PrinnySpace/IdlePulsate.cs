using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdlePulsate : MonoBehaviour
{
    public float speed = 2f;
    public float scaleFactor = 0.95f;
    Vector3 oScale;
    private void Awake()
    {
        oScale = transform.localScale;

        Sequence s = DOTween.Sequence();
        s.Append(transform.DOScale(oScale * scaleFactor, speed));
        s.Append(transform.DOScale(oScale, speed));
        s.SetLoops(-1);
    }
}
