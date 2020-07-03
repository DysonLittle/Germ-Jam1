using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class CreateWallWithDoor : EditorWindow
{
    string parentObjectName = "";
    Vector3 parentObjectLocation = Vector3.zero;
    Vector2 wallDimentions = Vector2.one * 3;
    Vector2 doorLocation = Vector2.zero;
    Vector2 doorDimentions = Vector2.one;
    GameObject wallPrefab;
    GameObject doorPrefab;

    [MenuItem("Tools/CreateWallWithDoorTool")]
    public static void ShowWindow()
    {
        GetWindow(typeof(CreateWallWithDoor));
    }

    private void OnGUI()
    {
        GUILayout.Label("Create Room", EditorStyles.boldLabel);

        parentObjectName = EditorGUILayout.TextField("Parent Object Name", parentObjectName);
        parentObjectLocation = EditorGUILayout.Vector3Field("Parent Object Location", parentObjectLocation);
        wallDimentions = EditorGUILayout.Vector2Field("Dimentions", wallDimentions);
        doorLocation = EditorGUILayout.Vector2Field("Door Location", doorLocation);
        doorDimentions = EditorGUILayout.Vector2Field("Door Dimentions", doorDimentions);
        wallPrefab = EditorGUILayout.ObjectField("Wall Prefab", wallPrefab, typeof(GameObject), false) as GameObject;
        doorPrefab = EditorGUILayout.ObjectField("Door Prefab", doorPrefab, typeof(GameObject), false) as GameObject;

        if (GUILayout.Button("Create"))
        {
            SpawnDoor();
        }
    }

    private void SpawnDoor()
    {
        if (wallDimentions.x < 1 || wallDimentions.y < 1 || doorDimentions.x < 1 || doorDimentions.y < 1)
        {
            Debug.LogError("Dimentions must be 1 or more");
            return;
        }

        GameObject parent = new GameObject();
        parent.name = parentObjectName;
        parent.transform.position = parentObjectLocation;

        GameObject door = Instantiate(doorPrefab, parent.transform);
        CreateWallFromCorners(door, doorLocation + doorDimentions / 2, doorLocation - doorDimentions / 2);
        door.name = "Door";

        //helper vars for math
        Vector2 topLeftDoor = new Vector2(doorLocation.x - doorDimentions.x / 2, doorLocation.y + doorDimentions.y / 2);
        Vector2 topRightDoor = new Vector2(doorLocation.x + doorDimentions.x / 2, doorLocation.y + doorDimentions.y / 2);
        Vector2 bottomRightDoor = new Vector2(doorLocation.x + doorDimentions.x / 2, doorLocation.y - doorDimentions.y / 2);
        Vector2 bottomLeftDoor = new Vector2(doorLocation.x - doorDimentions.x / 2, doorLocation.y - doorDimentions.y / 2);

        Vector2 topLeftWall = new Vector2(-wallDimentions.x / 2, wallDimentions.y / 2);
        Vector2 topRightWall = new Vector2(wallDimentions.x / 2, wallDimentions.y / 2);
        Vector2 bottomRightWall = new Vector2(wallDimentions.x / 2, -wallDimentions.y / 2);
        Vector2 bottomLeftWall = new Vector2(-wallDimentions.x / 2, -wallDimentions.y / 2);

        GameObject leftWall = Instantiate(wallPrefab, parent.transform);
        CreateWallFromCorners(leftWall, bottomLeftWall, topLeftDoor);
        leftWall.name = "Left Wall";

        GameObject topWall = Instantiate(wallPrefab, parent.transform);
        CreateWallFromCorners(topWall, topLeftWall, topRightDoor);
        topWall.name = "Top Wall";

        GameObject rightWall = Instantiate(wallPrefab, parent.transform);
        CreateWallFromCorners(rightWall, bottomRightDoor, topRightWall);
        rightWall.name = "Right Wall";

        GameObject bottomWall = Instantiate(wallPrefab, parent.transform);
        CreateWallFromCorners(bottomWall, bottomLeftDoor, bottomRightWall);
        bottomWall.name = "Bottom Wall";
    }

    void CreateWallFromCorners(GameObject wall, Vector2 corner1, Vector2 corner2)
    {
        wall.transform.localScale = new Vector3(Mathf.Abs(corner1.x - corner2.x) / 10, 1, Mathf.Abs(corner1.y - corner2.y) / 10);
        wall.transform.localPosition = new Vector3((corner1.x + corner2.x) / 2, 0, (corner1.y + corner2.y) / 2);
    }
}