using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightCameraRotate : MonoBehaviour
{
    Transform t;
    Vector3 right = Vector3.right;
    Vector3 up = Vector3.up;
    Vector3 forward = Vector3.forward;
    float amount = 60;
    // Start is called before the first frame update
    void Start()
    {
        t = gameObject.GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey("left"))
        {
            Quaternion r = Quaternion.AngleAxis(amount * Time.deltaTime, up);
            t.rotation = r * t.rotation;
            forward = r * forward;
            right = r * right;
        }
        if(Input.GetKey("right"))
        {
            Quaternion r = Quaternion.AngleAxis(-amount * Time.deltaTime, up);
            t.rotation = r * t.rotation;
            forward = r * forward;
            right = r * right;
        }
        if(Input.GetKey("down"))
        {
            Quaternion r = Quaternion.AngleAxis(amount * Time.deltaTime, right);
            t.rotation = r * t.rotation;
            forward = r * forward;
            up = r * up;
        }

        if(Input.GetKey("up"))
        {
            Quaternion r = Quaternion.AngleAxis(-amount * Time.deltaTime, right);
            t.rotation = r * t.rotation;
            forward = r * forward;
            up = r * up;
        }

        if(Input.GetKey("e"))
        {
            Quaternion r = Quaternion.AngleAxis(amount * Time.deltaTime, forward);
            t.rotation = r * t.rotation;
            right = r * right;
            up = r * up;
        }

        if(Input.GetKey("q"))
        {
            Quaternion r = Quaternion.AngleAxis(-amount * Time.deltaTime, forward);
            t.rotation = r * t.rotation;
            right = r * right;
            up = r * up;
        }
    }
}
