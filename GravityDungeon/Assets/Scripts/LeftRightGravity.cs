using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftRightGravity : MonoBehaviour
{
    bool firstSwitch = true;
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            if (firstSwitch)
            {
                Physics.gravity = new Vector3(-9.81f, 0.0f, 0.0f);
                firstSwitch = false;
            }
            Physics.gravity = -Physics.gravity;
        }
    }
}
