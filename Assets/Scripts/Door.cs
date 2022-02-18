using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Door : ActivatableParent
{
    [SerializeField] Transform openTo;
    [SerializeField] Vector3 closeTo;

    private void Awake()
    {
        if (!isActive) closeTo = transform.position;
    }

    protected override void OnDeactivate()
    {
        transform.DOMove(closeTo, 2);
    }

    protected override void OnActivate()
    {
        transform.DOMove(openTo.position, 2);
    }
}
