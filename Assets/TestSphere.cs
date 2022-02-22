using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

[ExecuteInEditMode]
public class TestSphere : MonoBehaviour
{
    public VisualEffect VFX;
    public float size;

    // Update is called once per frame
    void Update()
    {
        VFX.SetVector3("Sphere Position", transform.position);
        VFX.SetFloat("SphereRadius", size);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, size);
    }

}
