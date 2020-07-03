using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupScript : Interactable
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
        p.heldObject = this;

        //actually pick up (physics)
        gameObject.AddComponent<FixedJoint>();
        gameObject.GetComponent<FixedJoint>().connectedBody = player.GetComponent<Rigidbody>();

    }

    public override bool CanInteract(GameObject player)
    {
        PlayerInteractionScript p = player.GetComponent<PlayerInteractionScript>();
        return p.heldObject == null;
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
