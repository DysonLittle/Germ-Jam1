using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TerrainHazardScript : MonoBehaviour
{
    GameObject player; //shouldn't be needed anymore
    GameObject sceneManager;
    void Start()
    {
        player = GameObject.Find("PlayerCharacter"); //shouldn't be needed anymore
        sceneManager = GameObject.Find("SceneManager");
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Player")
        {
            PlayerDeath();
        }
    }

    void PlayerDeath()
    {
        sceneManager.GetComponent<SceneManagerScript>().ResetLevel();
    }
}
