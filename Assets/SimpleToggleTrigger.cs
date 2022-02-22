using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleToggleTrigger : TriggerParent
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "RobotComponent") Trigger();
    }
}
