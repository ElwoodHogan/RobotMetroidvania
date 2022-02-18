using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public abstract class ActivatableParent : MonoBehaviour
{
    public bool isActive = false;
    private void Awake()
    {
        isActive = false;
    }
    public void Activate()
    {
        isActive = !isActive;
        if (isActive) OnActivate();
        else OnDeactivate();
    }

    public void Activate(bool set)
    {
        if (isActive == set)
        {
            print("isActive status did not change");
            return;
        }
        if (set) OnActivate();
        else OnDeactivate();
        isActive = set;
    }

    protected abstract void OnDeactivate();
    protected abstract void OnActivate();

    [Button]
    public void Trigger()
    {
        Activate();
    }
}
