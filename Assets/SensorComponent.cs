using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SensorComponent : RobotComponent
{
    [SerializeField] Transform MaskSphere;
    [SerializeField] float MaskSphereActiveSize = 20;
    protected override void OnAttach()
    {
        MaskSphere.DOScale(150, .8f).SetEase(Ease.InQuad);
    }

    protected override void OnDetach()
    {
        MaskSphere.DOScale(MaskSphereActiveSize, .3f).SetEase(Ease.OutQuad);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, MaskSphereActiveSize/2);
    }
}
