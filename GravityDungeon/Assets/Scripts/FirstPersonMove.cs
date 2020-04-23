using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FirstPersonMove : MonoBehaviour
{
    CharacterController characterController;

    [SerializeField]
    float moveSpeed;

    [SerializeField]
    float sprintSpeedMultiplier = 1.5f;

    [SerializeField]
    float jumpHeight = 3f;

    [SerializeField]
    Image indicator;

    public float _gravity = -10f;

    private float axisVelocity;

    private bool upDown = true;
    public bool gravDown = true;

    private void Start()
    {
        characterController = GetComponent<CharacterController>();
        indicator.color = Color.gray;
    }

    private void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        if (Input.GetKey(KeyCode.LeftShift))
            vertical *= sprintSpeedMultiplier;

        Vector3 movement = horizontal * moveSpeed * Time.deltaTime * transform.right +
                           vertical * moveSpeed * Time.deltaTime * transform.forward;

        if (characterController.isGrounded)
            axisVelocity = -0.5f;


        if (Input.GetKeyDown(KeyCode.Space) && characterController.isGrounded)
            axisVelocity = Mathf.Sqrt(jumpHeight * -2f * _gravity);

        if(Input.mouseScrollDelta.y < 0 || Input.mouseScrollDelta.y > 0)
        {
            gravDown = !gravDown;
            if (gravDown)
                indicator.color = Color.gray;
            else
                indicator.color = Color.blue;
        }

        if (Input.GetKeyDown(KeyCode.Mouse1) && gravDown)
        {
            upDown = true;
            _gravity = -_gravity;
        }
        else if(Input.GetKeyDown(KeyCode.Mouse1) && !gravDown)
        {
            upDown = false;
            _gravity = -_gravity;
        }

        axisVelocity += _gravity * Time.deltaTime;
        if(upDown)
            movement.y = axisVelocity * Time.deltaTime;
        else
            movement.x = axisVelocity * Time.deltaTime;

        characterController.Move(movement);
    }
}
