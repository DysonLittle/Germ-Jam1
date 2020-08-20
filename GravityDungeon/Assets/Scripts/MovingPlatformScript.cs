using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatformScript : Toggleable
{
    public float lerpTotalTime;
    float lerpTimeElapsed = 0.0f;
    Vector3 offPosition, onPosition;
    bool toggle = false;
    

    // Start is called before the first frame update
    void Start()
    {
        offPosition = transform.Find("OffPosition").position;
        onPosition = transform.Find("OnPosition").position;
        transform.position = offPosition;
    }

    // Update is called once per frame
    void Update()
    {
        if (toggle && transform.position != onPosition)
        {
            lerpTimeElapsed += Time.deltaTime;
            if (lerpTimeElapsed > lerpTotalTime)
                lerpTimeElapsed = lerpTotalTime;
            transform.position = Vector3.Lerp(offPosition, onPosition, lerpTimeElapsed / lerpTotalTime);
        }
        else if (!toggle && transform.position != offPosition)
        {
            lerpTimeElapsed -= Time.deltaTime;
            if (lerpTimeElapsed < 0.0f)
                lerpTimeElapsed = 0.0f;
            transform.position = Vector3.Lerp(offPosition, onPosition, lerpTimeElapsed / lerpTotalTime);
        }
        
    }

    public override void Toggle(bool toggleState)
    {
        toggle = toggleState;
    }

}
