using System;
using UnityEngine;

public class FrontMan : MonoBehaviour
{

    public Camera MainCam;

    public GameObject Player;

    public static FrontMan FM;

    public Action OnUpdate;

    private void Awake()
    {
        FM = this;
    }

    private void Update()
    {
        OnUpdate?.Invoke();
    }

}
