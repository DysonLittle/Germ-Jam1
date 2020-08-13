using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleScreenGimmikScript : MonoBehaviour
{
    Quaternion worldRotation;
    public Vector2 sensitivity;

    // Start is called before the first frame update
    void Start()
    {
        worldRotation = Quaternion.identity;
    }

    // Update is called once per frame
    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * sensitivity.x;
        float mouseY = Input.GetAxis("Mouse Y") * sensitivity.y;

        Vector3 eulerRot = new Vector3(mouseY, 0, mouseX);


        worldRotation = worldRotation * Quaternion.Euler(eulerRot);
        //GetComponent<LineRenderer>().SetPosition(1, (worldRotation * Vector3.down) * 10);
        UpdateAllGravityObjects();
    }

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
