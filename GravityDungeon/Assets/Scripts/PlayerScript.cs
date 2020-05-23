using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    readonly float rootTwo = Mathf.Sqrt(2.0f);

    public Vector3 forwardVec = Vector3.forward;
    public Vector3 rightVec = Vector3.right;
    public float moveVelocity;
    public float jumpForce;
    Rigidbody rb;
    bool grounded = false;
    bool jumping = false;
    Vector3 moveDir = Vector3.zero;

    GravityObject grav;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        grav = GetComponent<GravityObject>();
    }

    void Update()
    {
        CheckMove();
        CheckGrounded();
        CheckJump();
    }
    void FixedUpdate()
    {
        MovePlayer();
    }

    //void CheckMoveDef()
    //{
    //    //Vector3 moveDir = forwardVec * Input.GetAxis("Vertical") + rightVec * Input.GetAxis("Horizontal");
    //    //float nonGravSpeedSq = Vector3.ProjectOnPlane(rb.velocity, GetGravVector()).sqrMagnitude;
    //    //float dragMultiplier = (termVel * termVel - nonGravSpeedSq) / (termVel * termVel);
    //    //rb.AddForce(moveDir.normalized * moveAccl * rb.mass * dragMultiplier);
    //    Vector3 moveDir = forwardVec * Input.GetAxis("Vertical") + rightVec * Input.GetAxis("Horizontal");
    //    rb.AddForce(moveDir.normalized * moveAccl * rb.mass);

    //    Vector3 horizontalVel = Vector3.ProjectOnPlane(rb.velocity, GetGravVector());
    //    if (horizontalVel.sqrMagnitude > termVel * termVel)
    //    {
    //        rb.velocity = Vector3.Project(rb.velocity, GetGravVector()) + (horizontalVel.normalized * termVel);
    //    }
    //}

    //void CheckMoveDef2()
    //{
    //    Vector3 moveDir = forwardVec * Input.GetAxis("Vertical") + rightVec * Input.GetAxis("Horizontal");
    //    Vector3 verticalComponent = Vector3.Project(rb.velocity, GetGravVector());
    //    //Vector3 horizontalComponent = Vector3.ProjectOnPlane(rb.velocity, GetGravVector());
    //    Vector3 horizontalComponent = moveDir.normalized * termVel;
    //    rb.velocity = verticalComponent + horizontalComponent;

    //}

    //void CheckMoveDef3()
    //{
    //    Vector3 moveDir = (forwardVec * Input.GetAxis("Vertical") + rightVec * Input.GetAxis("Horizontal")).normalized;

    //    Vector3 horizontalComponent = Vector3.ProjectOnPlane(rb.velocity, GetGravVector());
    //    float moveEffectiveness = (termVel - horizontalComponent.magnitude) / termVel;
    //    if (moveEffectiveness < 0)
    //        moveEffectiveness = 0;
    //    Vector3 parallelComponent = Vector3.Project(moveDir, horizontalComponent);
    //    Vector3 perpendicularComponent = moveDir - parallelComponent;
    //    if (Vector3.Dot(parallelComponent, horizontalComponent) >= 0)
    //    {
    //        parallelComponent *= moveEffectiveness;
    //    }

    //    rb.AddForce((parallelComponent + perpendicularComponent) * moveAccl * rb.mass);
    //}

    void CheckMove()
    {
        Vector3 verticalAxis = forwardVec * Input.GetAxis("Vertical");
        Vector3 horizontalAxis = rightVec * Input.GetAxis("Horizontal");
        if (verticalAxis != Vector3.zero && horizontalAxis != Vector3.zero)
        {
            verticalAxis /= rootTwo;
            horizontalAxis /= rootTwo;
        }
        moveDir = verticalAxis + horizontalAxis;
    }

    void MovePlayer()
    {
        Vector3 horizontalComponent = Vector3.ProjectOnPlane(rb.velocity, GetGravVector());

        Vector3 velDiff = (moveDir * moveVelocity) - horizontalComponent;

        if (grounded)
        {
            rb.AddForce((velDiff / Time.fixedDeltaTime) * rb.mass);
        }
        else
        {
            rb.AddForce((velDiff / Time.fixedDeltaTime) * rb.mass * 0.1f);
        }

        if (jumping)
        {
            rb.AddForce(-GetGravVector() * jumpForce);
        }
    }

    void CheckGrounded()
    {
        grounded = Physics.Raycast(transform.position, transform.TransformDirection(GetGravVector()), 1.1f);
    }

    void CheckJump()
    {
        jumping = grounded && Input.GetKey(KeyCode.Space);
    }

    public Vector3 GetGravVector()
    {
        return grav.gravityDirection;
    }

    public void SetGravVector(Vector3 vec)
    {
        grav.gravityDirection = vec;
    }
}
