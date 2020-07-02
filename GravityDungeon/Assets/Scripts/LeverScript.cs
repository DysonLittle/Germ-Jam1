using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverScript : Interactable
{

    private void Start()
    {
        
    }

    private void Update()
    {
        
    }

    public override void Interact(GameObject player)
    {
        Debug.Log("Pull the lever, Kronk!");
    }

    public override bool CanInteract(GameObject player)
    {
        return true;
    }
}
