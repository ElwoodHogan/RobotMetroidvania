using UnityEngine;

public class OneWayPlatform : MonoBehaviour
{
    [SerializeField] Collider PlatformCollider;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Physics.IgnoreCollision(PlatformCollider, other.GetComponent<Collider>(), true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player") Physics.IgnoreCollision(PlatformCollider, other.GetComponent<Collider>(), false);
    }


}
