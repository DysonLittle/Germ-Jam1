using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class CreateRoomScript : EditorWindow
{
    string parentObjectName = "";
    Vector3 parentObjectLocation = Vector3.zero;
    Vector3Int dimentions = Vector3Int.one;
    GameObject wallPrefab;

    [MenuItem("Tools/CreateRoomTool")]
    public static void ShowWindow()
    {
        GetWindow(typeof(CreateRoomScript));
    }

    private void OnGUI()
    {
        GUILayout.Label("Create Room", EditorStyles.boldLabel);

        parentObjectName = EditorGUILayout.TextField("Parent Object Name", parentObjectName);
        parentObjectLocation = EditorGUILayout.Vector3Field("Parent Object Location", parentObjectLocation);
        dimentions = EditorGUILayout.Vector3IntField("Dimentions", dimentions);
        wallPrefab = EditorGUILayout.ObjectField("Wall Prefab", wallPrefab, typeof(GameObject), false) as GameObject;

        if (GUILayout.Button("Create"))
        {
            SpawnBox();
        }
    }

    private void SpawnBox()
    {
        if (dimentions.x < 1 || dimentions.y < 1 || dimentions.z < 1)
        {
            Debug.LogError("Dimentions must be 1 or more");
            return;
        }
        GameObject parent = new GameObject();
        parent.name = parentObjectName;
        parent.transform.position = parentObjectLocation;

        //floor
        GameObject tempWall = Instantiate(wallPrefab, parent.transform.position + new Vector3(0f,
                                                                                                -dimentions.y / 2f,
                                                                                                0f
                                                                                                ), Quaternion.identity, parent.transform);
        tempWall.transform.localScale = new Vector3(dimentions.x * 0.1f, 1.0f, dimentions.z * 0.1f);
        tempWall.name = "Floor";

        //ceiling
        tempWall = Instantiate(wallPrefab, parent.transform.position + new Vector3(0f,
                                                                                        dimentions.y / 2f,
                                                                                        0f
                                                                                        ), Quaternion.identity, parent.transform);
        tempWall.transform.localScale = new Vector3(dimentions.x * 0.1f, 1.0f, dimentions.z * 0.1f);
        tempWall.transform.rotation = Quaternion.Euler(new Vector3(0.0f, 0.0f, 180.0f));
        tempWall.name = "Ceiling";

        //left wall
        tempWall = Instantiate(wallPrefab, parent.transform.position + new Vector3(-dimentions.x / 2f,
                                                                                        0f,
                                                                                        0f
                                                                                        ), Quaternion.identity, parent.transform);
        tempWall.transform.localScale = new Vector3(dimentions.x * 0.1f, 1.0f, dimentions.z * 0.1f);
        tempWall.transform.rotation = Quaternion.Euler(new Vector3(0.0f, 0.0f, -90.0f));
        tempWall.name = "Left Wall";

        //right wall
        tempWall = Instantiate(wallPrefab, parent.transform.position + new Vector3(dimentions.x / 2f,
                                                                                        0f,
                                                                                        0f
                                                                                        ), Quaternion.identity, parent.transform);
        tempWall.transform.localScale = new Vector3(dimentions.x * 0.1f, 1.0f, dimentions.z * 0.1f);
        tempWall.transform.rotation = Quaternion.Euler(new Vector3(0.0f, 0.0f, 90.0f));
        tempWall.name = "Right Wall";

        //far wall
        tempWall = Instantiate(wallPrefab, parent.transform.position + new Vector3(0f,
                                                                                        0f,
                                                                                        dimentions.z / 2f
                                                                                        ), Quaternion.identity, parent.transform);
        tempWall.transform.localScale = new Vector3(dimentions.x * 0.1f, 1.0f, dimentions.z * 0.1f);
        tempWall.transform.rotation = Quaternion.Euler(new Vector3(-90.0f, 0.0f, 0.0f));
        tempWall.name = "Far Wall";

        //near wall
        tempWall = Instantiate(wallPrefab, parent.transform.position + new Vector3(0f,
                                                                                        0f,
                                                                                        -dimentions.z / 2f
                                                                                        ), Quaternion.identity, parent.transform);
        tempWall.transform.localScale = new Vector3(dimentions.x * 0.1f, 1.0f, dimentions.z * 0.1f);
        tempWall.transform.rotation = Quaternion.Euler(new Vector3(90.0f, 0.0f, 0.0f));
        tempWall.name = "Near Wall";
    }

    private void SpawnBoxDef2()
    {
        if (dimentions.x < 1 || dimentions.y < 1 || dimentions.z < 1)
        {
            Debug.LogError("Dimentions must be 1 or more");
            return;
        }
        GameObject parent = new GameObject();
        parent.name = parentObjectName;
        parent.transform.position = parentObjectLocation;

        //floor
        GameObject tempWall = Instantiate(wallPrefab, parent.transform.position + new Vector3(  0f,
                                                                                                -dimentions.y / 2f,
                                                                                                0f
                                                                                                ), Quaternion.identity, parent.transform);
        tempWall.transform.localScale = new Vector3(dimentions.x, 0.1f, dimentions.z);
        tempWall.name = "Floor";

        //ceiling
        tempWall = Instantiate(wallPrefab, parent.transform.position + new Vector3(     0f,
                                                                                        dimentions.y / 2f,
                                                                                        0f
                                                                                        ), Quaternion.identity, parent.transform);
        tempWall.transform.localScale = new Vector3(dimentions.x, 0.1f, dimentions.z);
        tempWall.name = "Ceiling";

        //left wall
        tempWall = Instantiate(wallPrefab, parent.transform.position + new Vector3(     -dimentions.x / 2f,
                                                                                        0f,
                                                                                        0f
                                                                                        ), Quaternion.identity, parent.transform);
        tempWall.transform.localScale = new Vector3(0.1f, dimentions.y, dimentions.z);
        tempWall.name = "Left Wall";

        //right wall
        tempWall = Instantiate(wallPrefab, parent.transform.position + new Vector3(     dimentions.x / 2f,
                                                                                        0f,
                                                                                        0f
                                                                                        ), Quaternion.identity, parent.transform);
        tempWall.transform.localScale = new Vector3(0.1f, dimentions.y, dimentions.z);
        tempWall.name = "Right Wall";

        //far wall
        tempWall = Instantiate(wallPrefab, parent.transform.position + new Vector3(     0f,
                                                                                        0f,
                                                                                        dimentions.z / 2f
                                                                                        ), Quaternion.identity, parent.transform);
        tempWall.transform.localScale = new Vector3(dimentions.x, dimentions.y, 0.1f);
        tempWall.name = "Far Wall";

        //near wall
        tempWall = Instantiate(wallPrefab, parent.transform.position + new Vector3(     0f,
                                                                                        0f,
                                                                                        -dimentions.z / 2f
                                                                                        ), Quaternion.identity, parent.transform);
        tempWall.transform.localScale = new Vector3(dimentions.x, dimentions.y, 0.1f);
        tempWall.name = "Near Wall";
    }

    private void SpawnBoxDef()
    {
        if (dimentions.x < 1 || dimentions.y < 1 || dimentions.z < 1)
        {
            Debug.LogError("Dimentions must be 1 or more");
            return;
        }
        GameObject parent = new GameObject();
        parent.name = parentObjectName;
        parent.transform.position = parentObjectLocation;

        for (int i = 0; i < dimentions.x; i++)
        {
            for (int j = 0; j < dimentions.y; j++)
            {
                for (int k = 0; k < dimentions.z; k++)
                {
                    if (i == 1 || i == dimentions.x - 1 ||
                        j == 0 || j == dimentions.y - 1 ||
                        k == 0 || k == dimentions.z - 1)
                    {
                        GameObject tempWall = Instantiate(wallPrefab, parent.transform.position + new Vector3(
                                                                                                                i - (dimentions.x - 1) / 2,
                                                                                                                j - (dimentions.y - 1) / 2,
                                                                                                                k - (dimentions.z - 1) / 2
                                                                                                                ), Quaternion.identity, parent.transform);
                        tempWall.name = "Wall (" + i + ", " + j + ", " + k + ")";
                    }
                }
            }
        }
    }
}
