using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Raycast : MonoBehaviour
{
    private GameObject raycastedObj;

    [SerializeField]
    int rayLength = 10;

    [SerializeField]
    LayerMask layerMask;

    [SerializeField]
    Image crosshair;

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        Vector3 fwd = transform.TransformDirection(Vector3.forward);

        if(Physics.Raycast(transform.position, fwd, out hit, rayLength, layerMask.value))
        {
            if(hit.collider.CompareTag("Physics"))
            {
                raycastedObj = hit.collider.gameObject;
                CrosshairActive();

                if(Input.GetKeyDown(KeyCode.E))
                {
                    Debug.Log("GRAVITAS");
                    raycastedObj.GetComponent<GravityObject>().gravityDirection = -raycastedObj.GetComponent<GravityObject>().gravityDirection;
                }
            }
        }
        else
        {
            CrosshairNormal();
        }
    }

    void CrosshairActive()
    {
        crosshair.color = Color.green;
    }

    void CrosshairNormal()
    {
        crosshair.color = Color.white;
    }
}
