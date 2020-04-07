using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GravityScale : MonoBehaviour
{
    public Slider scaler;

    // Update is called once per frame
    void Update()
    {
        Physics.gravity = new Vector3(0.0f, -9.81f, 0.0f) * scaler.value;
    }
}
