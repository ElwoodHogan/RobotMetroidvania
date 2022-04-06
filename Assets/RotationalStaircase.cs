using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class RotationalStaircase : ActivatableParent
{
    [SerializeField] List<Transform> stairs = new List<Transform>();
    [SerializeField] Vector3 ClosedRotation;
    [SerializeField] Vector3 OpenedRotation;
    [SerializeField] float firstStairTime = 1;
    [SerializeField] float stairDelay = 1;
    protected override void OnActivate()
    {
        for (int i = 0; i < stairs.Count; i++)
        {
            stairs[i].DOLocalRotate(OpenedRotation, stairDelay + firstStairTime).SetDelay((float)i * stairDelay);
        }
    }

    protected override void OnDeactivate()
    {
        for (int i = 0; i < stairs.Count; i++)
        {
            stairs[i].DOLocalRotate(ClosedRotation, firstStairTime);
        }
    }
}
