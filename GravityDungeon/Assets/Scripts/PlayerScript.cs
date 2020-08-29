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
    public bool grounded = false;
    float jumpingCooldown = 0.0f;
    Vector3 moveDir = Vector3.zero;


    GravityObject grav;

    public float airControl = 0.1f;
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
        ChangeModelRotation();
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
            rb.AddForce((velDiff / Time.fixedDeltaTime) * rb.mass * airControl);
        }
    }

    void CheckGrounded()
    {
        for (int x = -1; x < 2; x++) //test nine points at the base of the player character
        {
            for (int z = -1; z < 2; z++)
            {
                Vector3 offset = new Vector3(x, 0, z) * 0.5f; //halved because of the scale of our model (the capsule extends .5 in each direction)
                RaycastHit rc;
                bool hit = Physics.Raycast(transform.position + offset, GetGravVector(), out rc, 2.0f);

                //addresses bug where you could jump on an object you were holding
                Interactable held = GetComponent<PlayerInteractionScript>().heldObject;
                if (held)
                {
                    grounded = hit && rc.transform.gameObject != GetComponent<PlayerInteractionScript>().heldObject.gameObject;
                }
                else
                {
                    grounded = hit;
                }

                if (grounded)
                    return; //only one needs to be grounded
            }
        }
    }

    void CheckJump()
    {
        if (jumpingCooldown <= 0.0f)
        {
            if (grounded && Input.GetKey(KeyCode.Space))
            {
                jumpingCooldown = 0.5f;
                rb.AddForce(-GetGravVector() * jumpForce);
            }
        }
        else
        {
            jumpingCooldown -= Time.deltaTime;
        }
    }

    public Vector3 GetGravVector()
    {
        return grav.gravityDirection;
    }

    public void SetGravVector(Vector3 vec)
    {
        grav.gravityDirection = vec;
    }

    public bool GetCanChangeGrav()
    {
        RaycastHit rc;
        bool hit = Physics.Raycast(transform.position, GetGravVector(), out rc, 2.0f);
        if (hit)
        {
            SpecialSurfaceScript surface = rc.transform.gameObject.GetComponent<SpecialSurfaceScript>();
            return !surface || !surface.prohibitGravityChanging;
        }
        else
        {
            return false;
        }
    }

    void ChangeModelRotation()
    {
        transform.rotation = Quaternion.LookRotation(forwardVec, -GetGravVector());
    }


    float Vector2ToAngle(Vector2 vec)
    {
        return Vector2.SignedAngle(Vector2.right, vec);
    }
}
