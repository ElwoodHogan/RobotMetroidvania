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
        print("activated");
        for (int i = 0; i < stairs.Count; i++)
        {
            stairs[i].DOLocalRotate(OpenedRotation, ((float)i* stairDelay) + firstStairTime);
        }
    }

    protected override void OnDeactivate()
    {
        for (int i = 0; i < stairs.Count; i++)
        {
            stairs[i].DOLocalRotate(ClosedRotation, ((float)i * stairDelay) + firstStairTime);
        }
    }
}
