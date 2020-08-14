using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class CreateRope : EditorWindow
{
        GameObject anchor;
        Vector3 anchorLocation;
        int segments;
        GameObject ropePrefab;


        [MenuItem("Tools/CreateRopeTool")]
        public static void ShowWindow()
        {
            GetWindow(typeof(CreateRope));
        }


        private void OnGUI()
        {
            GUILayout.Label("Create Rope", EditorStyles.boldLabel);

            anchor = EditorGUILayout.ObjectField("Anchor", anchor, typeof(GameObject), true) as GameObject;
            anchorLocation = EditorGUILayout.Vector3Field("Anchor Location", anchorLocation);
            segments = EditorGUILayout.IntField("Number of Segments", segments);
            ropePrefab = EditorGUILayout.ObjectField("Rope Segment Prefab", ropePrefab, typeof(GameObject), false) as GameObject;

            if (GUILayout.Button("Create"))
            {
                makeRope();
            }
        }

        private void makeRope()
        {

            Transform anchorTrans = anchor.GetComponent<Transform>();
            Vector3 attachPt = anchorTrans.position + anchorLocation;


            GameObject nextSeg;
            ConfigurableJoint joint;
            Rigidbody attachBody = anchor.GetComponent<Rigidbody>();
            Vector3 attachLoc = anchorLocation;
            Vector3 localOffset = ropePrefab.GetComponent<ConfigurableJoint>().anchor;
            Vector3 globalOffset = ropePrefab.transform.TransformPoint(localOffset) - ropePrefab.transform.position;
            Vector3 segPt = attachPt - globalOffset; //


            for(int i = 0; i < segments; i++)
            {
                nextSeg = Instantiate(ropePrefab, segPt, Quaternion.identity);
                joint = nextSeg.GetComponent<ConfigurableJoint>();
                joint.connectedBody = attachBody;
                joint.connectedAnchor = attachLoc;

                attachBody = nextSeg.GetComponent<Rigidbody>();
                attachLoc = -localOffset;
                segPt = segPt - 2 *  globalOffset; //below the previous segment
            }
        }
}
