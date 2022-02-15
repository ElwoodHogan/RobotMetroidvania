using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Sirenix.OdinInspector;
[ExecuteInEditMode]
public class SphereMaskController : MonoBehaviour
{
    public bool ShowIfRunning = true;
    public List<Material> MaskShaders = new List<Material>();

    void Update()
    {
        foreach (var maskShader in MaskShaders)
        {
            maskShader.SetFloat("_SphereRad", transform.localScale.x);
            maskShader.SetVector("_SpherePos", transform.position);
        }
        if(ShowIfRunning) print("running");
    }
}