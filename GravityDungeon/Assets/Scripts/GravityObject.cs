using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityObject : MonoBehaviour
{
    public Vector3 gravityDirection = Vector3.zero;
    public static float g = 9.8f;
    private void FixedUpdate()
    {
        Rigidbody rb = gameObject.GetComponent<Rigidbody>();
        rb.AddForce(rb.mass * gravityDirection * g);
    }
}
