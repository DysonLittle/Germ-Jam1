using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeInteractScript : Interactable
{

    private void Start()
    {

    }

    private void Update()
    {

    }

    public override void Interact(GameObject player)
    {
        //register as held object
        PlayerInteractionScript p = player.GetComponent<PlayerInteractionScript>();

        GameObject toAttach;

        if(p.heldObject == null) //no held, so grab the rope
        {
            p.heldObject = this;
            toAttach = player;
        }
        else //holding something, so affix it to rope
        {
            toAttach = p.heldObject.gameObject;
            p.heldObject.StopInteracting(player);
        }



        //actually pick up (physics)
        gameObject.AddComponent<FixedJoint>();
        gameObject.GetComponent<FixedJoint>().connectedBody = toAttach.GetComponent<Rigidbody>();

    }

    public override bool CanInteract(GameObject player)
    {
        PlayerInteractionScript p = player.GetComponent<PlayerInteractionScript>();
        if(p.heldObject == null)
            return true;

        return !(p.heldObject is RopeInteractScript r);
    }

    public override void StopInteracting(GameObject player)
    {
        //unregister as held object
        PlayerInteractionScript p = player.GetComponent<PlayerInteractionScript>();
        p.heldObject = null;

        //actually put down (physics)
        Destroy(gameObject.GetComponent<FixedJoint>());
    }
}
