using UnityEngine;

public abstract class RobotComponent : MonoBehaviour
{
    public bool isAttachedToPlayer { get; private set; }
    private void Awake()
    {
        isAttachedToPlayer = true;
    }


    public Rigidbody SetKinematic(bool set)
    {
        Rigidbody ComponentRigidbody = GetComponent<Rigidbody>();
        ComponentRigidbody.isKinematic = set;
        return ComponentRigidbody;
    }

    public void SetAttachedToPlayer(bool set)
    {
        if (isAttachedToPlayer == set)
        {
            print("Attachment status did not change");
            return;
        }
        if (set) OnAttach();
        else OnDetach();
        isAttachedToPlayer = set;
    }

    protected abstract void OnDetach();
    protected abstract void OnAttach();



}
