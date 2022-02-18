using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleWhileTrigger : TriggerParent
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "RobotComponent") Trigger(true);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "RobotComponent") Trigger(false);
    }
}
