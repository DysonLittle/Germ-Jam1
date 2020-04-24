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
    }

    void Update()
    {

    }
    void FixedUpdate()
    {
        CheckMove();
    }

    void CheckMoveDef()
    {
        //Vector3 moveDir = forwardVec * Input.GetAxis("Vertical") + rightVec * Input.GetAxis("Horizontal");
        //float nonGravSpeedSq = Vector3.ProjectOnPlane(rb.velocity, GetGravVector()).sqrMagnitude;
        //float dragMultiplier = (termVel * termVel - nonGravSpeedSq) / (termVel * termVel);
        //rb.AddForce(moveDir.normalized * moveAccl * rb.mass * dragMultiplier);
        Vector3 moveDir = forwardVec * Input.GetAxis("Vertical") + rightVec * Input.GetAxis("Horizontal");
        rb.AddForce(moveDir.normalized * moveAccl * rb.mass);

        Vector3 horizontalVel = Vector3.ProjectOnPlane(rb.velocity, GetGravVector());
        if (horizontalVel.sqrMagnitude > termVel * termVel)
        {
            rb.velocity = Vector3.Project(rb.velocity, GetGravVector()) + (horizontalVel.normalized * termVel);
        }
    }

    void CheckMoveDef2()
    {
        Vector3 moveDir = forwardVec * Input.GetAxis("Vertical") + rightVec * Input.GetAxis("Horizontal");
        Vector3 verticalComponent = Vector3.Project(rb.velocity, GetGravVector());
        //Vector3 horizontalComponent = Vector3.ProjectOnPlane(rb.velocity, GetGravVector());
        Vector3 horizontalComponent = moveDir.normalized * termVel;
        rb.velocity = verticalComponent + horizontalComponent;

    }

    void CheckMove()
    {
        Vector3 moveDir = (forwardVec * Input.GetAxis("Vertical") + rightVec * Input.GetAxis("Horizontal")).normalized;
        

        Vector3 horizontalComponent = Vector3.ProjectOnPlane(rb.velocity, GetGravVector());
        float moveEffectiveness = (termVel - horizontalComponent.magnitude) / termVel;
        if (moveEffectiveness < 0)
            moveEffectiveness = 0;
        Vector3 parallelComponent = Vector3.Project(moveDir, horizontalComponent);
        Vector3 perpendicularComponent = moveDir - parallelComponent;
        if (Vector3.Dot(parallelComponent, horizontalComponent) >= 0)
        {
            parallelComponent *= moveEffectiveness;
        }

        rb.AddForce((parallelComponent + perpendicularComponent) * moveAccl * rb.mass);
    }

    Vector3 GetGravVector()
    {
        return GetComponent<GravityObject>().gravityDirection;
    }
}
