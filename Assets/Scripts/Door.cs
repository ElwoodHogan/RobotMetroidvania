using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Door : ActivatableParent
{
    bool open = false;

    [SerializeField] Transform openTo;
    [SerializeField] Vector3 closeTo;

    private void Awake()
    {
        if (!open) closeTo = transform.position;
    }
    public override void Activate()
    {
        if (open) transform.DOMove(closeTo, 2);
        else transform.DOMove(openTo.position, 2);
        open = !open;
    }

    public override void Activate(bool set)
    {
        if (open) transform.DOMove(closeTo, 2);
        else transform.DOMove(openTo.position, 2);
    }
}
