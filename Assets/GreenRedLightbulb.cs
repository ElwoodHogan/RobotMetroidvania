using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenRedLightbulb : ActivatableParent
{
    [SerializeField] Material redBulb;
    [SerializeField] Material greenBulb;
    protected override void OnActivate()
    {
        GetComponent<Renderer>().material = greenBulb;
    }

    protected override void OnDeactivate()
    {
        GetComponent<Renderer>().material = redBulb;
    }
}
