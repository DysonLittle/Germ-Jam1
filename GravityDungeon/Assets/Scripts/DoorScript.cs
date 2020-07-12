using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : Toggleable
{
    Animator doorAnimator;
    // Start is called before the first frame update
    void Start()
    {
        doorAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public override void Toggle(bool toggleState)
    {

        if (toggleState)
        {
            doorAnimator.SetTrigger("Open Door");
        }
        else
        {
            doorAnimator.SetTrigger("Close Door");
        }
    }
}
