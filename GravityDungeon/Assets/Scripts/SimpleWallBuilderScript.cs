using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleWallBuilderScript : MonoBehaviour
{
    public GameObject wallPrefab;
    public Vector3 dimentions;
    void Start()
    {
        for (int i = 0; i < dimentions.x; i++)
        {
            for (int j = 0; j < dimentions.y; j++)
            {
                for (int k = 0; k < dimentions.z; k++)
                {
                    if (i != 0 && i != dimentions.x - 1 &&
                        j != 0 && j != dimentions.y - 1 &&
                        k != 0 && k != dimentions.z - 1)
                        continue;
                    Instantiate(wallPrefab, transform.position + new Vector3(i, j, k), Quaternion.identity, gameObject.transform);
                }
            }
        }
    }
}
