using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatformScriptAlt : Toggleable
{
    public float lerpTotalTime;
    float lerpTimeElapsed = 0.0f;
    Vector3 offPosition, onPosition;
    bool movingForward = true;
    bool moving = false;


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
        if (moving)
        {
            if (movingForward)
            {
                lerpTimeElapsed += Time.deltaTime;
                if (lerpTimeElapsed >= lerpTotalTime)
                {
                    lerpTimeElapsed = lerpTotalTime - (lerpTimeElapsed - lerpTotalTime); //written like this for clarity. When we overshoot the end, reflect us back appropriately
                    movingForward = false;
                }
            }
            else
            {
                lerpTimeElapsed -= Time.deltaTime;
                if (lerpTimeElapsed <= 0.0f)
                {
                    lerpTimeElapsed *= -1.0f; //When we overshoot the beginning, reflect us back appropriately
                    movingForward = true;
                }
                
            }
            transform.position = Vector3.Lerp(offPosition, onPosition, lerpTimeElapsed / lerpTotalTime);
        }
    }

    public override void Toggle(bool toggleState)
    {
        moving = toggleState;
    }

}
