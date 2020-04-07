using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpDownGravity : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            Physics.gravity = -Physics.gravity;
        }
    }
}
