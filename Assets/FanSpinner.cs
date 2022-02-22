using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FanSpinner : MonoBehaviour
{
    [SerializeField]float spinSpeed;

    private void Update()
    {
        transform.eulerAngles += new Vector3(0, 0, spinSpeed);
    }
}
