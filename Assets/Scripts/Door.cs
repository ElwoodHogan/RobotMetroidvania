using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Door : ActivatableParent
{
    [SerializeField] Transform openTo;
    [SerializeField] float openToFloat;
    [SerializeField] Vector3 closeTo;
    [SerializeField] float openCloseTime;

    private void Awake()
    {
        if (!isActive) closeTo = transform.position;
    }

    protected override void OnDeactivate()
    {
        transform.DOMoveY(closeTo.y, openCloseTime);
    }

    protected override void OnActivate()
    {
        if(openTo)   transform.DOMoveY(openTo.position.y, openCloseTime);
        else transform.DOMoveY(openToFloat, openCloseTime);
    }
}
