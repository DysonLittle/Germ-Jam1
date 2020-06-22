using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractionScript : MonoBehaviour
{
    SphereCollider sc;
    Interactable currentInteractableHover;
    public Interactable currentInteractableSelected;
    void Start()
    {
        sc = GetComponents<SphereCollider>()[1]; //get the second, larger one, used for interactable objects
        currentInteractableHover = null;
        currentInteractableSelected = null;
    }

    void Update()
    {
        if (!currentInteractableSelected)
            FindInteractables();
        CheckSelection();
    }

    void CheckSelection()
    {
        if (Input.GetKeyDown(KeyCode.E) && currentInteractableHover)
        {
            currentInteractableSelected = currentInteractableHover;
            UpdateCurrentInteractableHover(null);
            currentInteractableSelected.Interact(gameObject);
            
        }
    }

    void FindInteractables()
    {
        Collider[] colliderHits = Physics.OverlapSphere(sc.center + transform.position, sc.radius);
        
        if (colliderHits.Length == 0) //no gameObjects in sphere
        {
            UpdateCurrentInteractableHover(null);
            return;
        }

        GameObject closestInteractable = null;
        for (int i = 0; i < colliderHits.Length; i++)
        {
            GameObject currentObj = colliderHits[i].gameObject;
            if (currentObj.GetComponent<Interactable>())
            {
                if (!closestInteractable)
                {
                    closestInteractable = currentObj;
                }
                else if((currentObj.transform.position - sc.center - transform.position).sqrMagnitude < (closestInteractable.transform.position - sc.center - transform.position).sqrMagnitude)
                {
                    closestInteractable = currentObj;
                }
            }
        }

        if (!closestInteractable) //no interactables found
        {
            UpdateCurrentInteractableHover(null);
            return;
        }

        UpdateCurrentInteractableHover(closestInteractable.GetComponent<Interactable>());
    }

    void UpdateCurrentInteractableHover(Interactable newInteractable)
    {
        if (newInteractable == currentInteractableHover)
            return;

        if (currentInteractableHover)
        {
            currentInteractableHover.Hover(false);
        }
        currentInteractableHover = newInteractable;
        if (currentInteractableHover)
        {
            currentInteractableHover.Hover();
        }
    }
}
