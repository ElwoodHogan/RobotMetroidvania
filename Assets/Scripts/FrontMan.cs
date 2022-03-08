using System;
using UnityEngine;
using Sirenix.OdinInspector;

public class FrontMan : MonoBehaviour
{

    public Camera MainCam;

    public GameObject Player;

    public static FrontMan FM;

    public Action OnUpdate;

    public Action<bool> OnDimensionChange;
    public bool twoD3D;

    private void Awake()
    {
        FM = this;
    }

    private void Update()
    {
        OnUpdate?.Invoke();
    }

    [Button]
    public void SwitchDimesnion()
    {
        twoD3D = !twoD3D;
        OnDimensionChange?.Invoke(twoD3D);
    }

}
