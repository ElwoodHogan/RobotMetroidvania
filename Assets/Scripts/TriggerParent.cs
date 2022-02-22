using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Sirenix.OdinInspector;

public class TriggerParent : MonoBehaviour
{
    [SerializeField] List<ActivatableParent> ObjectsToActivate = new List<ActivatableParent>();
    public Action OnTrigger;

    public void Trigger()
    {
        foreach (var activatable in ObjectsToActivate)
        {
            activatable.Activate();
        }
        OnTrigger?.Invoke();
    }

    public void Trigger(bool set)
    {
        foreach (var activatable in ObjectsToActivate)
        {
            activatable.Activate(set);
        }
        OnTrigger?.Invoke();
    }

    [Button]
    private void ManualTrigger()
    {
        Trigger();
    }
}
