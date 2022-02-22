using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class DoorScale : ActivatableParent
{
    Vector3 ogScale;
    Vector3 ogPos;
    [SerializeField] float openCloseTime;

    private void Awake()
    {
        ogScale = transform.localScale;
        ogPos = transform.position;
    }
    protected override void OnActivate()
    {
        transform.DOScaleY(.01f, openCloseTime);
        transform.DOMoveY(transform.position.y - (ogScale.y), openCloseTime);
    }

    protected override void OnDeactivate()
    {
        transform.DOScale(ogScale, openCloseTime);
        transform.DOMove(ogPos, openCloseTime);
    }
}
