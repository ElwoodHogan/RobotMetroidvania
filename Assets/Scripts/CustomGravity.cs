using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomGravity : MonoBehaviour
{

    public static float globalGravity = -9.81f;
    [SerializeField] float gravityScale = 5.0f;
    Rigidbody _rb;
    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _rb.useGravity = false;
    }
    void Update()
    {
        //Custom gravity
        Vector3 gravity = globalGravity * gravityScale * Vector3.up;
        _rb.AddForce(gravity, ForceMode.Acceleration);
    }
}
