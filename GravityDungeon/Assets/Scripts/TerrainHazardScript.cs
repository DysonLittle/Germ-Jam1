using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TerrainHazardScript : MonoBehaviour
{
    GameObject player;
    GameObject sceneManager;
    void Start()
    {
        player = GameObject.Find("PlayerCharacter");
        sceneManager = GameObject.Find("SceneManager");
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.gameObject == player)
        {
            PlayerDeath();
        }
    }

    void PlayerDeath()
    {
        sceneManager.GetComponent<SceneManagerScript>().ResetLevel();
    }
}
