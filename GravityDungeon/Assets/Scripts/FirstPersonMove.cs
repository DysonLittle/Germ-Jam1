using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPersonMove : MonoBehaviour
{
    CharacterController characterController;

    [SerializeField]
    float moveSpeed;

    [SerializeField]
    float sprintSpeedMultiplier = 1.5f;

    [SerializeField]
    float jumpHeight = 3f;

    public float _gravity = -10f;

    private float _yAxisVelocity;

    private void Start()
    {
        characterController = GetComponent<CharacterController>();
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
            _yAxisVelocity = -0.5f;


        if (Input.GetKeyDown(KeyCode.Space) && characterController.isGrounded)
            _yAxisVelocity = Mathf.Sqrt(jumpHeight * -2f * _gravity);

        _yAxisVelocity += _gravity * Time.deltaTime;
        movement.y = _yAxisVelocity * Time.deltaTime;

        characterController.Move(movement);
    }
}
