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
        transform.GetChild(0).GetComponent<Animator>().SetTrigger("ToggleLever");
    }

    public override bool CanInteract(GameObject player)
    {
        PlayerInteractionScript p = player.GetComponent<PlayerInteractionScript>();
        return p.heldObject == null;
    }
}
