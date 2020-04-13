using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public Vector3 forwardVec = Vector3.forward;
    public Vector3 rightVec = Vector3.right;
    public float moveAccl;
    public float termVel;
    Rigidbody rb;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        //rb.drag = moveAccl / termVel;
    }

    void Update()
    {

    }
    void FixedUpdate()
    {
        CheckMove();
    }

    void CheckMove()
    {
        Vector3 moveDir = forwardVec * Input.GetAxis("Vertical") + rightVec * Input.GetAxis("Horizontal");
        
        rb.AddForce(moveDir.normalized * moveAccl * rb.mass);
        float nonGravSpeedSq = Vector3.ProjectOnPlane(rb.velocity, GetGravVector()).sqrMagnitude;

    }

    Vector3 GetGravVector()
    {
        return GetComponent<GravityObject>().gravityDirection;
    }
}
