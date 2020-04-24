using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public float cameraSpacingDefault;
    float cameraSpacing;
    public float cameraSpacingMin;

    Quaternion cameraRotation;

    public Vector3 sensitivity;

    Rigidbody rb;

    PlayerScript player;
    Transform cameraObj;

    void Start()
    {
        player = GameObject.Find("PlayerCharacter").GetComponent<PlayerScript>();
        cameraObj = transform.GetChild(0);
        rb = GetComponent<Rigidbody>();
        cameraSpacing = cameraSpacingDefault;
        cameraRotation = Quaternion.identity;
        Cursor.visible = false;
    }

    
    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        cameraRotation = Quaternion.AngleAxis(mouseX * sensitivity.x, Vector3.up) * cameraRotation * Quaternion.AngleAxis(mouseY * sensitivity.y, Vector3.left);

        
        rb.MoveRotation(cameraRotation);
        player.forwardVec = new Vector3(Mathf.Sin(Mathf.Deg2Rad * cameraRotation.eulerAngles.y), 0f, Mathf.Cos(Mathf.Deg2Rad * cameraRotation.eulerAngles.y));
        player.rightVec = Quaternion.AngleAxis(90, Vector3.up) * player.forwardVec;
        
        float scroll = Input.GetAxis("Mouse ScrollWheel");

        cameraSpacing += scroll * sensitivity.z;
        if (cameraSpacing >= -cameraSpacingMin)
            cameraSpacing = -cameraSpacingMin;

        cameraObj.localPosition = new Vector3(0f, 0f, cameraSpacing);
        
    }
}
