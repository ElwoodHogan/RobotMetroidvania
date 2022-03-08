using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class CustomGravity : MonoBehaviour
{

    public static float globalGravity = -9.81f;
    [SerializeField] static float gravityScale = 5.0f;
    Rigidbody _rb;
    Rigidbody2D _rb2D;
    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _rb.useGravity = false;
    }
    void FixedUpdate()
    {
        //Custom gravity
        Vector3 gravity = (globalGravity * gravityScale) * Vector3.up;
        try
        {
            if (FrontMan.FM.twoD3D)
                _rb2D.AddForce(gravity, ForceMode2D.Force);
            else
                _rb.AddForce(gravity, ForceMode.Acceleration);

        }
        catch (System.Exception)
        {
            if (FrontMan.FM.twoD3D) _rb2D = GetComponent<Rigidbody2D>();
            else _rb = GetComponent<Rigidbody>();
        }
        
       
    }

    [Button]
    public void SetGravityModifier(float newGrav)
    {
        gravityScale = newGrav;
    }
}
