using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityObject : MonoBehaviour
{
    public static ArrayList gravityObjectList;

    static GravityObject()
    {
        gravityObjectList = new ArrayList();
    }

    public Vector3 gravityDirection = Vector3.down;
    public static float g = 9.8f;
    public bool followCameraGravity = true;
    Rigidbody rb;

    ~GravityObject()
    {
        gravityObjectList.Remove(this);
    }
    private void Start()
    {
        gravityObjectList.Add(this);
        rb = gameObject.GetComponent<Rigidbody>();
    }
    private void FixedUpdate()
    {
        
        rb.AddForce(rb.mass * gravityDirection * g);
    }
}
