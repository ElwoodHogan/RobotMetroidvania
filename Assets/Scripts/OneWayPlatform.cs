using UnityEngine;

public class OneWayPlatform : MonoBehaviour
{
    public Collider PlatformCollider;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" || other.tag == "RobotComponent")
        {
            Physics.IgnoreCollision(PlatformCollider, other.GetComponent<Collider>(), true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player" || other.tag == "RobotComponent") Physics.IgnoreCollision(PlatformCollider, other.GetComponent<Collider>(), false);
    }


}
