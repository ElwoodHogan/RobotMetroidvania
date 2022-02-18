using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Airpush : MonoBehaviour
{
    [SerializeField] float pushForce;
    private void OnTriggerStay(Collider other)
    {
        Rigidbody otherRb;
        other.TryGetComponent<Rigidbody>(out otherRb);
        if (otherRb) otherRb.AddForce(transform.up * pushForce, ForceMode.Acceleration);
    }
}
