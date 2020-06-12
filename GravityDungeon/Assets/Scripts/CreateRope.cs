using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateRope : MonoBehaviour
{

    public GameObject prefabRope;
    public int segments;
    public GameObject anchor;
    public Vector3 anchorLoc;
    // Start is called before the first frame update
    void Start()
    {
        Transform anchorTrans = anchor.GetComponent<Transform>();
        Vector3 attachPt = anchorTrans.position + anchorLoc;


        GameObject nextSeg;
        ConfigurableJoint joint;
        Rigidbody attachBody = anchor.GetComponent<Rigidbody>();
        Vector3 attachLoc = anchorLoc;
        Vector3 segPt = attachPt + new Vector3(0, -1, 0); //


        for(int i = 0; i < segments; i++)
        {
            nextSeg = Instantiate(prefabRope, segPt, Quaternion.identity);
            joint = nextSeg.GetComponent<ConfigurableJoint>();
            joint.connectedBody = attachBody;
            joint.connectedAnchor = attachLoc;

            attachBody = nextSeg.GetComponent<Rigidbody>();
            attachLoc = new Vector3(0, -1, 0);
            segPt = segPt + new Vector3(0, -2, 0); //below the previous segment
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
