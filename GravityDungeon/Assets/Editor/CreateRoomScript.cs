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
        GameObject parent = Instantiate(new GameObject());
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
