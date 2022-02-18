using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Sirenix.OdinInspector;
[ExecuteInEditMode]
public class SphereMaskController : MonoBehaviour
{
    public bool ShowIfRunning = true;
    public List<Material> MaskShaders = new List<Material>();
    bool twoDor3D = true;
    private void Awake()
    {
        foreach (var maskShader in MaskShaders)
            maskShader.SetVector("_Dimensional", new Vector3(1, 1, 0));
    }

    void Update()
    {
        foreach (var maskShader in MaskShaders)
        {
            maskShader.SetFloat("_SphereRad", transform.localScale.x);
            maskShader.SetVector("_SpherePos", transform.position);
        }
        if(ShowIfRunning) print("running");
    }

    [Button]
    public void Switch2D3D()
    {
        if(twoDor3D) foreach (var maskShader in MaskShaders)
                maskShader.SetVector("_Dimensional", new Vector3(1,1,1));
        else foreach (var maskShader in MaskShaders)
                maskShader.SetVector("_Dimensional", new Vector3(1, 1, 0));
        twoDor3D = !twoDor3D;
    }
}