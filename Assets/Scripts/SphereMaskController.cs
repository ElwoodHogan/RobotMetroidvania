using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;
using UnityEngine.VFX;

using Sirenix.OdinInspector;
[ExecuteInEditMode]
public class SphereMaskController : MonoBehaviour
{
    public bool ShowIfRunning = true;
    public List<Material> MaskShaders = new List<Material>();
    public List<Material> MaskShadersAlwaysIn3D = new List<Material>();
    public List<VisualEffect> VFXParticles = new List<VisualEffect>();
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
        foreach (var maskShader in MaskShadersAlwaysIn3D)
        {
            maskShader.SetFloat("_SphereRad", transform.localScale.x *  1.3f);
            maskShader.SetVector("_SpherePos", transform.position);
        }
        foreach (var particle in VFXParticles)
        {
            particle.SetVector3("Sphere Position", transform.position);
            particle.SetFloat("SphereRadius", transform.localScale.x/2);
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