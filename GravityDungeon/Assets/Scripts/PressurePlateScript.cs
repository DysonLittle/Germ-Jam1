using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlateScript : MonoBehaviour
{
    public GameObject toggleableObject;
    Toggleable toggleScript;

    int collidedCount;

    // Start is called before the first frame update
    void Start()
    {
        toggleScript = toggleableObject.GetComponent<Toggleable>();
        collidedCount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnCollisionEnter(Collision collision)
    {
        collidedCount++;
        GetComponentInParent<Animator>().SetBool("Pressed", true);
        Toggle(true);
    }

    private void OnCollisionExit(Collision collision)
    {
        collidedCount--;
        if (collidedCount <= 0)
        {
            GetComponentInParent<Animator>().SetBool("Pressed", false);
            Toggle(false);
            collidedCount = 0;
        }
    }

    void Toggle(bool t)
    {
        if (toggleScript)
        {
            toggleScript.Toggle(t);
        }
        else
        {
            throw new System.Exception(toggleableObject.name + " does not have Toggleable script attached.");
        }
    }
}
