using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverScript : Interactable
{

    public GameObject toggleableObject;
    Toggleable toggleScript;
    bool toggle = false;

    private void Start()
    {
        toggleScript = toggleableObject.GetComponent<Toggleable>();
    }

    private void Update()
    {

    }

    public override void Interact(GameObject player)
    {
        toggle = !toggle;
        if (toggleScript)
        {
            toggleScript.Toggle(toggle);
        }
        else
        {
            throw new System.Exception(toggleableObject.name + " does not have Toggleable script attached.");
        }
        transform.GetChild(0).GetComponent<Animator>().SetTrigger("ToggleLever");
    }

    public override bool CanInteract(GameObject player)
    {
        PlayerInteractionScript p = player.GetComponent<PlayerInteractionScript>();
        return p.heldObject == null;
    }
}
