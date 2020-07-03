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
        gameObject.transform.SetParent(player.transform);
        gameObject.GetComponent<Rigidbody>().collisionDetectionMode = CollisionDetectionMode.ContinuousSpeculative;
        gameObject.GetComponent<Rigidbody>().isKinematic = true;
        gameObject.GetComponent<Collider>().enabled = false;

    }

    public override bool CanInteract(GameObject player)
    {
        PlayerInteractionScript p = player.GetComponent<PlayerInteractionScript>();
        return p.heldObject == null;
    }

    public override void StopInteracting(GameObject player)
    {
        //register as held object
        PlayerInteractionScript p = player.GetComponent<PlayerInteractionScript>();
        p.heldObject = null;

        //actually pick up (physics)
        gameObject.transform.SetParent(null);
        gameObject.GetComponent<Rigidbody>().isKinematic = false;
        gameObject.GetComponent<Rigidbody>().collisionDetectionMode = CollisionDetectionMode.Continuous;
        gameObject.GetComponent<Collider>().enabled = true;
    }
}
