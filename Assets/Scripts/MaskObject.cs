using UnityEngine;

public class MaskObject : MonoBehaviour
{
    private void Start()
    {
        GetComponent<MeshRenderer>().material.renderQueue = 3002;
    }
}
