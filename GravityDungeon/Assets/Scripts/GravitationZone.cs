using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravitationZone : MonoBehaviour
{
    [SerializeField]
    float gravStrength;

    [SerializeField]
    Vector3 gravDir = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerStay(Collider other)
    {
        if (other.GetComponent<Rigidbody>() != null)
            other.GetComponent<Rigidbody>().AddForce(gravDir * gravStrength);
    }
}
