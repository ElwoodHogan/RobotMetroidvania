using UnityEngine;

public class RobotComponent : MonoBehaviour
{
    public bool isAttachedToPlayer = true;


    public Rigidbody SetKinematic(bool set)
    {
        Rigidbody ComponentRigidbody = GetComponent<Rigidbody>();
        ComponentRigidbody.isKinematic = set;
        return ComponentRigidbody;
    }
}
