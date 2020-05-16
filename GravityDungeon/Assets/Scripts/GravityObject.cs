using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityObject : MonoBehaviour
{
    //static list of all GravityObjects
    public static ArrayList gravityObjectList;

    static GravityObject()
    {
        gravityObjectList = new ArrayList(); //static init
    }

    public Vector3 gravityDirection = Vector3.down; //normalized vector that describes gravity
    public static float g = 9.8f; //constant g
    public bool followCameraGravity = true; //determines whether or not the camera script will update the object gravity when the world rotation is changed
    Rigidbody rb;

    ~GravityObject()
    {
        gravityObjectList.Remove(this); //remove from static list on destroy
    }
    private void Start()
    {
        gravityObjectList.Add(this); //add to static list
        rb = gameObject.GetComponent<Rigidbody>();
    }
    private void FixedUpdate()
    {
        //add constant gravity force
        rb.AddForce(rb.mass * gravityDirection * g);
    }
}
