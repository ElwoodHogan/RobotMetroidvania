using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class TriggerParent : MonoBehaviour
{
    [SerializeField] List<ActivatableParent> ObjectsToActivate = new List<ActivatableParent>();

    public void Trigger()
    {
        foreach (var activatable in ObjectsToActivate)
        {
            activatable.Activate();
        }
    }

    public void Trigger(bool set)
    {
        foreach (var activatable in ObjectsToActivate)
        {
            activatable.Activate(set);
        }
    }

    [Button]
    private void ManualTrigger()
    {
        Trigger();
    }
}
