using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChangeZoneScript : MonoBehaviour
{
    public string scene;

    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Player")
        {
            SceneManager.LoadScene(scene);
        }
    }
}
