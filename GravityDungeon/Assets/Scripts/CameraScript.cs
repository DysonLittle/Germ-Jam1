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

    Quaternion worldRotation;
    Quaternion grabAxis;
    bool changingWorldRotation;

    LineRenderer axisLine;
    Vector3 axisLineDefaultPos = Vector3.down * 50;

    void Start()
    {
        player = GameObject.Find("PlayerCharacter").GetComponent<PlayerScript>();
        cameraObj = transform.GetChild(0);
        rb = GetComponent<Rigidbody>();
        cameraSpacing = cameraSpacingDefault;
        cameraRotation = Quaternion.identity;
        worldRotation = Quaternion.identity;
        grabAxis = Quaternion.identity;
        changingWorldRotation = false;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Confined;
        axisLine = GetComponent<LineRenderer>();
        axisLine.enabled = false;
    }

    
    void Update()
    {
        CheckWorldRotation();
        ChangeRotation();
        ChangeLineRenderer();

        transform.position = player.transform.position;
    }

    //checks mouse and scroll movement and move the camera accordingly
    void ChangeRotation()
    {
        //rotate camera around origin, still facing origin
        float mouseX = Input.GetAxis("Mouse X") * sensitivity.x;
        float mouseY = Input.GetAxis("Mouse Y") * sensitivity.y;

        cameraRotation = Quaternion.AngleAxis(mouseX, worldRotation * Vector3.up) * cameraRotation * Quaternion.AngleAxis(mouseY, Vector3.left);

        rb.MoveRotation(cameraRotation);

        //set the player forward and right vectors so that "forward" is away from the camera, etc. (player controls depend on camera direction)
        Vector3 forwardProj = Vector3.ProjectOnPlane(cameraRotation * Vector3.forward, worldRotation * Vector3.down);
        player.forwardVec = forwardProj.normalized;
        player.rightVec = Quaternion.AngleAxis(90, worldRotation * Vector3.up) * player.forwardVec;

        //zoom in and out by transforming camera object relative to parent at origin
        float scroll = Input.GetAxis("Mouse ScrollWheel");

        cameraSpacing += scroll * sensitivity.z;
        if (cameraSpacing >= -cameraSpacingMin)
            cameraSpacing = -cameraSpacingMin;

        cameraObj.localPosition = new Vector3(0f, 0f, cameraSpacing);
    }

    //checks right click controls for rotating gravity
    void CheckWorldRotation()
    {
        if (Input.GetMouseButtonDown(1))
        {
            changingWorldRotation = true;
            grabAxis = cameraRotation; //grab axis a lock of cameraRotation, that will be compared to the released cameraRotation quat (as a delta quat, so to speak)
            axisLine.enabled = true;
        }

        if (Input.GetMouseButtonUp(1))
        {
            changingWorldRotation = false;
            axisLine.enabled = false;
            //resolve change in gravity, camera, etc.
            Quaternion newGrav = SnapQuat(cameraRotation * Quaternion.Inverse(grabAxis) * worldRotation);   //calculate new gravity and snap it
            worldRotation = newGrav;

            UpdateAllGravityObjects();

            //change the camera rotation so that its z is level with the ground
            Vector3 cameraDown = cameraRotation * Vector3.down;
            Vector3 worldDown = worldRotation * Vector3.down;
            Vector3 worldDownProj = Vector3.ProjectOnPlane(worldDown, cameraRotation * Vector3.forward);
            float adjustmentAngle = Vector3.SignedAngle(cameraDown, worldDownProj, cameraRotation * Vector3.forward);

            cameraRotation = cameraRotation * Quaternion.AngleAxis(adjustmentAngle, Vector3.forward);
            rb.MoveRotation(cameraRotation);
        }
    }

    //rotate the red line renderer to indicate gravity selection
    void ChangeLineRenderer()
    {
        if (changingWorldRotation)
        {
            axisLine.SetPosition(1, cameraRotation * Quaternion.Inverse(grabAxis) * worldRotation * axisLineDefaultPos);
        }
    }

    //snaps quaternion to cardinal directions
    Quaternion SnapQuat(Quaternion q)
    {
        Vector3 rotation = q.eulerAngles;
        
        for (int i = 0; i < 3; i++)
        {
            if (rotation[i] <= 45.0f)
            {
                rotation[i] = 0.0f;
            }
            else if (rotation[i] <= 135.0f)
            {
                rotation[i] = 90.0f;
            }
            else if (rotation[i] <= 225.0f)
            {
                rotation[i] = 180.0f;
            }
            else if (rotation[i] <= 315.0f)
            {
                rotation[i] = 270.0f;
            }
            else
            {
                rotation[i] = 0.0f;
            }
        }

        return Quaternion.Euler(rotation);
    }

    //updates each GravityObject that has followCameraGravity set to true with the worldRotation
    void UpdateAllGravityObjects()
    {
        foreach (GravityObject i in GravityObject.gravityObjectList)
        {
            if (i.followCameraGravity)
            {
                i.gravityDirection = worldRotation * Vector3.down;
            }
        }
    }
}
